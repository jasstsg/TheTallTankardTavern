using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Enumerators;
using TheTallTankardTavern.Helpers;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Controllers
{
	[Authorized(ROLES.Dungeon_Master, ROLES.Dungeon_Master_Readonly)]
	public class ItemController : BaseController<ItemModel>
	{
        public ItemController() : base(ItemDataContext, FOLDER.Items) { }

		public override IActionResult Create()
		{
            ItemModel Item = new ItemModel
            {
                ID = NewGUID,
				Traits = CheckBoxListModel<WEAPON_TRAIT>.Empty(),
				Damage_Types = CheckBoxListModel<DAMAGE_TYPE>.Empty()
			};

            return View("Create", Item);
        }
	}
}
