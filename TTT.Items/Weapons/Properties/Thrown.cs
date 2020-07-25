using System.Text.Json.Serialization;

namespace TTT.Items.Weapons.Properties
{
    public class Thrown : BaseWeaponProperty
    {
        [JsonIgnore]
        public override string Description => "If a weapon has the thrown property, you can throw the weapon to make a ranged Attack. If the weapon is a melee weapon, you use the same ability modifier for that Attack roll and damage roll that you would use for a melee Attack with the weapon. For example, if you throw a Handaxe, you use your Strength, but if you throw a Dagger, you can use either your Strength or your Dexterity, since the Dagger has the finesse property.";

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

        public new Thrown Clone()
        {
            Thrown Clone = base.Clone().GenericTypeCast<BaseWeaponProperty, Thrown>();
            Clone.NormalRange = this.NormalRange;
            Clone.LongRange = this.LongRange;
            return Clone;
        }
    }
}
