using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Samim.DataLayer.AppUser;
using Samim.DataLayer.Context;
using Samim.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Samim.DataLayer.UnitOfWork.UserRepo
{
	public interface IUserRepository : IBaseRepository<ApplicationUser>
	{
		List<ApplicationUser> GetAllApplicationUsers();
		bool AddApplicationUser(VMUserCreateAndEdit vMUserCreate);
		bool DeleteApplicationUser(string id);
		ApplicationUser GetApplicationUserById(string id);
		bool EditApplicationUser(VMUserCreateAndEdit vMUserEdit);
	}
	public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserRepository(SamimDbContext context) : base(context)
		{
		}
		public UserRepository(SamimDbContext context, UserManager<ApplicationUser> userManager) : base(context)
		{
			_userManager = userManager;
		}

		public List<ApplicationUser> GetAllApplicationUsers()
		{
			return _userManager.Users.AsNoTracking().ToListAsync().GetAwaiter().GetResult();
		}

		public bool AddApplicationUser(VMUserCreateAndEdit vMUserCreate)
		{
			try
			{
				_userManager.CreateAsync(
				new ApplicationUser()
				{
					FirstName = vMUserCreate.FirstName,
					LastName = vMUserCreate.LastName,
					UserName = vMUserCreate.UserName,
					Email = vMUserCreate.Email,
					PhoneNumber = vMUserCreate.PhoneNumber
				},
				vMUserCreate.Password).GetAwaiter().GetResult();
				return true;
			}
			catch
			{
				return false;
			}

		}

		public ApplicationUser GetApplicationUserById(string id)
		{
			return _userManager.FindByIdAsync(id).GetAwaiter().GetResult();
		}

		public bool EditApplicationUser(VMUserCreateAndEdit vMUserEdit)
		{
			try
			{
				var applicationUser = GetApplicationUserById(vMUserEdit.Id);
				applicationUser.FirstName = vMUserEdit.FirstName;
				applicationUser.LastName = vMUserEdit.LastName;
				applicationUser.UserName = vMUserEdit.UserName;
				applicationUser.Email = vMUserEdit.Email;
				applicationUser.PhoneNumber = vMUserEdit.PhoneNumber;
				_userManager.UpdateAsync(applicationUser).GetAwaiter().GetResult();

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool DeleteApplicationUser(string id)
		{
			try
			{
				var applicationUser = GetApplicationUserById(id);
				_userManager.DeleteAsync(applicationUser).GetAwaiter().GetResult();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
