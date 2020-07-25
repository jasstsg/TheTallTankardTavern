using System.Collections.Generic;
using TheTallTankardTavern.Helpers;
using static TheTallTankardTavern.Configuration.Constants;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using Newtonsoft.Json;
using System.Linq;
using TTT.Items;

namespace TheTallTankardTavern.Models
{
    public class EquipmentModel : BaseListModel<string>
    {
        [JsonProperty]
        private ItemModel _armour = null;
        [JsonProperty]
        private ItemModel _shield = null;
        [JsonProperty]
        private ItemModel _twoHandWeapon = null;
        [JsonProperty]
        private ItemModel _weaponOne = null;
        [JsonProperty]
        private ItemModel _weaponTwo = null;

        [JsonIgnore]
        public ItemModel Armour
        {
            get { return _armour; }
            set
            {
                if (_armour != null)
                {
                    this.Remove(_armour.InventoryID);
                }
                if (value != null)
                {
                    this.Add(value.InventoryID);
                }
                _armour = value;
            }
        }
        [JsonIgnore]
        public ItemModel Shield
        {
            get { return _shield; }
            set
            {
                if (_shield != null)
                {
                    this.Remove(_shield.InventoryID);
                }
                if (value != null)
                {
                    this.Add(value.InventoryID);
                }
                _shield = value;
            }
        }
        [JsonIgnore]
        public ItemModel TwoHandWeapon
        {
            get { return _twoHandWeapon; }
            set
            {
                if (_twoHandWeapon != null)
                {
                    this.Remove(_twoHandWeapon.InventoryID);
                }
                if (value != null)
                {
                    this.Add(value.InventoryID);
                }
                _twoHandWeapon = value;
            }
        }
        [JsonIgnore]
        public ItemModel WeaponOne
        {
            get { return _weaponOne; }
            set
            {
                if (_weaponOne != null)
                {
                    this.Remove(_weaponOne.InventoryID);
                }
                if (value != null)
                {
                    this.Add(value.InventoryID);
                }
                _weaponOne = value;
            }
        }
        [JsonIgnore]
        public ItemModel WeaponTwo
        {
            get { return _weaponTwo; }
            set
            {
                if (_weaponTwo != null)
                {
                    this.Remove(_weaponTwo.InventoryID);
                }
                if (value != null)
                {
                    this.Add(value.InventoryID);
                }
                _weaponTwo = value;
            }
        }

        public bool TryEquip(string inventoryID, InventoryModel Inventory)
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
                case ItemTypeCategory.Weapon: return EquipWeapon(Item);
                case ItemTypeCategory.Armour: return EquipArmour(Item);
                case ItemTypeCategory.Shield: return EquipShield(Item);
                default: return false;
            }
        }

        public bool EquipArmour(ItemModel newArmour)
        {
            this.Armour = newArmour;
            return true;
        }

        public bool EquipShield(ItemModel newShield)
        {
            this.Shield = newShield;
            return true;
        }

        public bool EquipWeapon(ItemModel newWeapon)
        {
            if (newWeapon.Weapon.Properties.TwoHanded.Enabled)
            {
                WeaponOne = null;
                WeaponTwo = null;
                TwoHandWeapon = newWeapon;
            }
            else
            {
                TwoHandWeapon = null;
                if (WeaponOne == null)
                {
                    WeaponOne = newWeapon;
                }
                else if (WeaponTwo == null)
                {
                    WeaponTwo = newWeapon;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public bool UnequipArmour(string inventoryID)
        {
            if (inventoryID.Equals(Armour.InventoryID))
            {
                Armour = null;
                this.Remove(inventoryID);
                return true;
            }
            return false;
        }

        public bool UnequipShield(string inventoryID)
        {
            if (inventoryID.Equals(Shield.InventoryID))
            {
                Shield = null;
                this.Remove(inventoryID);
                return true;
            }
            return false;
        }

        public bool UnequipTwoHandWeapon(string inventoryID)
        {
            if (inventoryID.Equals(TwoHandWeapon.InventoryID))
            {
                TwoHandWeapon = null;
                this.Remove(inventoryID);
                return true;
            }
            return false;
        }

        public bool UnequipWeaponOne(string inventoryID)
        {
            if (inventoryID.Equals(WeaponOne.InventoryID))
            {
                WeaponOne = null;
                this.Remove(inventoryID);
                return true;
            }
            return false;
        }

        public bool UnequipWeaponTwo(string inventoryID)
        {
            if (inventoryID.Equals(WeaponTwo.InventoryID))
            {
                WeaponTwo = null;
                this.Remove(inventoryID);
                return true;
            }
            return false;
        }
    }
}