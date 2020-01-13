using System.Collections.Generic;
using TheTallTankardTavern.Helpers;
using static TheTallTankardTavern.Configuration.Constants;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using Newtonsoft.Json;

namespace TheTallTankardTavern.Models
{
	public class EquipmentModel : BaseListModel<string>
	{
		[JsonProperty]
		public int Weapon_Slots_Used { get; set; } = 0;

		public int Weapon_Slots_Max { get; set; } = 11;


		public bool TryEquip(string ItemID, ref string errorMessage)
		{
			return TryEquip(ItemDataContext.GetModelFromID(ItemID), ref errorMessage);
		}

		public bool TryEquip(ItemModel Item, ref string errorMessage)
		{
			switch (Item.Item_Type)
			{
			case "Armour":
				return TryEquip(Item, 1, ref errorMessage);
			case "Bracers":
				return TryEquip(Item, 1, ref errorMessage);
			case "Footwear":
				return TryEquip(Item, 1, ref errorMessage);
			case "Headgear":
				return TryEquip(Item, 1, ref errorMessage);
			case "Necklace":
				return TryEquip(Item, 1, ref errorMessage);
			case "Ring":
				return TryEquip(Item, 10, ref errorMessage);
			case "Weapon":
				return TryEquipWeapon(Item, Weapon_Slots_Max, ref errorMessage);
			default:
				errorMessage = "Unknown error occured while trying to equip item";
				return false;
			}
		}

		public void Unequip(string ItemID)
		{
			ItemModel Item = ItemDataContext.GetModelFromID(ItemID);
			if (Item.Item_Type == "Weapon")
			{
				int removedWeaponSize = GetNumericWeaponSize(Item);
				Weapon_Slots_Used -= ((Weapon_Slots_Used - removedWeaponSize >= 0) ? removedWeaponSize : 0);
			}
			InnerCollection.Remove(ItemID);
		}

		public ItemModel GetEquipmentFromID(string ItemID)
		{
			return ItemDataContext.GetModelFromID(ItemID);
		}

		private bool TryEquip(ItemModel Item, int quantityAllowed, ref string errorMessage)
		{
			if (InnerCollection.FindAll((string itemid) => ItemDataContext.GetModelFromID(itemid).Item_Type.Equals(Item.Item_Type)).Count >= quantityAllowed)
			{
				errorMessage = $"The max quantity of {Item.Item_Type} items has already be reached ({quantityAllowed}).";
				return false;
			}
			return Equip(Item);
		}

		private bool TryEquipWeapon(ItemModel Weapon, int weaponSlotsAllowed, ref string errorMessage)
		{
			IEnumerable<string> Weapons = InnerCollection.FindAll((string itemid) => ItemDataContext.GetModelFromID(itemid).Item_Type.Equals(Weapon.Item_Type));
			int newWeaponSize = GetNumericWeaponSize(Weapon);
			if (Weapon_Slots_Used + newWeaponSize >= weaponSlotsAllowed)
			{
				errorMessage = "Cannot equip Weapon because the maximum number of weapon slots will be exceededLarge, Medium, and Small weapons use 3, 2, and 1 slots respectively";
				return false;
			}
			Weapon_Slots_Used += newWeaponSize;
			return Equip(Weapon);
		}

		private bool Equip(ItemModel Item)
		{
			InnerCollection.Add(Item.ID);
			return true;
		}

		private int GetNumericWeaponSize(ItemModel Weapon)
		{
			switch (Weapon.Size)
			{
				case WEAPON_SIZE.LARGE: return 3;
				case WEAPON_SIZE.MEDIUM: return 2;
				case WEAPON_SIZE.SMALL: return 1;
				default: return -1;
			}
		}
	}
}
