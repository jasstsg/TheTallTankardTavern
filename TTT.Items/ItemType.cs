using TTT.Common.Abstractions;

namespace TTT.Items
{
    public class ItemType : BaseStringEnum
    {
        public ItemType(string stringValue) : base(stringValue) { }

        public static readonly ItemType Miscellaneous = new ItemType("Miscellaneous");
        public static readonly ItemType AdventuringGear = new ItemType("Adventuring Gear");
        public static readonly ItemType Ammunition = new ItemType("Ammunition");
        public static readonly ItemType EquipmentPack = new ItemType("Equipment Pack");
        public static readonly ItemType Gemstone = new ItemType("Gemstone");
        public static readonly ItemType Ring = new ItemType("Ring");
        public static readonly ItemType Poison = new ItemType("Poison");
        public static readonly ItemType Tool = new ItemType("Tool");

        //Modes of Transportation
        public static readonly ItemType Mount = new ItemType("Mount");
        public static readonly ItemType LandVehicle = new ItemType("Vehicle (Land)");
        public static readonly ItemType WaterVehicle = new ItemType("Vehicle (Water)");

        //SpellCasting Focuses
        public static readonly ItemType ArcaneFocus = new ItemType("Arcane Focus");
        public static readonly ItemType DruidicFocus = new ItemType("Druidic Focus");
        public static readonly ItemType HolySymbol = new ItemType("Holy Symbol");

        //Armour
        public static readonly ItemType LightArmour = new ItemType("Light Armour");
        public static readonly ItemType MediumArmour = new ItemType("Medium Armour");
        public static readonly ItemType HeavyArmour = new ItemType("Heavy Armour");
        public bool IsArmour { get { return this.ToString().Contains("Armour"); } }

        //Weapon
        public static readonly ItemType SimpleMeleeWeapon = new ItemType("Simple Ranged Weapon");
        public static readonly ItemType SimpleRangedWeapon = new ItemType("Martial Melee Weapon");
        public static readonly ItemType MartialMeleeWeapon = new ItemType("Martial Melee Weapon");
        public static readonly ItemType MartialRangedWeapon = new ItemType("Martial Ranged Weapon");
        public static readonly ItemType FirearmsRangedWeapon = new ItemType("Firearms Ranged Weapon");
        public bool IsWeapon { get { return this.ToString().Contains("Weapon"); } }
    }
}
