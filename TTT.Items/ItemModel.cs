using TTT.Common.Abstractions;

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
        public int cost { get; set; } = 0;

        /// <summary>
        /// The weight of the item in pounds
        /// </summary>
        public int Weight { get; set; } = 0;

        /// <summary>
        /// True is the item is a magic item
        /// </summary>
        public bool IsMagic { get; set; } = false;
    }
}
