using Microsoft.AspNetCore.Mvc;

namespace TheTallTankardTavern.Controllers
{
    public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AccessDenied()
		{
			return View("AccessDenied");
		}
	}
}
