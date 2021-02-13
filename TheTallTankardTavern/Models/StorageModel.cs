using Newtonsoft.Json;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using TTT.Common.Abstractions;
using TheTallTankardTavern.Helpers;

namespace TheTallTankardTavern.Models
{
    public class StorageModel : BaseListModel<string>, IFileDataModel
    {
		public string ID { get; set; }

		public int MaxCapacity { get; set; } = 1500;

		[JsonIgnore]
		public int CurrentWeight
		{
			get
			{
				int weight = 0;
				foreach (string inventoryID in this)
				{
					weight += ItemDataContext.GetModelFromID(inventoryID.Substring(0, inventoryID.IndexOf('+'))).Weight;
				}
				return weight;
			}
		}
	}
}
