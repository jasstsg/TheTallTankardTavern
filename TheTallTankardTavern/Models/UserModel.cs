using System.ComponentModel;
using TheTallTankardTavern.Configuration;

namespace TheTallTankardTavern.Models
{
	public class UserModel : BaseModel
	{
		public string Username { get; set; }

		public Constants.ROLES Role { get; set; }

		[DisplayName("My Public Notepad")]
		public string Message { get; set; }

		public string MessageUpdated { get; set; }
	}
}
