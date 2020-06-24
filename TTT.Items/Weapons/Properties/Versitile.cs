using System;
using System.Collections.Generic;
using System.Text;

namespace TTT.Items.Weapons.Properties
{
    class Versitile : BaseWeaponProperty
    {
        public override string Description => "This weapon can be used with one or two hands. A damage value in parentheses appears with the property—the damage when the weapon is used with two hands to make a melee Attack.";

        public string TwoHandedDamage { get; set; } = "";

        public override string QuickInfo
        {
            get
            {
                return $"{this.Name} ({TwoHandedDamage})";
            }
        }
    }
}
