using Microsoft.AspNetCore.Mvc;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Configuration;

namespace TheTallTankardTavern.Controllers
{
	[Authorized(Constants.ROLES.Administrator)]
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult ReloadConfigsAndData()
		{
			ApplicationSettings.ReloadConfiguration();
			ApplicationSettings.ReloadDataContext();
			return View("Index");
		}
	}
}
