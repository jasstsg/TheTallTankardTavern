namespace TTT.Items
{
    public class ItemType
    {
        private string innerString;

        public static readonly ItemType Miscellaneous = new ItemType("Miscellaneous");
        public static readonly ItemType AdventuringGear = new ItemType("Adventuring Gear");
        public static readonly ItemType Ammunition = new ItemType("Ammunition");
        public static readonly ItemType EquipmentPack = new ItemType("Equipment Pack");
        public static readonly ItemType Gemstone = new ItemType("Gemstone");
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

        //Armour Types
        public static readonly ItemType LightArmour = new ItemType("Light Armour");
        public static readonly ItemType MediumArmour = new ItemType("Medium Armour");
        public static readonly ItemType HeavyArmour = new ItemType("Heavy Armour");

        //Weapon Types
        public static readonly ItemType SimpleMeleeWeapon = new ItemType("Simple Ranged Weapon");
        public static readonly ItemType SimpleRangedWeapon = new ItemType("Martial Melee Weapon");
        public static readonly ItemType MartialMeleeWeapon = new ItemType("Martial Melee Weapon");
        public static readonly ItemType MartialRangedWeapon = new ItemType("Martial Ranged Weapon");
        public static readonly ItemType FireArmsRangedWeapon = new ItemType("Firearms Ranged Weapon");

        private ItemType(string stringValue)
        {
            this.innerString = stringValue;
        }

        public static implicit operator string(ItemType weaponType)
        {
            return weaponType.innerString;
        }

        public static implicit operator ItemType(string itemType)
        {
            return itemType;
        }

        public override string ToString()
        {
            return this.innerString;
        }
    }
}
