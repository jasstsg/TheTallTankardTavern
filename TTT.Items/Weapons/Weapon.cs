using TTT.Items.Weapons.Damage;
using TTT.Items.Weapons.Properties;

namespace TTT.Items.Weapons
{
    public class Weapon
    {
        public string Name { get; set; }
        public WeaponType Type { get; set; } = WeaponType.SimpleMeleeWeapon;
        public TotalWeaponDamage Damage { get; set; } = new TotalWeaponDamage();
        public WeaponProperties Properties { get; set; } = new WeaponProperties();

        /// <summary>
        /// The weapon's weight in lbs
        /// </summary>
        public int Weight { get; set; } = 0;
    }
}
