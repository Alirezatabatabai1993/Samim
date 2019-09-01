using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samim.DataLayer.AppUser;
using Samim.DataLayer.Context;
using Samim.DataLayer.Helpers;
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
			var users = _userRepository.GetAllApplicationUsers();
			return View(users.Select(x=>new VMUserIndex(x)));
		}

		[HttpGet]
		public IActionResult Create()
		{
			return PartialView("_CreateAndEdit", new VMUserCreateAndEdit());
		}

		[HttpPost]
		public IActionResult Create(VMUserCreateAndEdit vMUserCreate)
		{
			if (string.IsNullOrEmpty(vMUserCreate.Password))
				ModelState.AddModelError("Password","The password field is reuired.");
			if (ModelState.IsValid)
			{
				_userRepository.AddApplicationUser(vMUserCreate);
				return Json(new { success = true });
			}
			return Json(new { success = false, errors = ModelState.PopulateErrors() });
		}

		[HttpGet]
		public IActionResult EditPassword(string id)
		{
			var applicationUser = _userRepository.GetApplicationUserById(id);
			return PartialView("_EditPassword", new VMUserEditPassword(applicationUser));
		}

		[HttpGet]
		public IActionResult Edit(string id)
		{
			var applicationUser = _userRepository.GetApplicationUserById(id);
			var applicationUserToVMUserCreateAndEdit = new VMUserCreateAndEdit(applicationUser);
			return PartialView("_CreateAndEdit", applicationUserToVMUserCreateAndEdit);
		}

		[HttpPost]
		public IActionResult Edit(VMUserCreateAndEdit vMUserEdit)
		{
			var d = ModelState.PopulateErrors();
			if (ModelState.IsValid)
			{
				_userRepository.EditApplicationUser(vMUserEdit);
				return Json(new { success = true });
			}
			
			return Json(new { success = false, errors = ModelState.PopulateErrors() });
		}

		[HttpPost]
		public IActionResult Delete(string id)
		{
			_userRepository.DeleteApplicationUser(id);
			return Json(new { success = true });
		}
	}
}