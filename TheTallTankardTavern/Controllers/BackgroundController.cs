using Microsoft.AspNetCore.Mvc;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Helpers;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Controllers
{
	[Authenticated]
	public class BackgroundController : BaseController<BackgroundModel>
	{
        public BackgroundController() : base(BackgroundDataContext, FOLDER.Backgrounds) { }

        public override IActionResult Create()
		{
            BackgroundModel Background = new BackgroundModel
            {
                ID = NewGUID,
            };
            return View("Create", Background);
        }
	}
}
