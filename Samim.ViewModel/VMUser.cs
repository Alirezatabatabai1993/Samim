using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Samim.DataLayer.AppUser;
using System.ComponentModel.DataAnnotations;

namespace Samim.ViewModel
{
	public class VMUserIndex
	{
		public VMUserIndex()
		{

		}
		public VMUserIndex(ApplicationUser applicationUser)
		{
			Id = applicationUser.Id;
			FirstName = applicationUser.FirstName;
			LastName = applicationUser.LastName;
			UserName = applicationUser.UserName;
			Email = applicationUser.Email;
			PhoneNumber = applicationUser.PhoneNumber;
		}
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
	}

	public class VMUserCreateAndEdit
	{
		public VMUserCreateAndEdit()
		{

		}
		public VMUserCreateAndEdit(ApplicationUser applicationUser)
		{
			Id = applicationUser.Id;
			FirstName = applicationUser.FirstName;
			LastName = applicationUser.LastName;
			UserName = applicationUser.UserName;
			Email = applicationUser.Email;
			PhoneNumber = applicationUser.PhoneNumber;
		}
		public string Id { get; set; }
		[Required]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Required]
		[Display(Name = "User Name")]
		public string UserName { get; set; }
		//[Required]
		public string Password { get; set; }
		public string Email { get; set; }
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
	}

	public class VMUserEditPassword
	{
		public VMUserEditPassword()
		{

		}
		public VMUserEditPassword(ApplicationUser applicationUser)
		{
			UserName = applicationUser.UserName;
		}
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
