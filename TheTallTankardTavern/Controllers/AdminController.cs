using Microsoft.AspNetCore.Mvc;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Helpers;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.ApplicationSettings;

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
			ViewData["Message"] = $"Configuration and Data reloaded at {System.DateTime.Now}";
			return View("Index");
		}

		[HttpPost]
		public IActionResult UnequipAllItems(string cid)
        {
			CharacterModel Character = CharacterDataContext.GetModelFromID(cid);
			Character.Equipment.Clear();
			CharacterDataContext.Save(Character, Constants.FOLDER.Characters);

			ViewData["Message"] = $"{Character.Name} had all of their equipment unequipped at {System.DateTime.Now}";
			return View("Index");
		}

		[HttpPost]
		public IActionResult RemoveAllItems(string cid)
		{
			CharacterModel Character = CharacterDataContext.GetModelFromID(cid);
			Character.Equipment.Clear();
			Character.Inventory.Clear();
			CharacterDataContext.Save(Character, Constants.FOLDER.Characters);

			ViewData["Message"] = $"{Character.Name} had all of their items removed at {System.DateTime.Now}";
			return View("Index");
		}
	}
}
