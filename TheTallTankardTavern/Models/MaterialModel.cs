using Newtonsoft.Json;
using System;
using System.ComponentModel;
using TTT.Common.Abstractions;

namespace TheTallTankardTavern.Models
{
	public class MaterialModel : BaseModel
	{
		[DisplayName("Attack")]
		public int Attack { get; set; } = 0;

		[DisplayName("Damage")]
		public int Damage { get; set; } = 0;

		[DisplayName("Armour Class")]
		public int Armour_Class { get; set; } = 10;

		[DisplayName("Enchantment Slots")]
		public int Enchantment_Slots { get; set; } = 1;

		[DisplayName("Weight Class")]
		public string Weight_Class { get; set; } = "Medium";

		[JsonIgnore]
		[DisplayName("Cost / Unit")]
		public int Cost_Per_Unit
		{
			get
			{
				if (this.Name.Equals("None"))
				{
					return 1;
				}
				return 20 * (Attack + Damage + Armour_Class) + (int)(10.0 * Math.Pow(5.0, Enchantment_Slots)) - 10;
			}
		}
	}
}
