using System.ComponentModel;

namespace TTT.Common.Abstractions
{
	public class BaseModel
	{
		public string ID { get; set; } = "";

		[DisplayName("Name")]
		public string Name { get; set; } = "";

		[DisplayName("Homebrew")]
        public bool IsHomebrew { get; set; } = false;

		[DisplayName("Hide")]
		public bool IsHidden { get; set; } = false;
	}
}
