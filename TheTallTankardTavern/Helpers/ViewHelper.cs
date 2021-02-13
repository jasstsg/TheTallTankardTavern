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

        public static string Disable(bool disableIfTrue)
        {
            if (disableIfTrue)
            {
                return "disabled-cell";
            }
            return "";
        }

        public static string CheckPCHP(int remainingHP, int maxHP)
        {
            if (remainingHP > (maxHP / 2))
            {
                return "healthy";
            }
            else if (remainingHP > 0)
            {
                return "bloodied";
            }
            else
            {
                return "unconcious";
            }
        }
    }
}
