using System.Text.Json.Serialization;

namespace TTT.Items.Weapons.Properties
{
    public class Loading : BaseWeaponProperty
    {
        [JsonIgnore]
        public override string Description => "Because of the time required to load this weapon, you can fire only one piece of Ammunition from it when you use an action, Bonus Action, or Reaction to fire it, regardless of the number of attacks you can normally make.";

        public new Loading Clone()
        {
            return base.Clone().GenericTypeCast<BaseWeaponProperty, Loading>();
        }
    }
}
