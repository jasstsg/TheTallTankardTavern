using System.ComponentModel;

namespace TheTallTankardTavern.Models
{
	public class BaseModel
	{
		public string ID { get; set; }

		[DisplayName("Name")]
		public string Name { get; set; }

		[DisplayName("Is Homebrew")]
        public bool IsHomebrew { get; set; }

		[DisplayName("Is Hidden")]
		public bool IsHidden { get; set; }
	}
}
