using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TTT;
using TTT.Items;

namespace TheTallTankardTavern.Models
{
    public class WeaponArmourProficienciesModel : Dictionary<string, bool>
    {
        public WeaponArmourProficienciesModel()
        {
            //Parent Item Types
            //this.Add(ItemType.ShieldAndArmour, false);
            //this.Add(ItemType.SimpleMeleeWeapon, false);
            //this.Add(ItemType.SimpleRangedWeapon, false);
            //this.Add(ItemType.MartialMeleeWeapon, false);
            //this.Add(ItemType.MartialRangedWeapon, false);
            //this.Add(ItemType.OtherWeapon, false);

            //Shield and Armour
            this.Add(ItemType.Shield, false);
            this.Add(ItemType.LightArmour, false);
            this.Add(ItemType.MediumArmour, false);
            this.Add(ItemType.HeavyArmour, false);

            //Simple Melee Weapons
            this.Add(ItemType.Club, false);
            this.Add(ItemType.Dagger, false);
            this.Add(ItemType.Greatclub, false);
            this.Add(ItemType.Handaxe, false);
            this.Add(ItemType.Javelin, false);
            this.Add(ItemType.LightHammer, false);
            this.Add(ItemType.Mace, false);
            this.Add(ItemType.Quaterstaff, false);
            this.Add(ItemType.Sickle, false);
            this.Add(ItemType.Spear, false);

            //Simple Ranged Weapons
            this.Add(ItemType.LightCrossbow, false);
            this.Add(ItemType.Dart, false);
            this.Add(ItemType.Shortbow, false);
            this.Add(ItemType.Sling, false);

            //Martial Melee Weapons
            this.Add(ItemType.Battleaxe, false);
            this.Add(ItemType.Flail, false);
            this.Add(ItemType.Glaive, false);
            this.Add(ItemType.Greataxe, false);
            this.Add(ItemType.Greatsword, false);
            this.Add(ItemType.Halberd, false);
            this.Add(ItemType.Lance, false);
            this.Add(ItemType.Longsword, false);
            this.Add(ItemType.Maul, false);
            this.Add(ItemType.Morningstar, false);
            this.Add(ItemType.Pike, false);
            this.Add(ItemType.Rapier, false);
            this.Add(ItemType.Scimitar, false);
            this.Add(ItemType.Shortsword, false);
            this.Add(ItemType.Trident, false);
            this.Add(ItemType.WarPick, false);
            this.Add(ItemType.Warhammer, false);
            this.Add(ItemType.Whip, false);

            //Martial Ranged Weapons
            this.Add(ItemType.Blowgun, false);
            this.Add(ItemType.HandCrossbow, false);
            this.Add(ItemType.HeavyCrossbow, false);
            this.Add(ItemType.Longbow, false);
            this.Add(ItemType.Net, false);

            //Other Weapon
            //this.Add(ItemType.BuffItem, false);
            //this.Add(ItemType.DebuffItem, false);
        }

        public IEnumerable<string> ShieldAndArmourKeys
        {
            get
            {
                return this.Keys.Where(k => ItemType.GetItemType(k).Category
                .EqualsAny(ItemTypeCategory.Shield, ItemTypeCategory.Armour));
            }
        }

        public IEnumerable<string> SimpleMeleeWeaponKeys { get { return GetWeaponKeys(ItemType.SimpleMeleeWeapon); } }
        public IEnumerable<string> SimpleRangedWeaponKeys { get { return GetWeaponKeys(ItemType.SimpleRangedWeapon); } }
        public IEnumerable<string> MartialMeleeWeaponKeys { get { return GetWeaponKeys(ItemType.MartialMeleeWeapon); } }
        public IEnumerable<string> MartialRangedWeaponKeys { get { return GetWeaponKeys(ItemType.MartialRangedWeapon); } }
        public IEnumerable<string> OtherWeapons { get { return GetWeaponKeys(ItemType.OtherWeapon); } }

        private IEnumerable<string> GetWeaponKeys(ItemType ParentItemType)
        {
            return this.Keys.Where(k => ParentItemType.HasChildType(k));
        }
    }
}
