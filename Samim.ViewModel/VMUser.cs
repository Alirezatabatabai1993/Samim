using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Samim.DataLayer.AppUser;
using System.ComponentModel.DataAnnotations;

namespace Samim.ViewModel
{
	public class VMUser
	{
		public VMUser()
		{

		}
		public VMUser(List<ApplicationUser> vMUserIndex)
		{
			Users = vMUserIndex.Select(x => new VMUserIndex(x)).ToList();
		}
		public List<VMUserIndex> Users { get; set; } = new List<VMUserIndex>();
		public VMUserCreateAndEdit UserCreateAndEdit { get; set; } = new VMUserCreateAndEdit();
	}
	public class VMUserIndex
	{
		public VMUserIndex()
		{

		}
		public VMUserIndex(ApplicationUser identityUser)
		{
			FirstName = identityUser.FirstName;
			LastName = identityUser.LastName;
			UserName = identityUser.UserName;
			Email = identityUser.Email;
			PhoneNumber = identityUser.PhoneNumber;
		}
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
	}
	public class VMUserCreateAndEdit
	{
		public Guid? Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
	}
}
