using System.ComponentModel;

namespace TTT.Common.Abstractions
{
	public class BaseModel : IFileDataModel
	{
		public string ID { get; set; } = "";

		[DisplayName("Name")]
		public string Name { get; set; } = "";

		[DisplayName("Homebrew")]
        public bool IsHomebrew { get; set; } = false;

		[DisplayName("Hide")]
		public bool IsHidden { get; set; } = false;

		public BaseModel Clone()
        {
			return new BaseModel()
			{
				ID = this.ID,
				Name = this.Name,
				IsHomebrew = this.IsHomebrew,
				IsHidden = this.IsHidden
			};
        }
	}
}
