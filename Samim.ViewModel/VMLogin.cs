using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Samim.ViewModel
{
	public class VMLogin
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
