using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TheTallTankardTavern.Attributes;
using TTT.Items;
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
            ItemModel Item = new ItemModel()
            {
                ID = NewGUID,
			};

            return View("Create", Item);
        }

		[HttpPost]
		public IActionResult Save(ItemModel Item, string submit)
		{
			return SaveModel(Item, submit);
		}

		[HttpPost]
		public IActionResult FilteredIndex(string searchtext)
		{
			IEnumerable<ItemModel> Items = !string.IsNullOrEmpty(searchtext) ?
				DataContext.Where(i => i.IsMatch(searchtext)).ToList() :
				Items = DataContext.Where(i => true);

			ViewData["searchtext"] = searchtext;

			return View("Index", Items.OrderBy(i => i.Name).ToList());
		}
	}

	public static class ItemControllerExtensions
	{
		public static bool IsMatch(this ItemModel item, string searchtext)
		{
			searchtext = searchtext.ToLower();
			return item.Name.SafeContains(searchtext) || item.Type.ToString().SafeContains(searchtext) ||
				item.Type.Category.ToString().SafeContains(searchtext);
		}

		private static bool SafeContains(this string searchProperty, string searchtext)
		{
			return string.IsNullOrEmpty(searchProperty) ? false : searchProperty.ToLower().Contains(searchtext);
		}
	}
}
