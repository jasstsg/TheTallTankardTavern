using System.Text.Json.Serialization;

namespace TTT.Items.Weapons.Properties
{
    public class Two_Handed : BaseWeaponProperty
    {
        [JsonIgnore]
        public override string Description => "This weapon requires two hands when you Attack with it.";

        public new Two_Handed Clone()
        {
            return base.Clone().GenericTypeCast<BaseWeaponProperty, Two_Handed>();
        }
    }
}
