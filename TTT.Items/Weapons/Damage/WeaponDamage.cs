using System.Text;

namespace TTT.Items.Weapons.Damage
{
    public class WeaponDamage
    {
        public Damage Damage1 { get; set; } = new Damage();
        public Damage Damage2 { get; set; } = new Damage();
        public Damage Damage3 { get; set; } = new Damage();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(!this.Damage1.IsEmpty ? $"{Damage1.ToString()}" : "");
            sb.Append(!this.Damage2.IsEmpty ? $" + {Damage2.ToString()}" : "");
            sb.Append(!this.Damage3.IsEmpty ? $" + {Damage3.ToString()}" : "");
            return sb.ToString();
        }
    }
}
