using System.Text;

namespace TTT.Items.Weapons.Properties
{
    public class WeaponProperties
    {
        public Ammunition Ammunition { get; set; } = new Ammunition();
        public Finesse Finesse { get; set; } = new Finesse();
        public Heavy Heavy { get; set; } = new Heavy();
        public Improvised Improvised { get; set; } = new Improvised();
        public Light Light { get; set; } = new Light();
        public Loading Loading { get; set; } = new Loading();
        public Range Range { get; set; } = new Range();
        public Reach Reach { get; set; } = new Reach();
        public Silvered Silvered { get; set; } = new Silvered();
        public Special Special { get; set; } = new Special();
        public Thrown Thrown { get; set; } = new Thrown();
        public Two_Handed TwoHanded { get; set; } = new Two_Handed();
        public Versitile Versitile { get; set; } = new Versitile();

        public string ToShortString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Ammunition.Enabled ? $"{Ammunition.QuickInfo}, " : "");
            sb.Append(Finesse.Enabled ? $"{Finesse.QuickInfo}, " : "");
            sb.Append(Heavy.Enabled ? $"{Heavy.QuickInfo}, " : "");
            sb.Append(Improvised.Enabled ? $"{Improvised.QuickInfo}, " : "");
            sb.Append(Light.Enabled ? $"{Light.QuickInfo}, " : "");
            sb.Append(Loading.Enabled ? $"{Loading.QuickInfo}, " : "");
            sb.Append(Range.Enabled ? $"{Range.QuickInfo}, " : "");
            sb.Append(Silvered.Enabled ? $"{Silvered.QuickInfo}, " : "");
            sb.Append(Special.Enabled ? $"{Special.QuickInfo}, " : "");
            sb.Append(Thrown.Enabled ? $"{Thrown.QuickInfo}, " : "");
            sb.Append(TwoHanded.Enabled ? $"{TwoHanded.QuickInfo}, " : "");
            sb.Append(Versitile.Enabled ? $"{Versitile.QuickInfo}, " : "");
            if (sb.Length >= 2)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Ammunition.Enabled ? $"{Ammunition.QuickInfo}: {Ammunition.Description}\n" : "");
            sb.Append(Finesse.Enabled ? $"{Finesse.QuickInfo}: {Finesse.Description}\n" : "");
            sb.Append(Heavy.Enabled ? $"{Heavy.QuickInfo}: {Heavy.Description}\n" : "");
            sb.Append(Improvised.Enabled ? $"{Improvised.QuickInfo}: {Improvised.Description}\n" : "");
            sb.Append(Light.Enabled ? $"{Light.QuickInfo}: {Light.Description}\n" : "");
            sb.Append(Loading.Enabled ? $"{Loading.QuickInfo}: {Loading.Description}\n" : "");
            sb.Append(Range.Enabled ? $"{Range.QuickInfo}: {Range.Description}\n" : "");
            sb.Append(Silvered.Enabled ? $"{Silvered.QuickInfo}: {Silvered.Description}\n" : "");
            sb.Append(Special.Enabled ? $"{Special.QuickInfo}: {Special.Description}\n" : "");
            sb.Append(Thrown.Enabled ? $"{Thrown.QuickInfo}: {Thrown.Description}\n" : "");
            sb.Append(TwoHanded.Enabled ? $"{TwoHanded.QuickInfo}: {TwoHanded.Description}\n" : "");
            sb.Append(Versitile.Enabled ? $"{Versitile.QuickInfo}: {Versitile.Description}\n" : "");

            if (sb.Length > 0)
            {
                sb.Insert(0, "\nProperties:\n", 1);
            }
            return sb.ToString();
        }

        public WeaponProperties Clone()
        {
            return new WeaponProperties()
            {
                Ammunition = this.Ammunition.Clone(),
                Finesse = this.Finesse.Clone(),
                Heavy = this.Heavy.Clone(),
                Improvised = this.Improvised.Clone(),
                Light = this.Light.Clone(),
                Loading = this.Loading.Clone(),
                Range = this.Range.Clone(),
                Reach = this.Reach.Clone(),
                Silvered = this.Silvered.Clone(),
                Special = this.Special.Clone(),
                Thrown = this.Thrown.Clone(),
                TwoHanded = this.TwoHanded.Clone(),
                Versitile = this.Versitile.Clone()
            };
        }
    }
}
