using Newtonsoft.Json;
using System.Collections.Generic;
using TheTallTankardTavern.Helpers;
using static TheTallTankardTavern.Configuration.ApplicationSettings;

namespace TheTallTankardTavern.Models
{
    public class PartyModel : BaseModel
    {
        public List<MemberModel> Members { get; set; } = new List<MemberModel>();

        [JsonIgnore]
        public string NewMemberId { get; set; } = "";
    }

    public class MemberModel
    {
        private CharacterModel _character = null;
        public string CharacterId { get; set; } = "";
        public int Initiative { get; set; } = 0;

        [JsonIgnore]
        public CharacterModel Character
        {
            get
            {
                if (_character == null)
                {
                    _character = CharacterDataContext.GetModelFromID(this.CharacterId);
                }
                return _character;
            }
        }
    }
}
