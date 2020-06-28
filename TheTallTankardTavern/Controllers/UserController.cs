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
	[Authorized(Constants.ROLES.Administrator)]
	public class UserController : BaseController<UserModel>
	{
		public UserController() : base(UserDataContext, FOLDER.User) { }

		public override IActionResult Create()
		{
			return View("Create", new UserModel
			{
				ID = NewGUID
			});
		}

		[HttpPost]
		public IActionResult Save(UserModel Model, string submit)
		{
			return SaveModel(Model, submit);
		}

		#region User Profile Editing
		[Authorized(Constants.ROLES.Player)]
		public IActionResult Profile()
		{
			return View("Profile", ApplicationSettings.UserDataContext.GetModelFromID(ContextUser.GetContextUser.ID));
		}

		[Authorized(Constants.ROLES.Player)]
		public JsonResult SaveChanges(string uid, string username, string message)
		{
			try
			{
				UserModel User = ApplicationSettings.UserDataContext.GetModelFromID(uid);
				if (User.Username != username)
				{
					foreach (CharacterModel Character in ApplicationSettings.CharacterDataContext.Where((CharacterModel u) => u.Player_Name == User.Username))
					{
						Character.Player_Name = username;
						ApplicationSettings.CharacterDataContext.Save(Character, FOLDER.Characters);
					}
					User.Username = username;
					ContextUser.SetContextUser(User);
				}
				if (User.Message != message)
				{
					User.Message = message;
					User.MessageUpdated = DateTime.Now.ToString("ddd, MMM dd yyyy");
				}
				ApplicationSettings.UserDataContext.Save(User, FOLDER.User);
				return Json(new
				{
					success = true
				});
			}
			catch
			{
				return Json(new
				{
					success = false
				});
			}
		}
        #endregion
    }
}
