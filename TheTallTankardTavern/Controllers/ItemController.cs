using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TheTallTankardTavern.Attributes;
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

		[HttpPost]
		public IActionResult FilteredIndex(string searchtext)
		{
			IEnumerable<ItemModel> Spells = !string.IsNullOrEmpty(searchtext) ?
				DataContext.Where(i => i.IsMatch(searchtext)).ToList() :
				Spells = DataContext.Where(i => true);

			ViewData["searchtext"] = searchtext;

			return View("Index", Spells.OrderBy(i => i.Name).ToList());
		}
	}

	public static class ItemControllerExtensions
	{
		public static bool IsMatch(this ItemModel item, string searchtext)
		{
			searchtext = searchtext.ToLower();

			return item.Name.SafeContains(searchtext) ||
				item.Material_Name.SafeContains(searchtext) ||
				item.Type.SafeContains(searchtext) ||
				item.Damage_Types.Any(d => d.ToString().SafeContains(searchtext));
		}

		private static bool SafeContains(this string searchProperty, string searchtext)
		{
			return string.IsNullOrEmpty(searchProperty) ? false : searchProperty.ToLower().Contains(searchtext);
		}
	}
}
