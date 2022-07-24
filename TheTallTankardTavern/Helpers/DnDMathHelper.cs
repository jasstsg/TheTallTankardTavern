using System.Collections.Generic;
using System.Linq;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Models;

namespace TheTallTankardTavern.Helpers
{
	public static class DnDMathHelper
	{
		public static int LevelProgressPercent(this CharacterModel c)
        {
			int nextLevelExp = ApplicationSettings.ConfigurationSettings.CharacterAdvancement[c.Level + 1];
			int currentLevelExp = ApplicationSettings.ConfigurationSettings.CharacterAdvancement[c.Level];
			return ((nextLevelExp - c.Experience_Points) * 100) / (nextLevelExp - currentLevelExp);
		}

		public static string LevelToExp(this CharacterModel c)
		{
			int level = c.Level + 1;
			if (level < 1)
			{
				return "0";
			}
			if (level > 30)
			{
				return "∞";
			}
			return ApplicationSettings.ConfigurationSettings.CharacterAdvancement[level].FormatNumber();
		}

		public static int ExpToLevel(this CharacterModel c)
		{
			foreach (KeyValuePair<int, int> pair in ApplicationSettings.ConfigurationSettings.CharacterAdvancement)
			{
				if (pair.Value > c.Experience_Points)
				{
					return pair.Key - 1;
				}
			}
			return ApplicationSettings.ConfigurationSettings.CharacterAdvancement.Keys.Max();
		}

		public static int GetProfBonus(this CharacterModel c)
		{
			return (c.Level - 1) / 4 + 2;
		}

        public static int GetProfBonus(int level)
        {
            return (level - 1) / 4 + 2;
        }

        public static int AbilityScoreToModifier(this int abilityScore)
		{
			return abilityScore / 2 - 5;
		}

		public static int GetSkillModifider(this CharacterModel c, Constants.SKILL_CATEGORY skillCategory, string skillProficiencyLevel)
		{
			int skillMod = c.Proficiency_Bonus * Constants.SKILL_LEVELS[skillProficiencyLevel];
			switch (skillCategory)
			{
			    case Constants.SKILL_CATEGORY.Strength: return skillMod + c.Strength.Modifier;
			    case Constants.SKILL_CATEGORY.Dexterity: return skillMod + c.Dexterity.Modifier;
			    case Constants.SKILL_CATEGORY.Constitution: return skillMod + c.Constitution.Modifier;
			    case Constants.SKILL_CATEGORY.Intelligence: return skillMod + c.Intelligence.Modifier;
			    case Constants.SKILL_CATEGORY.Wisdom: return skillMod + c.Wisdom.Modifier;
			    case Constants.SKILL_CATEGORY.Charisma: return skillMod + c.Charisma.Modifier;
			    default: return int.MinValue;
			}
		}

		public static int GetSaveProficiency(this CharacterModel c, Constants.SKILL_CATEGORY saveCategory, string saveProficiencyLevel)
		{
			int saveMod = c.Proficiency_Bonus * Constants.SAVE_PROFICIENCIES[saveProficiencyLevel];
			switch (saveCategory)
			{
			    case Constants.SKILL_CATEGORY.Strength: return saveMod + c.Strength.Modifier;
			    case Constants.SKILL_CATEGORY.Dexterity: return saveMod + c.Dexterity.Modifier;
			    case Constants.SKILL_CATEGORY.Constitution: return saveMod + c.Constitution.Modifier;
			    case Constants.SKILL_CATEGORY.Intelligence: return saveMod + c.Intelligence.Modifier;
			    case Constants.SKILL_CATEGORY.Wisdom: return saveMod + c.Wisdom.Modifier;
			    case Constants.SKILL_CATEGORY.Charisma: return saveMod + c.Charisma.Modifier;
			    default: return int.MinValue;
			}
		}

		public static int GetSpellCastingDC(this int abilityScore, int proficiencyBonus)
		{
			return 8 + abilityScore.AbilityScoreToModifier() + proficiencyBonus;
		}
	}
}
