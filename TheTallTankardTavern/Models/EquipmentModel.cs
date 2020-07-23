using System.Collections.Generic;
using TheTallTankardTavern.Helpers;
using static TheTallTankardTavern.Configuration.Constants;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using Newtonsoft.Json;
using System.Linq;
using TTT.Items;

namespace TheTallTankardTavern.Models
{
	public class EquipmentModel : BaseListModel<string>
	{
		/// <summary>
		/// Currently only Armour can be equipped
		/// </summary>
		/// <param name="inventoryID">The specific instance of the item to be equipped</param>
		/// <returns></returns>
		public bool TryEquip(string inventoryID)
		{
			ItemModel Item = ItemDataContext.GetModelFromID(inventoryID.Substring(0, inventoryID.IndexOf("&")));

			//If the item is armour, remove any armour that is currently being worn
			if (Item.Type.IsArmour)
			{
				string CurrentArmourID = this.InnerCollection.FirstOrDefault(id =>
					ItemDataContext.GetModelFromID(id.Substring(0, inventoryID.IndexOf("&"))).Type.IsArmour);
				if (!string.IsNullOrEmpty(CurrentArmourID))
				{
					this.Remove(CurrentArmourID);
				}
			}

			this.Add(inventoryID);
			return true;
		}

		public bool Unequip(string inventoryID)
		{
			return this.Remove(inventoryID);
		}
	}
}