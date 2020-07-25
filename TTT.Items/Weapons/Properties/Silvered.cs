using System.Text.Json.Serialization;

namespace TTT.Items.Weapons.Properties
{
    public class Silvered : BaseWeaponProperty
    {
        [JsonIgnore]
        public override string Description => "Some Monsters that have immunity or Resistance to nonmagical Weapons are susceptible to silver Weapons, so cautious adventurers invest extra coin to plate their Weapons with silver. You can silver a single weapon or ten pieces of Ammunition for 100 gp. This cost represents not only the price of the silver, but the time and expertise needed to add silver to the weapon without making it less effective.";

        public new Silvered Clone()
        {
            return base.Clone().GenericTypeCast<BaseWeaponProperty, Silvered>();
        }
    }
}
