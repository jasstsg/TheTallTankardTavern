using System.ComponentModel;
using TheTallTankardTavern.Configuration;
using TTT.Common.Abstractions;

namespace TheTallTankardTavern.Models
{
	public class UserModel : BaseModel
	{
		public string Username { get; set; }

		public Constants.ROLES Role { get; set; }

		[DisplayName("Active Character")]
		public string ActiveCharacterID { get; set; }

		[DisplayName("My Public Notepad")]
		public string Message { get; set; }

		public string MessageUpdated { get; set; }
	}
}
