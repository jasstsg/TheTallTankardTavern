using TTT.Items;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using TheTallTankardTavern.Helpers;
using System;
using System.Linq;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Models
{
	public class InventoryModel : BaseListModel<string>
	{
		public int CurrentWeight
		{
			get
			{
				int weight = 0;
				foreach (string inventoryID in this)
				{
					weight += ItemDataContext.GetModelFromID(inventoryID.Substring(0, inventoryID.IndexOf('&'))).Weight;
				}
				return weight;
			}
		}

		public bool IsEncumbered(int strengthScore)
		{
			return (CurrentWeight > 15 * strengthScore);
		}

		public override void Add(string itemID)
		{
			ItemModel Item = ItemDataContext.GetModelFromID(itemID).Clone();
			Item.InstanceID = Guid.NewGuid().ToString();
			base.Add(Item.InventoryID);
		}

		public override bool Remove(string inventoryID)
        {
			//If the item is in the inventory, and is removed successfully, return true
			return this.Contains(inventoryID) && base.Remove(inventoryID);
        }
	}
}
