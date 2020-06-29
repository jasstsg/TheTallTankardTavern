using System.ComponentModel;

namespace TTT.Items.Armour
{
    public class ArmourModel
    {
        [DisplayName("Armour Class")]
        public int ArmourClass { get; set; } = 10;

        [DisplayName("Strength Required")]
        public int StrengthRequired { get; set; } = 0;

        [DisplayName("Disadvantage On Stealth")]
        public bool DisadvantageOnStealth { get; set; } = false;
    }
}
