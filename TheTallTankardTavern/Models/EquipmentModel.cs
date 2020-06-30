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
		/// <param name="itemID">Item ID of Armour Item to be equipped</param>
		/// <returns></returns>
		public bool TryEquip(string itemID)
		{
			ItemModel Item = ItemDataContext.GetModelFromID(itemID);

			//If the item is armour, remove any armour that is currently being worn
			if (Item.Type.IsArmour)
			{
				string CurrentArmourID = this.FirstOrDefault(id => ItemDataContext.GetModelFromID(id).Type.IsArmour);
				if (!string.IsNullOrEmpty(CurrentArmourID))
				{
					this.Remove(CurrentArmourID);
				}
			}

			this.Add(itemID);
			return true;
		}

		public bool Unequip(string itemID)
		{
			return this.Remove(itemID);
		}
	}
}