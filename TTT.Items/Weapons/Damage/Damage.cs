using System;
using System.Text.Json.Serialization;
using TTT.Common;

namespace TTT.Items.Weapons.Damage
{
    public class Damage
    {
        [JsonIgnore]
        public string DieAsString
        {
            get { return Die.ToString().Replace("_", ""); }
            set { this.Die = (Die)Enum.Parse(typeof(Die), $"_{value}"); }
        }

        [JsonIgnore]
        public string DamageTypeAsString
        {
            get { return Type.ToString(); }
            set { this.Type = (DamageType)Enum.Parse(typeof(DamageType), value); }
        }

        [JsonIgnore]
        public bool IsEmpty { get { return ((int)Die * (int)Type) <= 0; } }

        public Die Die { get; set; } = Die._None;
        public DamageType Type { get; set; } = DamageType.None;


        public override string ToString()
        {
            return $"{Die.ToString().Replace("_", "")} {Type.ToString()}";
        }
    }
}
