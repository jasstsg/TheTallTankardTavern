using System.ComponentModel;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Helpers;
using TTT.Common.Abstractions;

namespace TheTallTankardTavern.Models
{
	public class UserModel : BaseModel
	{
		public string Username { get; set; }

		public Constants.ROLES Role { get; set; }

		[DisplayName("Active Character")]
		public string ActiveCharacterID { get; set; }

		[DisplayName("My Dashboard Message")]
		public string Message { get; set; }

		public string MessageUpdated { get; set; }

		public CharacterModel GetActiveCharacter()
        {
			return ApplicationSettings.CharacterDataContext.GetModelFromID(ActiveCharacterID);
        }
	}
}
