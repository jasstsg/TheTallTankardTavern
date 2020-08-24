using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using TTT.Common.Abstractions;

namespace TTT.Items
{
    public class ItemType : BaseStringEnum
    {
        public ItemType(string stringValue, ItemTypeCategory category) : base(stringValue)
        {
            this.Category = category;
        }

        [JsonIgnore]
        public ItemTypeCategory Category { get; private set; }

        public static readonly ItemType Miscellaneous = new ItemType("Miscellaneous", ItemTypeCategory.Other);
        public static readonly ItemType AdventuringGear = new ItemType("Adventuring Gear", ItemTypeCategory.Other);
        public static readonly ItemType Ammunition = new ItemType("Ammunition", ItemTypeCategory.Other);
        public static readonly ItemType EquipmentPack = new ItemType("Equipment Pack", ItemTypeCategory.Other);
        public static readonly ItemType Gemstone = new ItemType("Gemstone", ItemTypeCategory.Other);
        public static readonly ItemType Ring = new ItemType("Ring", ItemTypeCategory.Other);
        public static readonly ItemType Poison = new ItemType("Poison", ItemTypeCategory.Other);
        public static readonly ItemType Tool = new ItemType("Tool", ItemTypeCategory.Other);

        //Modes of Transportation
        public static readonly ItemType Mount = new ItemType("Mount", ItemTypeCategory.Other);
        public static readonly ItemType VehicleLand = new ItemType("Vehicle (Land)", ItemTypeCategory.Other);
        public static readonly ItemType VehicleWater = new ItemType("Vehicle (Water)", ItemTypeCategory.Other);

        //SpellCasting Focuses
        public static readonly ItemType ArcaneFocus = new ItemType("Arcane Focus", ItemTypeCategory.SpellCastingFocus);
        public static readonly ItemType ComponentPouch = new ItemType("Component Pouch", ItemTypeCategory.SpellCastingFocus);
        public static readonly ItemType DruidicFocus = new ItemType("Druidic Focus", ItemTypeCategory.SpellCastingFocus);
        public static readonly ItemType HolySymbol = new ItemType("Holy Symbol", ItemTypeCategory.SpellCastingFocus);

        //Armour
        public static readonly ItemType LightArmour = new ItemType("Light Armour", ItemTypeCategory.Armour);
        public static readonly ItemType MediumArmour = new ItemType("Medium Armour", ItemTypeCategory.Armour);
        public static readonly ItemType HeavyArmour = new ItemType("Heavy Armour", ItemTypeCategory.Armour);
        public static readonly ItemType Shield = new ItemType("Shield", ItemTypeCategory.Shield);

        //Weapon
        public static readonly ItemType SimpleMeleeWeapon = new ItemType("Simple Melee Weapon", ItemTypeCategory.Weapon);
        public static readonly ItemType SimpleRangedWeapon = new ItemType("Simple Ranged Weapon", ItemTypeCategory.Weapon);
        public static readonly ItemType MartialMeleeWeapon = new ItemType("Martial Melee Weapon", ItemTypeCategory.Weapon);
        public static readonly ItemType MartialRangedWeapon = new ItemType("Martial Ranged Weapon", ItemTypeCategory.Weapon);
        //public static readonly ItemType FirearmsRangedWeapon = new ItemType("Firearms Ranged Weapon", ItemTypeCategory.Weapon);

        public static ItemType GetItemType(string stringName)
        {
            StringBuilder sb = new StringBuilder(stringName);
            sb.Replace("(", "");
            sb.Replace(")", "");
            sb.Replace(" ", "");
            return (ItemType)typeof(ItemType).GetField(sb.ToString()).GetValue(null);
        }
    }
}
