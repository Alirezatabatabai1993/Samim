using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Samim.DomainLayer
{
	public class SideBarMenu
	{
		[Required]
		public Guid Id { get; set; }
		public Guid? ParentId { get; set; }
		[Required]
		public string Title { get; set; }
	}
}
