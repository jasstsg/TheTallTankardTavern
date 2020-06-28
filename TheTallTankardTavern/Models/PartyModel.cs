using Newtonsoft.Json;
using System.Collections.Generic;
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
    }

    public class MemberModel
    {
        public string CharacterId { get; set; } = "";
        public int Initiative { get; set; } = 0;

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
