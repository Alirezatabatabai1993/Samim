using System;
using System.Collections.Generic;
using System.Text;
using Samim.ViewModel;
using Samim.DomainLayer;
using Samim.DataLayer.Context;
using System.Linq;
using Samim.DataLayer.UnitOfWork;
using Samim.DataLayer.UnitOfWork.SideBarMenuRepo;
using Samim.DataLayer.Helpers;

namespace Samim.BusinessLayer.Services
{
	public interface ISideBarMenuService
	{
		List<VMSideBarMenu> GetVMSideBarMenuItems();
		List<VMSideBarMenu> GetVMSideBarMenuItemsByFilter(string filter);
	}

	public class SideBarMenuService : ISideBarMenuService
	{
		private readonly ISideBarMenuRepository _sideBarMenuRepository;

		public SideBarMenuService(ISideBarMenuRepository sideBarMenuRepository)
		{
			_sideBarMenuRepository = sideBarMenuRepository;
		}

		public List<VMSideBarMenu> GetVMSideBarMenuItems()
		{
			return _sideBarMenuRepository.GetAll().GetAwaiter().GetResult().CreateChildren();
		}

		public List<VMSideBarMenu> GetVMSideBarMenuItemsByFilter(string filter)
		{
			var menuItemsWithParentChildRealation = _sideBarMenuRepository.GetAll().GetAwaiter().GetResult().CreateChildren().ToList();
			var flattenMenuItems = menuItemsWithParentChildRealation.Flatten(x => x.Children).ToList();
			var SearchOnFlattenMenuItems = flattenMenuItems.Where(x => x.Title.ToLower().Contains(filter.ToLower())).ToList();
			var searchedMenuItemsThatAreNotFolders = SearchOnFlattenMenuItems.Where(x => !x.HasChildren).ToList();
			return searchedMenuItemsThatAreNotFolders;
		}
	}
}
