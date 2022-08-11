using TTT.Items;
using TTT.Common.Abstractions;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using TheTallTankardTavern.Helpers;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheTallTankardTavern.Models
{
	public class InventoryModel : BaseListModel<string>
	{
		private Dictionary<string, int> _compactInventory = new Dictionary<string, int>();

		public Dictionary<string, int> CompactInventory
		{
			get
			{
				if (_compactInventory == null || _compactInventory.Count == 0)
                {
					GenerateCompactInventory();
                }
				return _compactInventory;
			}
		}

		[JsonIgnore]
		public int CurrentWeight
		{
			get
			{
				int weight = 0;
				foreach (string inventoryID in this)
				{
					weight += ItemDataContext.GetModelFromInventoryID(inventoryID).Weight;
				}
				return weight;
			}
		}

		public override void Add(string inventoryID)
        {
			base.Add(inventoryID);
			GenerateCompactInventory();
		}

		private void AddNewItemInstance(string itemID)
		{
			ItemModel Item = ItemDataContext.GetModelFromID(itemID).Clone();
			Item.InstanceID = Guid.NewGuid().ToString();
			base.Add(Item.InventoryID);
		}

		public void AddNewItemInstance(string itemID, int quantity)
		{
			for (int i = 0; i < quantity; i++)
            {
				AddNewItemInstance(itemID);
			}

			GenerateCompactInventory();
		}

		public override bool Remove(string inventoryID)
		{
			//If the item is in the inventory, and is removed successfully, return true
			bool inventoryRemoved = this.Contains(inventoryID) && base.Remove(inventoryID);

			GenerateCompactInventory();

			return inventoryRemoved; 
        }

		private void GenerateCompactInventory()
        {
			_compactInventory.Clear();
			foreach (string inventoryID in this)
            {
				string id = inventoryID.ToItemID();
				if (_compactInventory.ContainsKey(id))
                {
					_compactInventory[id] = _compactInventory[id] + 1;
                }
				else
                {
					_compactInventory.Add(id, 1);
                }
            }
        }
	}
}
