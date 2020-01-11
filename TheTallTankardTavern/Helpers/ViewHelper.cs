using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Helpers
{
    public class ViewHelper
    {
        public static string SkillHighlight(string skill)
        {
            switch (skill)
            {
                case PROFICIENCY.EXPERTISE: return "skill-expert";
                case PROFICIENCY.PROFICIENT: return "skill-proficient";
                default: return "";
            }
        }
    }
}
