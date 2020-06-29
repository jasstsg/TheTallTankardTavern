using System;
using System.Text.Json.Serialization;
using TTT.Common;

namespace TTT.Items.Weapons.Properties
{
    public class Versitile : BaseWeaponProperty
    {
        [JsonIgnore]
        public override string Description => "This weapon can be used with one or two hands. A damage value in parentheses appears with the property—the damage when the weapon is used with two hands to make a melee Attack.";

        [JsonIgnore]
        public string TwoHandedDamageAsString
        {
            get { return TwoHandedDamage.ToString().Replace("_", ""); }
            set { this.TwoHandedDamage = (Die)Enum.Parse(typeof(Die), $"_{value}"); }
        }
        public Die TwoHandedDamage { get; set; } = Die._None;
        
        [JsonIgnore]
        public override string QuickInfo
        {
            get
            {
                return $"{this.Name} ({TwoHandedDamage.ToString().Replace("_", "")})";
            }
        }
    }
}
