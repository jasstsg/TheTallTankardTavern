using System.Collections.Generic;
using TheTallTankardTavern.Helpers;
using static TheTallTankardTavern.Configuration.Constants;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using Newtonsoft.Json;
using System.Linq;

namespace TheTallTankardTavern.Models
{
	public class EquipmentModel : BaseListModel<string>
	{

		[JsonProperty]
		public int Weapon_Slots_Used
		{
			get
			{
				int _slots = 0;
				foreach (string id in InnerCollection.Where(x => ItemDataContext.GetModelFromID(x).Item_Type.Equals(ITEM_TYPES.WEAPON)))
				{
					_slots += GetNumericWeaponSize(ItemDataContext.GetModelFromID(id));
				}
				return _slots;
			}
		}

		public int Weapon_Slots_Max { get; set; } = 11;


		public EquipResult TryEquip(string ItemID)
		{
			return TryEquip(ItemDataContext.GetModelFromID(ItemID));
		}

		public EquipResult TryEquip(ItemModel Item)
		{
			switch (Item.Item_Type)
			{
				case ITEM_TYPES.ARMOUR: return TryEquipOrReplace(Item);
				case ITEM_TYPES.BRACERS: return TryEquipOrReplace(Item);
				case ITEM_TYPES.FOOTWEAR: return TryEquipOrReplace(Item);
				case ITEM_TYPES.HEADGEAR: return TryEquipOrReplace(Item);
				case ITEM_TYPES.NECKLACE: return TryEquipOrReplace(Item);
				case ITEM_TYPES.RING: return TryEquip(Item, 10);
				case ITEM_TYPES.WEAPON: return TryEquipWeapon(Item, Weapon_Slots_Max);
				default: return new EquipResult();
			}
		}

		public void Unequip(string ItemID)
		{
			InnerCollection.Remove(ItemID);
		}

		public ItemModel GetEquipmentFromID(string ItemID)
		{
			return ItemDataContext.GetModelFromID(ItemID);
		}

		private EquipResult TryEquipOrReplace(ItemModel Item)
		{
			//Check if there is an item with the same item type already equipped
			string itemIdToReplace = InnerCollection.Find(id => ItemDataContext.GetModelFromID(id).Item_Type.Equals(Item.Item_Type));

			//Equip the new item
			EquipResult FinalResult = Equip(Item);

			//If there was an item of the same type we will want to replace it, so unequip it and pass it back up
			if (string.IsNullOrEmpty(itemIdToReplace))
			{
				Unequip(itemIdToReplace);
				FinalResult.Result = ITEM_EQUIP_RESULT.REPLACED;
				FinalResult.ReplacedItemId = itemIdToReplace;
			}

			return FinalResult;
		}

		private EquipResult TryEquip(ItemModel Item, int quantityAllowed)
		{
			//If there is room available equip the item, otherwise return false
			if (InnerCollection.FindAll(itemid => ItemDataContext.GetModelFromID(itemid).Item_Type.Equals(Item.Item_Type)).Count >= quantityAllowed)
			{
				return new EquipResult();
			}
			return Equip(Item);
		}

		private EquipResult TryEquipWeapon(ItemModel Weapon, int weaponSlotsAllowed)
		{
			IEnumerable<string> Weapons = InnerCollection.FindAll((string itemid) => ItemDataContext.GetModelFromID(itemid).Item_Type.Equals(Weapon.Item_Type));
			int newWeaponSize = GetNumericWeaponSize(Weapon);
			if (Weapon_Slots_Used + newWeaponSize >= weaponSlotsAllowed)
			{
				return new EquipResult();
			}
			return Equip(Weapon);
		}

		private EquipResult Equip(ItemModel Item)
		{
			InnerCollection.Add(Item.ID);
			return new EquipResult()
			{
				Result = ITEM_EQUIP_RESULT.EQUIPPED
			};
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

	public class EquipResult
	{
		public ITEM_EQUIP_RESULT Result { get; set; } = ITEM_EQUIP_RESULT.NO_ACTION;
		public string ReplacedItemId { get; set; } = "";
	}
}