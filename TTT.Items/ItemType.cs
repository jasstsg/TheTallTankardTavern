using Newtonsoft.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using TTT.Common.Abstractions;

namespace TTT.Items
{
    public class ItemType : BaseStringEnum
    {
        [JsonIgnore]
        public ItemTypeCategory Category { get; }

        [JsonIgnore]
        public ItemType ParentType { get; } = null;

        public ItemType(string stringValue, ItemTypeCategory category) : base(stringValue)
        {
            this.Category = category;
        }

        public ItemType(string stringValue, ItemTypeCategory category, ItemType ParentType) : this(stringValue, category)
        {
            this.ParentType = ParentType;
        }

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

        //Weapon parent types
        public static readonly ItemType ShieldAndArmour = new ItemType("Shields And Armour", ItemTypeCategory.ParentType);
        public static readonly ItemType SimpleMeleeWeapon = new ItemType("Simple Melee Weapons", ItemTypeCategory.ParentType);
        public static readonly ItemType SimpleRangedWeapon = new ItemType("Simple Ranged Weapons", ItemTypeCategory.ParentType);
        public static readonly ItemType MartialMeleeWeapon = new ItemType("Martial Melee Weapons", ItemTypeCategory.ParentType);
        public static readonly ItemType MartialRangedWeapon = new ItemType("Martial Ranged Weapons", ItemTypeCategory.ParentType);
        public static readonly ItemType OtherWeapon = new ItemType("Other Weapons", ItemTypeCategory.ParentType);
        public static readonly ItemType OtherEquippableItem = new ItemType("Other Equippable Items", ItemTypeCategory.ParentType);
        //public static readonly ItemType FirearmsRangedWeapon = new ItemType("Firearms Ranged Weapon", ItemTypeCategory.Weapon);

        //Simple Melee Weapons
        public static readonly ItemType Club = new ItemType("Club", ItemTypeCategory.Weapon, SimpleMeleeWeapon);
        public static readonly ItemType Dagger = new ItemType("Dagger", ItemTypeCategory.Weapon, SimpleMeleeWeapon);
        public static readonly ItemType Greatclub = new ItemType("Greatclub", ItemTypeCategory.Weapon, SimpleMeleeWeapon);
        public static readonly ItemType Handaxe = new ItemType("Handaxe", ItemTypeCategory.Weapon, SimpleMeleeWeapon);
        public static readonly ItemType Javelin = new ItemType("Javelin", ItemTypeCategory.Weapon, SimpleMeleeWeapon);
        public static readonly ItemType LightHammer = new ItemType("Light Hammer", ItemTypeCategory.Weapon, SimpleMeleeWeapon);
        public static readonly ItemType Mace = new ItemType("Mace", ItemTypeCategory.Weapon, SimpleMeleeWeapon);
        public static readonly ItemType Quaterstaff = new ItemType("Quaterstaff", ItemTypeCategory.Weapon, SimpleMeleeWeapon);
        public static readonly ItemType Sickle = new ItemType("Sickle", ItemTypeCategory.Weapon, SimpleMeleeWeapon);
        public static readonly ItemType Spear = new ItemType("Spear", ItemTypeCategory.Weapon, SimpleMeleeWeapon);

        //Simple Ranged Weapons
        public static readonly ItemType LightCrossbow = new ItemType("Light Crossbow", ItemTypeCategory.Weapon, SimpleRangedWeapon);
        public static readonly ItemType Dart = new ItemType("Dart", ItemTypeCategory.Weapon, SimpleRangedWeapon);
        public static readonly ItemType Shortbow = new ItemType("Shortbow", ItemTypeCategory.Weapon, SimpleRangedWeapon);
        public static readonly ItemType Sling = new ItemType("Sling", ItemTypeCategory.Weapon, SimpleRangedWeapon);

        //Martial Melee Weapons
        public static readonly ItemType Battleaxe = new ItemType("Battleaxe", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Flail = new ItemType("Flail", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Glaive = new ItemType("Glaive", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Greataxe = new ItemType("Greataxe", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Greatsword = new ItemType("Greatsword", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Halberd = new ItemType("Halberd", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Lance = new ItemType("Lance", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Longsword = new ItemType("Longsword", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Maul = new ItemType("Maul", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Morningstar = new ItemType("Morningstar", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Pike = new ItemType("Pike", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Rapier = new ItemType("Rapier", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Scimitar = new ItemType("Scimitar", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Shortsword = new ItemType("Shortsword", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Trident = new ItemType("Trident", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType WarPick = new ItemType("War Pick", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Warhammer = new ItemType("Warhammer", ItemTypeCategory.Weapon, MartialMeleeWeapon);
        public static readonly ItemType Whip = new ItemType("Whip", ItemTypeCategory.Weapon, MartialMeleeWeapon);

        //Martial Ranged Weapons
        public static readonly ItemType Blowgun = new ItemType("Blowgun", ItemTypeCategory.Weapon, MartialRangedWeapon);
        public static readonly ItemType HandCrossbow = new ItemType("Hand Crossbow", ItemTypeCategory.Weapon, MartialRangedWeapon);
        public static readonly ItemType HeavyCrossbow = new ItemType("Heavy Crossbow", ItemTypeCategory.Weapon, MartialRangedWeapon);
        public static readonly ItemType Longbow = new ItemType("Longbow", ItemTypeCategory.Weapon, MartialRangedWeapon);
        public static readonly ItemType Net = new ItemType("Net", ItemTypeCategory.Weapon, MartialRangedWeapon);

        //AttunableItems
        public static readonly ItemType AttunableItem = new ItemType("Attunable Item", ItemTypeCategory.Attunable);

        //Other Weapons
        public static readonly ItemType BuffItem = new ItemType("Buff Item", ItemTypeCategory.Weapon, OtherWeapon);
        public static readonly ItemType DebuffItem = new ItemType("Debuff Item", ItemTypeCategory.Weapon, OtherWeapon);

        public static ItemType GetItemType(string stringName)
        {
            StringBuilder sb = new StringBuilder(stringName);
            sb.Replace("(", "");
            sb.Replace(")", "");
            sb.Replace(" ", "");
            return (ItemType)typeof(ItemType).GetField(sb.ToString())?.GetValue(null);
        }
    }

    public static class ItemTypeExtensions
    {
        public static bool HasChildType(this ItemType ParentItemType, ItemType ChildItemType)
        {
            return ChildItemType?.ParentType != null && ChildItemType.ParentType.Equals(ParentItemType);
        }

        public static bool HasChildType(this ItemType ParentItemType, string childItemTypeString)
        {
            return ParentItemType.HasChildType(ItemType.GetItemType(childItemTypeString));
        }

        //Checks if the item type is the same, or if one is the parent type of the other
        public static bool Is(this ItemType itemType1, ItemType itemType2)
        {
            return itemType1 == itemType2 || itemType1 == itemType2.ParentType || itemType1.ParentType == itemType2;
        }
    }
}
