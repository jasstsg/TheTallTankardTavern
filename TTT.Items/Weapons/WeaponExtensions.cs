using System.Text;
using TTT.Items.Weapons.Damage;
using TTT.Items.Weapons.Properties;

namespace TTT.Items.Weapons
{
    public static class WeaponExtensions
    {
        public static string GetDamageString(this Weapon Weapon)
        {
            TotalWeaponDamage totalDamage = Weapon.Damage;
            StringBuilder sb = new StringBuilder();
            if (totalDamage.Count > 0)
            {
                foreach (WeaponDamage damage in totalDamage)
                {
                    sb.Append($"{damage.Die.ToString()} {damage.Type.ToString()} + ");
                }
                return sb.ToString().Remove(sb.Length - 3);
            }
            return "";
        }

        public static string GetPropertiesString(this Weapon Weapon)
        {
            WeaponProperties properties = Weapon.Properties;
            StringBuilder sb = new StringBuilder();
            if (properties.Count > 0)
            {
                foreach (BaseWeaponProperty property in properties)
                {
                    sb.Append($"{property.QuickInfo}, ");
                }
                return sb.ToString().Remove(sb.Length - 2);
            }
            return "";
        }
    }
}
