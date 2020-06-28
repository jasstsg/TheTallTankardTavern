using System.Text;
using TTT.Common.Abstractions;

namespace TTT.Items.Weapons.Damage
{
    public class WeaponDamage : BaseEnumerableCollection<Damage>
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Count > 0)
            {
                foreach (Damage damage in this)
                {
                    sb.Append($"{damage.Die.ToString()} {damage.Type.ToString()} + ");
                }
                return sb.ToString().Remove(sb.Length - 3);
            }
            return "";
        }
    }
}
