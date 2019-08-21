using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samim.DomainLayer;

namespace Samim.ViewModel
{
	public class VMSideBarMenu
	{
		public VMSideBarMenu()
		{

		}
		public VMSideBarMenu(SideBarMenu dBSideBarMenu)
		{
			Id = dBSideBarMenu.Id;
			Title = dBSideBarMenu.Title;
			HasParent = dBSideBarMenu.ParentId != null;
		}

		public Guid Id { get; set; }
		public string Title { get; set; }
		public List<VMSideBarMenu> Children { get; set; } = new List<VMSideBarMenu>();
		public bool HasChildren => Children.Any();
		public bool HasParent { get; set; }
	}
}
