using System.ComponentModel;
using TTT.Common.Abstractions;
using TTT.Items.Armour;
using TTT.Items.Weapons;

namespace TTT.Items
{
    public class ItemModel : BaseModel
    {
        /// <summary>
        /// The type of item
        /// </summary>
        public ItemType Type { get; set; } = ItemType.Miscellaneous;

        /// <summary>
        /// Description of the item
        /// </summary>
        public string Description { get; set; } = "";
        
        /// <summary>
        /// Cost of the item in copper pieces
        /// </summary>
        [DisplayName("Cost (cp)")]
        public int Cost { get; set; } = 0;

        /// <summary>
        /// The weight of the item in pounds
        /// </summary>
        [DisplayName("Weight (lbs)")]
        public int Weight { get; set; } = 0;

        /// <summary>
        /// True is the item is a magic item
        /// </summary>
        public bool IsMagic { get; set; } = false;

        public WeaponModel Weapon = new WeaponModel();

        public ArmourModel Armour = new ArmourModel();
    }
}
