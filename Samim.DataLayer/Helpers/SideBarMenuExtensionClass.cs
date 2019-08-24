using Samim.DomainLayer;
using Samim.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samim.DataLayer.Helpers
{
	internal static class SideBarMenuExtensionClass
	{
		public static List<VMSideBarMenu> CreateChildren(this IEnumerable<SideBarMenu> SideBarMenuItems)
		{
			var returningMenuItems = new List<VMSideBarMenu>();
			foreach (var menuItem in SideBarMenuItems.Where(x => x.ParentId == null))
			{
				var sideBarMenuItem = new VMSideBarMenu(menuItem);
				if (!sideBarMenuItem.HasParent)
				{
					if (SideBarMenuItems.Any(x => x.ParentId == sideBarMenuItem.Id))
					{
						var children = SideBarMenuItems.Where(x => x.ParentId == sideBarMenuItem.Id).Select(x => new VMSideBarMenu(x)).ToList();
						var sideBarItems = SideBarMenuItems.Select(x => new VMSideBarMenu(x)).ToList();
						sideBarMenuItem.Children.AddRange(MakeParentChildRelations(children, sideBarItems));
					}
					returningMenuItems.Add(sideBarMenuItem);
				}
			}
			return returningMenuItems;
		}

		private static List<VMSideBarMenu> MakeParentChildRelations(List<VMSideBarMenu> children, List<VMSideBarMenu> allVMSideBarMenuItems)
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
