using System.ComponentModel;
using TTT.Items.Weapons.Damage;
using TTT.Items.Weapons.Properties;

namespace TTT.Items.Weapons
{
    public class WeaponModel
    {
        [DisplayName("Magic Bonus")]
        public int Plus { get; set; } = 0;
        public WeaponDamage Damage { get; set; } = new WeaponDamage();
        public WeaponProperties Properties { get; set; } = new WeaponProperties();

    }
}
