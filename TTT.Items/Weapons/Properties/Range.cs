﻿using System.Text.Json.Serialization;

namespace TTT.Items.Weapons.Properties
{
    public class Range : BaseWeaponProperty
    {
        [JsonIgnore]
        public override string Description => "A weapon that can be used to make a ranged Attack has a range in parentheses after the Ammunition or thrown property. The range lists two numbers. The first is the weapon’s normal range in feet, and the second indicates the weapon’s long range. When attacking a target beyond normal range, you have disadvantage on the Attack roll. You can’t Attack a target beyond the weapon’s long range.";
        
        /// <summary>
        /// Attack up to this range in feet with no disadvantage
        /// </summary>
        public int NormalRange { get; set; } = 0;

        /// <summary>
        /// Attack up to this range in feet with disadvantage
        /// </summary>
        public int LongRange { get; set; } = 0;

        [JsonIgnore]
        public override string QuickInfo
        {
            get
            {
                return $"{this.Name} ({NormalRange}/{LongRange})";
            }
        }

        public new Range Clone()
        {
            Range Clone = base.Clone().GenericTypeCast<BaseWeaponProperty, Range>();
            Clone.NormalRange = this.NormalRange;
            Clone.LongRange = this.LongRange;
            return Clone;
        }
    }
}
