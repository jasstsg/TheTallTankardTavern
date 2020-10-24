using static TheTallTankardTavern.Configuration.ApplicationSettings;
using Newtonsoft.Json;
using System.Linq;
using TTT.Items;
using TTT.Common.Abstractions;
using TheTallTankardTavern.Helpers;

namespace TheTallTankardTavern.Models
{
    public class EquipmentModel : BaseListModel<string>
    {
        [JsonProperty]
        public string _spellCastingFocus { get; set; } = null;
        [JsonProperty]
        public string _armour { get; set; } = null;
        [JsonProperty]
        public string _shield { get; set; } = null;
        [JsonProperty]
        public string _twoHand { get; set; } = null;
        [JsonProperty]
        public string _mainHand { get; set; } = null;
        [JsonProperty]
        public string _offHand { get; set; } = null;
        [JsonProperty]
        public string _attunedItem1 { get; set; } = null;
        [JsonProperty]
        public string _attunedItem2 { get; set; } = null;
        [JsonProperty]
        public string _attunedItem3 { get; set; } = null;

        public override void Clear()
        {
            if (Armour != null) { UnequipArmour(Armour.InventoryID); }
            if (MainHand != null) { UnequipMainHand(MainHand.InventoryID); }
            if (OffHand != null) { UnequipOffHand(OffHand.InventoryID); }
            if (Shield != null) { UnequipShield(Shield.InventoryID); }
            if (SpellCastingFocus != null) { UnequipSpellcastingFocus(SpellCastingFocus.InventoryID); }
            if (TwoHand != null) { UnequipTwoHand(TwoHand.InventoryID); }
            if (AttunedItem1 != null) { UnequipMagicItem(AttunedItem1.InventoryID); }
            if (AttunedItem2 != null) { UnequipMagicItem(AttunedItem2.InventoryID); }
            if (AttunedItem3 != null) { UnequipMagicItem(AttunedItem3.InventoryID); }
            base.Clear();
        }

        private string EquipmentSetter(string _innerEquipmentVar, ItemModel value)
        {
            if (!string.IsNullOrEmpty(_innerEquipmentVar ))
            {
                Remove(_innerEquipmentVar);
            }
            if (value != null)
            {
                Add(value.InventoryID);
            }
            return value?.InventoryID;
        }

        private ItemModel EquipmentGetter(string _innerEquipmentVar)
        {
            if (string.IsNullOrEmpty(_innerEquipmentVar))
            {
                return null;
            }
            ItemModel Item = ItemDataContext.GetModelFromID(_innerEquipmentVar.Substring(0, _innerEquipmentVar.IndexOf("+"))).Clone();
            Item.InstanceID = _innerEquipmentVar.Substring(_innerEquipmentVar.IndexOf("+") + 1);
            return Item;
        }

        [JsonIgnore]
        public ItemModel SpellCastingFocus
        {
            get { return EquipmentGetter(_spellCastingFocus); }
            set { _spellCastingFocus = EquipmentSetter(_spellCastingFocus, value); }
        }
        [JsonIgnore]
        public ItemModel Armour
        {
            get { return EquipmentGetter(_armour); }
            set { _armour = EquipmentSetter(_armour, value); }
        }
        [JsonIgnore]
        public ItemModel Shield
        {
            get { return EquipmentGetter(_shield); }
            set { _shield = EquipmentSetter(_shield, value); }
        }
        [JsonIgnore]
        public ItemModel TwoHand
        {
            get { return EquipmentGetter(_twoHand); }
            set { _twoHand = EquipmentSetter(_twoHand, value); }
        }
        [JsonIgnore]
        public ItemModel MainHand
        {
            get { return EquipmentGetter(_mainHand); }
            set { _mainHand = EquipmentSetter(_mainHand, value); }
        }
        public ItemModel OffHand
        {
            get { return EquipmentGetter(_offHand); }
            set { _offHand = EquipmentSetter(_offHand, value); }
        }
        public ItemModel AttunedItem1
        {
            get { return EquipmentGetter(_attunedItem1); }
            set { _attunedItem1 = EquipmentSetter(_attunedItem1, value); }
        }
        public ItemModel AttunedItem2        {
            get { return EquipmentGetter(_attunedItem2); }
            set { _attunedItem2 = EquipmentSetter(_attunedItem2, value); }
        }
        public ItemModel AttunedItem3
        {
            get { return EquipmentGetter(_attunedItem3); }
            set { _attunedItem3 = EquipmentSetter(_attunedItem3, value); }
        }

        public bool TryEquip(string inventoryID, InventoryModel Inventory, bool isDualWielder)
        {
            if (!Inventory.Contains(inventoryID))
            {
                return false;
            }
            ItemModel Item = ItemDataContext.FirstOrDefault(
                item => item.ID.Equals(inventoryID.Substring(0, inventoryID.IndexOf("+")))).Clone();
            Item.InventoryID = inventoryID;

            switch (Item.Type.Category)
            {
                case ItemTypeCategory.SpellCastingFocus: return EquipSpellCastingFocus(Item);
                case ItemTypeCategory.Armour: return EquipArmour(Item);
                case ItemTypeCategory.Shield: return EquipShield(Item);
                case ItemTypeCategory.Weapon: return EquipWeapon(Item, isDualWielder);
                case ItemTypeCategory.Equippable: return EquipMagicItem(Item);
                default: return false;
            }
        }

        public bool EquipSpellCastingFocus(ItemModel newSpellCastingFocus)
        {
            if (MainHand != null)
            {
                Shield = null;
            }
            TwoHand = null;
            OffHand = null;
            SpellCastingFocus = newSpellCastingFocus;
            return true;
        }

        public bool EquipArmour(ItemModel newArmour)
        {
            Armour = newArmour;
            return true;
        }

        public bool EquipShield(ItemModel newShield)
        {
            if (MainHand != null)
            {
                SpellCastingFocus = null;
            }
            TwoHand = null;
            OffHand = null;
            Shield = newShield;
            return true;
        }

        public bool EquipWeapon(ItemModel newWeapon, bool isDualWielder)
        {
            if (newWeapon.Weapon.Properties.TwoHanded.Enabled)
            {
                MainHand = null;
                OffHand = null;
                SpellCastingFocus = null;
                Shield = null;
                TwoHand = newWeapon;
            }
            else
            {
                TwoHand = null;
                if (MainHand == null)
                {
                    MainHand = newWeapon;
                }
                else if (OffHand == null)
                {
                    if (isDualWielder || AreBothWeaponsLight(MainHand, newWeapon))
                    {
                        SpellCastingFocus = null;
                        Shield = null;
                        OffHand = newWeapon;
                    }
                    else
                    {
                        OffHand = null;
                        MainHand = newWeapon;
                    }
                }
                else
                {
                    if (isDualWielder || AreBothWeaponsLight(OffHand, newWeapon))
                    {
                        MainHand = newWeapon;
                    }
                    else
                    {
                        OffHand = null;
                        MainHand = newWeapon;
                    }
                }
            }
            return true;
        }

        public bool EquipMagicItem(ItemModel Item)
        {
            if (AttunedItem1 == null) { AttunedItem1 = Item; }
            else if (AttunedItem2 == null) { AttunedItem2 = Item; }
            else if (AttunedItem3 == null) { AttunedItem3 = Item; }
            else { return false; }  //Item was not equipped
            return true;
        }

        public bool UnequipArmour(string inventoryID)
        {
            if (inventoryID.Equals(Armour.InventoryID))
            {
                Armour = null;
                Remove(inventoryID);
                return true;
            }
            return false;
        }

        public bool UnequipSpellcastingFocus(string inventoryID)
        {
            if (inventoryID.Equals(SpellCastingFocus.InventoryID))
            {
                SpellCastingFocus = null;
                Remove(inventoryID);
                return true;
            }
            return false;
        }

        public bool UnequipShield(string inventoryID)
        {
            if (inventoryID.Equals(Shield.InventoryID))
            {
                Shield = null;
                Remove(inventoryID);
                return true;
            }
            return false;
        }

        public bool UnequipTwoHand(string inventoryID)
        {
            if (inventoryID.Equals(TwoHand.InventoryID))
            {
                TwoHand = null;
                Remove(inventoryID);
                return true;
            }
            return false;
        }

        public bool UnequipMainHand(string inventoryID)
        {
            if (inventoryID.Equals(MainHand.InventoryID))
            {
                if (OffHand != null && OffHand.Is(ItemTypeCategory.Weapon))
                {
                    MainHand = OffHand;
                    OffHand = null;
                }
                else
                {
                    MainHand = null;
                }
                Remove(inventoryID);
                return true;
            }
            return false;
        }

        public bool UnequipOffHand(string inventoryID)
        {
            if (inventoryID.Equals(OffHand.InventoryID))
            {
                OffHand = null;
                Remove(inventoryID);
                return true;
            }
            return false;
        }

        public bool UnequipMagicItem(string inventoryID)
        {
            if (inventoryID.Equals(AttunedItem1.InventoryID))
            {
                AttunedItem1 = AttunedItem2?.Clone();
                AttunedItem2 = AttunedItem3?.Clone();
                AttunedItem3 = null;
                Remove(inventoryID);
                return true;
            }
            else if (inventoryID.Equals(AttunedItem2.InventoryID))
            {
                AttunedItem2 = AttunedItem3?.Clone();
                AttunedItem3 = null;
                Remove(inventoryID);
                return true;
            }
            else if (inventoryID.Equals(AttunedItem3.InventoryID))
            {
                AttunedItem3 = null;
                Remove(inventoryID);
                return true;
            }
            return false;
        }

        private bool AreBothWeaponsLight(ItemModel Weapon1, ItemModel Weapon2)
        {
            return Weapon1.Weapon.Properties.Light.Enabled && Weapon2.Weapon.Properties.Light.Enabled;
        }
    }
}