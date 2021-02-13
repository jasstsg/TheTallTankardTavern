using Microsoft.AspNetCore.Mvc;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Models;

namespace TheTallTankardTavern.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View("Index");
		}

        [HttpPost]
		public IActionResult Login(UserModel User)
		{
			User = ApplicationSettings.UserDataContext.Find(u => u.Username.Equals(User?.Username, System.StringComparison.OrdinalIgnoreCase));
			if (User == null)
			{
				ViewData["Message"] = "That username does not exist.  Please make sure you typed your username correctly or ask the administrator to create a user for you.";
				return View("Index");
			}
			ContextUser.Set(User);
			ContextUser.SetAuthentication(isAuthenticated: true);
			return RedirectToAction("Index", "Home");
		}

		public IActionResult LogOut()
		{
			ContextUser.RemoveContextUser();
			ContextUser.SetAuthentication(isAuthenticated: false);
			return RedirectToAction("Index", "Login");
		}
	}
}
