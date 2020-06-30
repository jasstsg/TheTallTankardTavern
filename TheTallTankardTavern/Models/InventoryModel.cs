using TTT.Items;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using TheTallTankardTavern.Helpers;
using System;

namespace TheTallTankardTavern.Models
{
	public class InventoryModel : BaseListModel<string>
	{
		public int CurrentWeight
		{
			get
			{
				int weight = 0;
				foreach (string itemID in this)
				{
					weight += ItemDataContext.GetModelFromID(itemID).Weight;
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
			ItemModel Item = ItemDataContext.GetModelFromID(itemID);
			Item.InstanceID = $"{Item.ID}&{Guid.NewGuid().ToString()}";
			base.Add(Item.InstanceID);
		}
	}
}
