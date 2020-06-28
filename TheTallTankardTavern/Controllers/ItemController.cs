using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Models;
using TTT.Common;
using TTT.Items;
using TTT.Items.Weapons.Damage;
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
		public IActionResult Save(ItemModel Item, string submit,
			Die die1, Die die2, Die die3, DamageType type1, DamageType type2, DamageType type3)
		{
			Item.Weapon.Damage.Add(new Damage(die1, type1));
			Item.Weapon.Damage.Add(new Damage(die2, type2));
			Item.Weapon.Damage.Add(new Damage(die3, type3));

			return SaveModel(Item, submit);
		}

		//[HttpPost]
		//public override IActionResult Save(ItemModel NewItem, string submit)
		//{
		//	ExtendedItemModel newItem = NewItem as ExtendedItemModel;
		//	if (newItem != null)
		//	{
		//		ItemModel Item = new ItemModel()
		//		{
		//			ID = newItem.ID,
		//			Name = newItem.Name,
		//			Type = newItem.Type,
		//			Description = newItem.Description,
		//			Cost = newItem.Cost,
		//			Weight = newItem.Weight,
		//			IsHomebrew = newItem.IsHomebrew,
		//			IsHidden = newItem.IsHidden,
		//			IsMagic = newItem.IsMagic,
		//			Armour = newItem.Armour
		//		};
		//		Item.Weapon.Plus = newItem.Weapon.Plus;
		//		Item.Weapon.Damage.Add(new Damage(newItem.Die1, newItem.Type1));
		//		Item.Weapon.Damage.Add(new Damage(newItem.Die2, newItem.Type2));
		//		Item.Weapon.Damage.Add(new Damage(newItem.Die3, newItem.Type3));

		//		return base.Save(Item, submit);
		//	}
		//	return base.Save(NewItem, submit);
		//}

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
			return item.Name.SafeContains(searchtext) || item.Type.ToString().SafeContains(searchtext);
		}

		private static bool SafeContains(this string searchProperty, string searchtext)
		{
			return string.IsNullOrEmpty(searchProperty) ? false : searchProperty.ToLower().Contains(searchtext);
		}
	}
}
