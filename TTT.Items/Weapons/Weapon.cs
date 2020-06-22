

using TTT.Items.Weapons.Damage;
using TTT.Items.Weapons.Properties;

namespace TTT.Items.Weapons
{
    public class Weapon
    {
        public string Name { get; set; }
        public WeaponDamage Damage { get; set; } = new WeaponDamage();
        public WeaponProperties Properties { get; set; } = new WeaponProperties();

        /// <summary>
        /// The weapon's weight in lbs
        /// </summary>
        public int Weight { get; set; } = 0;
    }
}
