using System.Text.Json.Serialization;

namespace TTT.Items.Weapons.Properties
{
    public class BaseWeaponProperty
    {
        [JsonIgnore]
        public virtual string Name { get { return this.GetType().Name.Replace("_", "-"); } }
        [JsonIgnore]
        public virtual string Description { get; }
        [JsonIgnore]
        public virtual string QuickInfo { get { return this.Name; } }

        public virtual bool Enabled { get; set; } = false;

        protected BaseWeaponProperty Clone()
        {
            return new BaseWeaponProperty()
            {
                Enabled = this.Enabled
            };
        }
    }
}
