using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TTT.Common.Abstractions;

namespace TheTallTankardTavern.Models
{
	public class SpellModel : BaseModel, IFileDataModel
	{
		[DisplayName("Description")]
		public string Desc { get; set; } = "";

		[DisplayName("Higher Level")]
		public string Higher_Level_Desc { get; set; } = "";

		[DisplayName("Range")]
		public string Range { get; set; } = "";

		[DisplayName("Verbal Components")]
		public bool Verbal_Components { get; set; }

		[DisplayName("Somatic Components")]
		public bool Somatic_Components { get; set; }

		[DisplayName("Material Components")]
		public bool Material_Components { get; set; }

		[DisplayName("Materials")]
		public string Materials { get; set; }

		[DisplayName("Material Cost")]
		public string Material_Cost { get; set; }

		public bool HasMaterialCost => !string.IsNullOrEmpty(this.Material_Cost);

		[DisplayName("Material Consumed")]
		public bool Material_Consumed { get; set; }

		[DisplayName("Verbal Components")]
		public string V { get => this.TF2YN(this.Verbal_Components); }

		[DisplayName("Somatic Components")]
		public string S { get => this.TF2YN(this.Somatic_Components); }

		[DisplayName("Material Components")]
		public string M { get => this.TF2YN(this.Material_Components); }

		private string TF2YN(bool boolValue) { return boolValue ? "Y" : "N"; }

		[DisplayName("Ritual")]
		public bool Ritual { get; set; }

		[DisplayName("Duration")]
		public string Duration { get; set; } = "";

		[DisplayName("Concentration")]
		public bool Concentration { get; set; }

		[DisplayName("Casting Time")]
		public string Casting_Time { get; set; } = "";

		[DisplayName("Level")]
		public int Level { get; set; }

		[DisplayName("School")]
		public string School { get; set; } = "";

		[DisplayName("Classes")]
		public List<string> Classes { get; set; } = new List<string>();

		[JsonIgnore]
		public string Classes_Setter
		{
			get
			{
				return string.Join(",", this.Classes);
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.Classes = new List<string>(value.Split(","));
				}
			}
		}

		[DisplayName("Subclasses")]
		public List<string> Subclasses { get; set; } = new List<string>();

		[JsonIgnore]
		public string Subclasses_Setter
		{
			get
			{
				return string.Join(",", this.Subclasses);
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.Subclasses = new List<string>(value.Split(","));
				}
			}
		}

		//Shortform classes string list for Index view
		[DisplayName("Class")]
		public string ClassStringList
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				foreach (string clsStr in this.Classes)
				{
					sb.Append($"{clsStr.Trim().Substring(0, 3).ToUpper()}, ");
				}
				return sb.ToString().Substring(0, sb.Length - 2);
			}
		}
	}
}
