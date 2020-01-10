using System.Collections.Generic;

namespace TheTallTankardTavern.Configuration
{
	public class ConfigurationSettings
	{
		public Dictionary<string, string[]> Classes { get; set; } = new Dictionary<string, string[]>();

		public Dictionary<string, string[]> Races { get; set; } = new Dictionary<string, string[]>();

		public Dictionary<string, string[]> Alignments { get; set; } = new Dictionary<string, string[]>();

		public Dictionary<string, string[]> Item_Types { get; set; } = new Dictionary<string, string[]>();

		public Dictionary<int, int> CharacterAdvancement { get; set; } = new Dictionary<int, int>();
	}
}
