using System.ComponentModel;

namespace TTT.Common.Abstractions
{
	public class BaseModel
	{
		public string ID { get; set; } = "";

		[DisplayName("Name")]
		public string Name { get; set; } = "";

		[DisplayName("Is Homebrew")]
        public bool IsHomebrew { get; set; } = false;

		[DisplayName("Is Hidden")]
		public bool IsHidden { get; set; } = false;
	}
}
