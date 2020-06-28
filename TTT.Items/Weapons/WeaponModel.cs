using TTT.Items.Weapons.Damage;
using TTT.Items.Weapons.Properties;

namespace TTT.Items.Weapons
{
    public class WeaponModel : ItemModel
    {
        public WeaponDamage Damage { get; set; } = new WeaponDamage();
        public WeaponProperties Properties { get; set; } = new WeaponProperties();

    }
}
