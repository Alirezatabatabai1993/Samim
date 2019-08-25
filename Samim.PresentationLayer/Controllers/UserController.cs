using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samim.DataLayer.AppUser;
using Samim.DataLayer.Context;
using Samim.DataLayer.UnitOfWork.UserRepo;
using Samim.ViewModel;

namespace Samim.PresentationLayer.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserRepository _userRepository;
		public UserController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		public IActionResult Index()
		{
			var users = _userRepository.GetApplicationUsers();
			return View(new VMUser(users));
		}
		//public IActionResult Create()
		//{

		//}
	}
}