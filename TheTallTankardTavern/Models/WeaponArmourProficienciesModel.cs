using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTT.Items;

namespace TheTallTankardTavern.Models
{
    public class WeaponArmourProficienciesModel : Dictionary<string, bool>
    {
        public WeaponArmourProficienciesModel()
        {
            this.Add(ItemType.Shield, false);
            this.Add(ItemType.LightArmour, false);
            this.Add(ItemType.MediumArmour, false);
            this.Add(ItemType.HeavyArmour, false);
            this.Add(ItemType.SimpleMeleeWeapon, false);
            this.Add(ItemType.SimpleRangedWeapon, false);
            this.Add(ItemType.MartialMeleeWeapon, false);
            this.Add(ItemType.MartialRangedWeapon, false);
        }
    }
}
