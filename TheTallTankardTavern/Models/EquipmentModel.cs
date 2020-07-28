using static TheTallTankardTavern.Configuration.ApplicationSettings;
using Newtonsoft.Json;
using System.Linq;
using TTT.Items;

namespace TheTallTankardTavern.Models
{
    public class EquipmentModel : BaseListModel<string>
    {
        [JsonProperty]
        private ItemModel _spellCastingFocus = null;
        [JsonProperty]
        private ItemModel _armour = null;
        [JsonProperty]
        private ItemModel _shield = null;
        [JsonProperty]
        private ItemModel _twoHand = null;
        [JsonProperty]
        private ItemModel _mainHand = null;
        [JsonProperty]
        private ItemModel _offHand = null;

        private void EquipmentSetter(ref ItemModel _privateEquipmentVariable, ItemModel value)
        {
            if (_privateEquipmentVariable != null)
            {
                Remove(_privateEquipmentVariable.InventoryID);
            }
            if (value != null)
            {
                Add(value.InventoryID);
            }
            _privateEquipmentVariable = value;
        }
        [JsonIgnore]
        public ItemModel SpellCastingFocus
        {
            get { return _spellCastingFocus; }
            set { EquipmentSetter(ref _spellCastingFocus, value); }
        }
        [JsonIgnore]
        public ItemModel Armour
        {
            get { return _armour; }
            set { EquipmentSetter(ref _armour, value); }
        }
        [JsonIgnore]
        public ItemModel Shield
        {
            get { return _shield; }
            set { EquipmentSetter(ref _shield, value); }
        }
        [JsonIgnore]
        public ItemModel TwoHand
        {
            get { return _twoHand; }
            set { EquipmentSetter(ref _twoHand, value); }
        }
        [JsonIgnore]
        public ItemModel MainHand
        {
            get { return _mainHand; }
            set { EquipmentSetter(ref _mainHand, value); }
        }
        [JsonIgnore]
        public ItemModel OffHand
        {
            get { return _offHand; }
            set { EquipmentSetter(ref _offHand, value); }
        }

        public bool TryEquip(string inventoryID, InventoryModel Inventory, bool isDualWielder)
        {
            if (!Inventory.Contains(inventoryID))
            {
                return false;
            }
            ItemModel Item = ItemDataContext.FirstOrDefault(
                item => item.ID.Equals(inventoryID.Substring(0, inventoryID.IndexOf("&")))).Clone();
            Item.InventoryID = inventoryID;

            switch (Item.Type.Category)
            {
                case ItemTypeCategory.SpellCastingFocus: return EquipSpellCastingFocus(Item);
                case ItemTypeCategory.Armour: return EquipArmour(Item);
                case ItemTypeCategory.Shield: return EquipShield(Item);
                case ItemTypeCategory.Weapon: return EquipWeapon(Item, isDualWielder);
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

        private bool AreBothWeaponsLight(ItemModel Weapon1, ItemModel Weapon2)
        {
            return Weapon1.Weapon.Properties.Light.Enabled && Weapon2.Weapon.Properties.Light.Enabled;
        }
    }
}