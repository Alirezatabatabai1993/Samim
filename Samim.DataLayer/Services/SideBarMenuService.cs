using System;
using System.Collections.Generic;
using System.Text;
using Samim.ViewModel;
using Samim.DomainLayer;
using Samim.DataLayer.Context;
using System.Linq;

namespace Samim.BusinessLayer.Services
{
	public interface ISideBarMenuService
	{
	}

	public class SideBarMenuService : ISideBarMenuService
	{
		private readonly SamimDbContext _db;

		public SideBarMenuService(SamimDbContext db)
		{
			_db = db;
		}
		public List<VMSideBarMenu> vMSideBarMenuItems()
		{
			var dbSideBarMenuItems = DBSideBarMenuItems();
			var vmSideBarMenuItems = dbSideBarMenuItems.Select(x => new VMSideBarMenu(x)).ToList();
			foreach (var vmSideBarMenuItem in vmSideBarMenuItems)
			{
				if (vmSideBarMenuItem.HasChildren)
				{
					MakeParentChildRelations(vmSideBarMenuItem.Children, vmSideBarMenuItems);
				}
			}
			return vmSideBarMenuItems.Where(x => !x.HasParent).ToList();
		}
		private List<VMSideBarMenu> MakeParentChildRelations(List<VMSideBarMenu> children, List<VMSideBarMenu> allVMSideBarMenuItems)
		{
			foreach (var item in children)
			{
				if (item.HasChildren)
				{
					item.Children.AddRange(MakeParentChildRelations(item.Children, allVMSideBarMenuItems));
				}
			}
		}
		private List<SideBarMenu> DBSideBarMenuItems()
		{
			return _db.SideBarMenu.ToList();
		}
	}
}
