using System.Text;
using TTT.Common.Abstractions;

namespace TTT.Items.Weapons.Properties
{
    public class WeaponProperties : BaseEnumerableCollection<BaseWeaponProperty>
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Count > 0)
            {
                foreach (BaseWeaponProperty property in this)
                {
                    sb.Append($"{property.QuickInfo}, ");
                }
                return sb.ToString().Remove(sb.Length - 2);
            }
            return "";
        }
    }
}
