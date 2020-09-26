using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TheTallTankardTavern.Helpers;
using TTT.Common.Abstractions;
using static TheTallTankardTavern.Configuration.ApplicationSettings;

namespace TheTallTankardTavern.Models
{
    public class PartyModel : BaseModel
    {
        public List<MemberModel> Members { get; set; } = new List<MemberModel>();

        [JsonIgnore]
        public string NewMemberId { get; set; } = "";

        public string Date { get; set; } = "";

        public List<CharacterModel> GetAllPartyMemberCharacters()
        {
            return Members.Select(m => m.Character).OrderBy(c => c.Name).ToList();
        }
    }

    public class MemberModel
    {
        public string CharacterId { get; set; } = "";
        public int Initiative { get; set; } = 0;

        public string Conditions { get; set; } = "";

        [JsonIgnore]
        public CharacterModel Character
        {
            get
            {
                return CharacterDataContext.GetModelFromID(this.CharacterId);
            }
        }
    }
}
