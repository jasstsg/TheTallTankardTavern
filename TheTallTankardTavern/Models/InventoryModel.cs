using TTT.Items;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using TheTallTankardTavern.Helpers;

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
	}
}
