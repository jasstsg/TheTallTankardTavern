using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Helpers;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Controllers
{
	public class UserController : BaseController<UserModel>
	{
		public UserController() : base(UserDataContext, FOLDER.User) { }

		[Authorized(ROLES.Administrator)]
		public override IActionResult Create()
		{
			return View("Create", new UserModel { ID = NewGUID });
		}

		[HttpPost]
		public IActionResult Save(UserModel Model, string submit)
		{
			if (ContextUser.Current.ID.Equals(Model.ID))
            {
				ContextUser.Set(Model);
            }
			return SaveModel(Model, submit);
		}

		#region User Profile Editing
		[Authorized(ROLES.Dungeon_Master, ROLES.Dungeon_Master_Readonly, ROLES.Player)]
		public IActionResult Profile()
		{			
			return View(ContextUser.IsAdministrator ? "Create" : "Profile", ContextUser.Current);
		}

		[Authorized(ROLES.Dungeon_Master, ROLES.Dungeon_Master_Readonly, ROLES.Player)]
		public JsonResult SaveChanges(string uid, string username, string message)
		{
			try
			{
				UserModel User = UserDataContext.GetModelFromID(uid);
				if (User.Username != username)
				{
					foreach (CharacterModel Character in CharacterDataContext.Where((CharacterModel u) => u.Player_Name == User.Username))
					{
						Character.Player_Name = username;
						CharacterDataContext.Save(Character, FOLDER.Characters);
					}
					User.Username = username;
					ContextUser.Set(User);
				}
				if (User.Message != message)
				{
					User.Message = message;
					User.MessageUpdated = DateTime.Now.ToString("ddd, MMM dd yyyy");
				}
				UserDataContext.Save(User, FOLDER.User);
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}
		}
        #endregion
    }
}
