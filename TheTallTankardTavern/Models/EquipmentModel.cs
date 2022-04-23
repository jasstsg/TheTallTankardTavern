using static TheTallTankardTavern.Configuration.ApplicationSettings;
using Newtonsoft.Json;
using System.Linq;
using TTT.Items;
using TTT.Common.Abstractions;
using TheTallTankardTavern.Helpers;
using System.Collections.Generic;

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
        public List<string> _attunedItems { get; set; } = new List<string>();

        public override void Clear()
        {
            if (Armour != null) { UnequipArmour(Armour.InventoryID); }
            if (MainHand != null) { UnequipMainHand(MainHand.InventoryID); }
            if (OffHand != null) { UnequipOffHand(OffHand.InventoryID); }
            if (Shield != null) { UnequipShield(Shield.InventoryID); }
            if (SpellCastingFocus != null) { UnequipSpellcastingFocus(SpellCastingFocus.InventoryID); }
            if (TwoHand != null) { UnequipTwoHand(TwoHand.InventoryID); }
            if (AttunedItems != null) { UnequipAllAttunableItems(); }
            base.Clear();
        }

        private ItemModel EquipmentGetter(string _innerEquipmentVar)
        {
            if (string.IsNullOrEmpty(_innerEquipmentVar))
            {
                return null;
            }
            ItemModel Item = ItemDataContext.GetModelFromInventoryID(_innerEquipmentVar).Clone();
            Item.InstanceID = _innerEquipmentVar.Substring(_innerEquipmentVar.IndexOf("+") + 1);
            return Item;
        }

        private string EquipmentSetter(string _innerEquipmentVar, ItemModel value)
        {
            if (!string.IsNullOrEmpty(_innerEquipmentVar))
            {
                Remove(_innerEquipmentVar);
            }
            if (value != null)
            {
                Add(value.InventoryID);
            }
            return value?.InventoryID;
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
        [JsonIgnore]
        public ItemModel OffHand
        {
            get { return EquipmentGetter(_offHand); }
            set { _offHand = EquipmentSetter(_offHand, value); }
        }

        [JsonIgnore]
        public List<ItemModel> _attunedItemModels = null;

        [JsonIgnore]
        public List<string> AttunedItems
        {
            get
            {
                return _attunedItems;
                /*
                if (_attunedItemModels == null)
                {
                    _attunedItemModels = new List<ItemModel>();
                    for (int i = 0; i < _attunedItems.Count; i++)
                    {
                        _attunedItemModels.Add(EquipmentGetter(_attunedItems[i]));
                    }
                }
                return _attunedItemModels;
                */
            }
        }

        public bool TryEquip(string inventoryID, InventoryModel Inventory, bool isDualWielder, int maxAttunedItems)
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
                case ItemTypeCategory.Attunable: return EquipAttunableItem(Item, maxAttunedItems);
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

        public bool EquipAttunableItem(ItemModel Item, int maxAtunnableItems)
        {
            if (AttunedItems.Count >= maxAtunnableItems)
            {
                return false;
            }

            Add(Item.InventoryID);
            AttunedItems.Add(Item.InventoryID);
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

        public bool UnequipAttunableItem(string inventoryID)
        {
            if (Remove(inventoryID))
            {
                AttunedItems.Remove(inventoryID);
                return true;
            }
            return false;
        }

        public void UnequipAllAttunableItems()
        {
            _attunedItems.Clear();
            AttunedItems.Clear();
        }

        private bool AreBothWeaponsLight(ItemModel Weapon1, ItemModel Weapon2)
        {
            return Weapon1.Weapon.Properties.Light.Enabled && Weapon2.Weapon.Properties.Light.Enabled;
        }
    }
}