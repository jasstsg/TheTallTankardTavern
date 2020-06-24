namespace TTT.Items.Weapons
{
    public class WeaponType
    {
        private string innerString;

        public static readonly WeaponType SimpleMeleeWeapon = new WeaponType("Simple Ranged Weapon");
        public static readonly WeaponType SimpleRangedWeapons = new WeaponType("Martial Melee Weapons");
        public static readonly WeaponType MartialMeleeWeapons = new WeaponType("Martial Melee Weapons");
        public static readonly WeaponType MartialRangedWeapons = new WeaponType("Martial Ranged Weapons");

        public WeaponType(string stringValue)
        {
            this.innerString = stringValue;
        }

        public static implicit operator string(WeaponType weaponType)
        {
            return weaponType.innerString;
        }

        public override string ToString()
        {
            return this.innerString;
        }
    }
}
