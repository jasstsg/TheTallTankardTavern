using System.ComponentModel;

namespace TheTallTankardTavern.Models
{
	public class FeatureModel : BaseModel
	{
		[DisplayName("Prerequisite")]
		public string Prerequisite { get; set; }

		[DisplayName("Description")]
		public string Desc { get; set; }

		[DisplayName("Class")]
		public string Class { get; set; }

		[DisplayName("Subclass")]
		public string Subclass { get; set; }

		[DisplayName("Is Class Feature")]
		public bool IsClassFeature { get; set; }
	}
}
