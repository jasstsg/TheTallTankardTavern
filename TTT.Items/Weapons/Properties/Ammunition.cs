using System;
using System.Collections.Generic;
using System.Text;

namespace TTT.Items.Weapons.Properties
{
    class Ammunition : BaseWeaponProperty
    {
        public override string Description => "You can use a weapon that has the Ammunition property to make a ranged Attack only if you have Ammunition to fire from the weapon. Each time you Attack with the weapon, you expend one piece of Ammunition. Drawing the Ammunition from a Quiver, case, or other container is part of the Attack (you need a free hand to load a one-handed weapon). At the end of the battle, you can recover half your expended Ammunition by taking a minute to Search the battlefield. If you use a weapon that has the Ammunition property to make a melee Attack, you treat the weapon as an Improvised Weapon (see “Improvised Weapons” later in the section). A sling must be loaded to deal any damage when used in this way.";
    }
}
