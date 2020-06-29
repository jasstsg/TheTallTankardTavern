using System.Text.Json.Serialization;

namespace TTT.Items.Weapons.Properties
{
    public class Reach : BaseWeaponProperty
    {
        [JsonIgnore]
        public override string Description => "This weapon adds 5 feet to your reach when you Attack with it, as well as when determining your reach for Opportunity Attacks with it.";
    }
}
