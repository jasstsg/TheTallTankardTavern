using Newtonsoft.Json;
using System.ComponentModel;
using TheTallTankardTavern.Helpers;
using static TheTallTankardTavern.Configuration.ApplicationSettings;

namespace TheTallTankardTavern.Models
{
	public class BackgroundModel : BaseModel
	{
		[DisplayName("Description")]
		public string Desc { get; set; }

		[DisplayName("Skill Proficiencies")]
		public string Skill_Proficiencies { get; set; }

		[DisplayName("Tool Proficiencies")]
		public string Tool_Proficiencies { get; set; }

		public string Languages { get; set; }

		public string Equipment { get; set; }

		[JsonIgnore]
		[DisplayName("Feature")]
		public string Feature { get { return FeatureDataContext.GetModelFromID(this.FeatureID)?.Name ?? ""; } }

		[DisplayName("Feature")]
		public string FeatureID { get; set; }
	}
}
