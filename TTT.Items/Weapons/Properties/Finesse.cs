using System.Text.Json.Serialization;

namespace TTT.Items.Weapons.Properties
{
    public class Finesse : BaseWeaponProperty
    {
        [JsonIgnore]
        public override string Description => "When Making an Attack with a finesse weapon, you use your choice of your Strength or Dexterity modifier for the Attack and Damage Rolls. You must use the same modifier for both rolls.";
    }
}
