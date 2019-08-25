using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Samim.DataLayer.AppUser;
using Samim.PresentationLayer.Models;
using Samim.ViewModel;

namespace Samim.PresentationLayer.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Login(VMLogin userModel)
		{
			if (ModelState.IsValid)
			{
				var result = _signInManager.PasswordSignInAsync(userModel.UserName, userModel.Password, false, false).GetAwaiter().GetResult();
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError(string.Empty, "User Name or Password is incorrect.");
			}
			return View(nameof(Login),userModel);
		}
	}
}