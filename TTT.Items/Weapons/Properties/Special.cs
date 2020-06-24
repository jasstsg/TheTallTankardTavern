using System;
using System.Collections.Generic;
using System.Text;

namespace TTT.Items.Weapons.Properties
{
    class Special : BaseWeaponProperty
    {
        public override string Description => "A weapon with the special property has unusual rules governing its use, explained in the weapon’s description (see “Special Weapons” later in this section).";

        public string SpecialDescription { get; set; } = "";
    }
}
