using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Samim.DataLayer.AppUser;
using Samim.DataLayer.Context;
using Samim.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samim.DataLayer.UnitOfWork.UserRepo
{
	public interface IUserRepository : IBaseRepository<ApplicationUser>
	{
		List<ApplicationUser> GetApplicationUsers();
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

		public List<ApplicationUser> GetApplicationUsers()
		{
			return _userManager.Users.ToListAsync().GetAwaiter().GetResult();
		}
	}
}
