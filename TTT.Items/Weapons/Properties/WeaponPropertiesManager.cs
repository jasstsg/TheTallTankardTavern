using System;
using System.Collections.Generic;
using System.Text;

namespace TTT.Items.Weapons.Properties
{
    public static class WeaponPropertiesManager
    {
        public static string GetPropertiesList(this WeaponProperties WeaponProperties)
        {
            StringBuilder sb = new StringBuilder();
            if (WeaponProperties.Count > 0)
            {
                foreach (AbstractWeaponProperty property in WeaponProperties)
                {
                    sb.Append($"{property.QuickInfo}, ");
                }
                return sb.ToString().Remove(sb.Length - 2);
            }
            return "";
        }
    }
}
