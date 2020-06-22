namespace TTT.Items.Weapons.Properties
{
    public abstract class AbstractWeaponProperty
    {
        public virtual string Name { get { return this.GetType().Name.Replace("_", "-"); } }
        public virtual string Description { get; }
        public virtual string QuickInfo { get { return this.Name; } }
    }
}
