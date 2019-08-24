using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Samim.PresentationLayer.Models;

namespace Samim.PresentationLayer.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index(string filter)
		{
			return View();
		}
		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Filter(string filter)
		{
			return RedirectToAction(nameof(Index), new { filter });
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
