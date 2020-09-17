using System.Text.Json.Serialization;

namespace TTT.Items.Weapons.Properties
{
    public class Light : BaseWeaponProperty
    {
        [JsonIgnore]
        public override string Description => "A light weapon is small and easy to handle, making it ideal for use when fighting with two Weapons.";

        public new Light Clone()
        {
            return base.Clone().GenericTypeCast<BaseWeaponProperty, Light>();
        }
    }
}
