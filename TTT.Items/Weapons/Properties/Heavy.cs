using System.Text.Json.Serialization;

namespace TTT.Items.Weapons.Properties
{
    public class Heavy : BaseWeaponProperty
    {
        [JsonIgnore]
        public override string Description => "Small creatures have disadvantage on Attack rolls with heavy Weapons. A heavy weapon’s size and bulk make it too large for a Small creature to use effectively.";

        public new Heavy Clone()
        {
            return base.Clone().GenericTypeCast<BaseWeaponProperty, Heavy>();
        }
    }
}
