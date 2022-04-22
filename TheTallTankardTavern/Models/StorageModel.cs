using Newtonsoft.Json;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using TTT.Common.Abstractions;
using TheTallTankardTavern.Helpers;
using System.ComponentModel;

namespace TheTallTankardTavern.Models
{
    public class StorageModel: IFileDataModel
    {
		[JsonProperty]
		public string ID { get; set; }

		[JsonProperty]
		public string Name { get; set; }

		[JsonProperty]
		[DisplayName("Max Capacity (lbs)")]
		public int MaxCapacity { get; set; }
		
		[JsonProperty]
		[DisplayName("Disable Storage")]
		public bool IsLocked { get; set; }

		[JsonProperty]
		public InventoryModel Inventory { get; set; } = new InventoryModel();
	}
}
