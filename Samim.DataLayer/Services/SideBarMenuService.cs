using System;
using System.Collections.Generic;
using System.Text;
using Samim.ViewModel;
using Samim.DomainLayer;
using Samim.DataLayer.Context;
using System.Linq;
using Samim.DataLayer.UnitOfWork;

namespace Samim.BusinessLayer.Services
{
	public interface ISideBarMenuService
	{
		List<VMSideBarMenu> GetVMSideBarMenuItems();
	}

	public class SideBarMenuService : ISideBarMenuService
	{
		private readonly IBaseRepository<SideBarMenu> _baseRepository;

		public SideBarMenuService(IBaseRepository<SideBarMenu> baseRepository)
		{
			_baseRepository = baseRepository;
		}

		public List<VMSideBarMenu> GetVMSideBarMenuItems()
		{
			var vmSideBarMenuItems = _baseRepository.GetAll().GetAwaiter().GetResult();
			var returningMenuItems = new List<VMSideBarMenu>();
			foreach (var menuItem in vmSideBarMenuItems.Where(x => x.ParentId == null))
			{
				var sideBarMenuItem = new VMSideBarMenu(menuItem);
				if (!sideBarMenuItem.HasParent)
				{
					if (vmSideBarMenuItems.Any(x => x.ParentId == sideBarMenuItem.Id))
					{
						var children = vmSideBarMenuItems.Where(x => x.ParentId == sideBarMenuItem.Id).Select(x => new VMSideBarMenu(x)).ToList();
						var sideBarItems = vmSideBarMenuItems.Select(x => new VMSideBarMenu(x)).ToList();
						sideBarMenuItem.Children.AddRange(MakeParentChildRelations(children, sideBarItems));
					}
					returningMenuItems.Add(sideBarMenuItem);
				}
			}
			return returningMenuItems;
		}

		private List<VMSideBarMenu> MakeParentChildRelations(List<VMSideBarMenu> children, List<VMSideBarMenu> allVMSideBarMenuItems)
		{
			foreach (var child in children)
			{
				if (allVMSideBarMenuItems.Any(x => x.ParentId == child.Id))
				{
					var childsChildren = allVMSideBarMenuItems.Where(x => x.ParentId == child.Id).ToList();
					child.Children.AddRange(MakeParentChildRelations(childsChildren, allVMSideBarMenuItems));
				}
			}
			return children;
		}
	}
}
