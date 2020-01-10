using Microsoft.AspNetCore.Mvc;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;


namespace TheTallTankardTavern.Controllers
{
	[Authorized(ROLES.Dungeon_Master, ROLES.Dungeon_Master_Readonly)]
	public class MaterialController : BaseController<MaterialModel>
	{
        public MaterialController() : base(MaterialDataContext, FOLDER.Materials) { }

		public override IActionResult Create()
		{
            MaterialModel Material = new MaterialModel()
            {
                ID = NewGUID
            };
            return View("Create", Material);
		}
	}
}
