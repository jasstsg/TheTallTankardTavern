using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;
using TTT.Common.Abstractions;
using TTT.Items.Armour;
using TTT.Items.Weapons;

namespace TTT.Items
{
    public class ItemModel : BaseModel
    {
        /// <summary>
        /// Used to differenciate separate instances of an item in a characters inventory
        /// </summary>
        public string InstanceID { get; set; } = "";

        /// <summary>
        /// Used for inventory and equipment (Uses the format: ID+InstanceID)
        /// </summary>
        [JsonIgnore]
        public string InventoryID
        { 
            get { return $"{this.ID}+{this.InstanceID}"; }
            set
            {
                this.ID = value.Substring(0, value.IndexOf("+"));
                this.InstanceID = value.Substring(value.IndexOf("+") + 1);
            }
        }

        /// <summary>
        /// The type of item
        /// </summary>
        [JsonIgnore]
        public string TypeAsString
        {
            get { return Type.ToString(); }
            set { Type = ItemType.GetItemType(value); }
        }
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
        [DisplayName("Magical")]
        public bool IsMagic { get; set; } = false;

        public WeaponModel Weapon { get; set; } = new WeaponModel();

        public ArmourModel Armour { get; set; } = new ArmourModel();

        public bool Is(ItemTypeCategory itemTypeCategory)
        {
            return this.Type.Category.Equals(itemTypeCategory);
        }

        [JsonIgnore]
        public string PopUpText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(this.Description);
                sb.AppendLine(this.Weapon.Properties.ToString());
                return sb.ToString();
            }
        }

        public new ItemModel Clone()
        {
            ItemModel Clone = base.Clone().GenericTypeCast<BaseModel, ItemModel>();
            Clone.InstanceID = this.InstanceID;
            Clone.TypeAsString = this.TypeAsString;
            Clone.Description = this.Description;
            Clone.Cost = this.Cost;
            Clone.Weight = this.Weight;
            Clone.IsMagic = this.IsMagic;
            Clone.Weapon = this.Weapon.Clone();
            Clone.Armour = this.Armour.Clone();
            return Clone;
        }
    }
}
