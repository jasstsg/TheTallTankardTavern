using Microsoft.AspNetCore.Html;
using System.Text;
using TheTallTankardTavern.Models;
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

        public static string CheckEncumberance(bool isEncumbered)
        {
            return isEncumbered ? "card-body-border-red" : "";
        }

        public static HtmlString SpellComponents(SpellModel spell)
        {
            StringBuilder comps = new StringBuilder();

            if (spell.Verbal_Components)
            {
                comps.Append("V");
            }
            
            if (spell.Somatic_Components)
            {
                comps.Append("S");
            }
            
            if (spell.Material_Components)
            {
                if (spell.HasMaterialCost)
                {
                    comps.Append("<span class='material-cost'>M");
                    if (spell.Material_Consumed)
                    {
                        comps.Append("↻");
                    }
                    comps.Append("</span>");
                }
                else
                {
                    comps.Append("M");
                }
            }

            return new HtmlString(comps.ToString());
            //< td >@(Spell.Verbal_Components ? "V" : " ")@(Spell.Somatic_Components ? "S" : " ") < span class="@(Spell.HasMaterialCost ? "material-cost" : "")">@(Spell.Material_Components? "M" : " ")</span></td>
        }
    }
}
