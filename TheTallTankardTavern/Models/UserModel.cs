using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Helpers;
using TTT.Common.Abstractions;
using static TheTallTankardTavern.Configuration.ApplicationSettings;

namespace TheTallTankardTavern.Models
{
	public class UserModel : BaseModel
	{
		private List<PartyModel> _parties;

		[JsonIgnore]
		public List<PartyModel> Parties
		{
			get
			{
				if (this._parties == null)
				{
					LoadParties();
				}
				return _parties;
			}
		}

		public string Username { get; set; }

		public Constants.ROLES Role { get; set; }

		[DisplayName("Active Character")]
		public string ActiveCharacterID { get; set; }

		[DisplayName("My Dashboard Message")]
		public string Message { get; set; }

		public string MessageUpdated { get; set; }

		public CharacterModel GetActiveCharacter()
        {
			return CharacterDataContext.GetModelFromID(ActiveCharacterID);
        }

		public List<CharacterModel> GetCharacters()
		{
			return CharacterDataContext.Where(c => c.Player_Name == this.Username).ToList();
		}

		public void LoadParties()
		{
			List<CharacterModel> Characters = GetCharacters();
            _parties = new List<PartyModel>();

            foreach (CharacterModel c in Characters)
			{
				foreach (PartyModel p in PartyDataContext)
				{
					foreach (MemberModel m in p.Members)
					{
						if (c.ID == m.CharacterId)
						{
							this._parties.Add(p);
                        }
					}
				}
			}
		}
	}
}
