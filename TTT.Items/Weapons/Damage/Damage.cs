using TTT.Common;

namespace TTT.Items.Weapons.Damage
{
    public class Damage
    {
        public Damage(Die die, DamageType damageType)
        {
            this.Die = die;
            this.Type = damageType;
        }

        public Die Die { get; set; } = Die.None;
        public DamageType Type { get; set; } = DamageType.None;
    }
}
