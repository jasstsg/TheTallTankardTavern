using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;
using TTT.Enumerator;
using TTT.Common.Abstractions;
using TTT.Items;
using System.Reflection;
using System;
using TTT.Common;

namespace TheTallTankardTavern.Helpers
{
	public static class SelectListHelper
	{
		public static SelectList GetPartyMembers()
        {
			return new SelectList(PartyDataContext.Single().GetAllPartyMemberCharacters(), "ID", "Name");
        }

		public static SelectList GetDataContextSelectList<T>(this IEnumerable<T> DataContext) where T : BaseModel
		{
			return new SelectList(DataContext.OrderBy(x => x.Name), "ID", "Name");
		}

		public static SelectList GetMaterialsForItemsSelectList()
		{
			return new SelectList(MaterialDataContext.OrderBy(m => m.Cost_Per_Unit), "ID", "Name");
		}

		public static SelectList GetFeaturesForBackgroundsSelectList()
		{
			return GetDataContextSelectList(FeatureDataContext.Where(f => !f.IsClassFeature).ToList());
		}

		public static SelectList GetClassSelectList()
		{
			return new SelectList(ApplicationSettings.ConfigurationSettings.Classes.Keys);
		}

		public static SelectList GetSubclassSelectList(string selectedClass)
		{
			if (string.IsNullOrEmpty(selectedClass))
			{
				return GetSubclassSelectList(ApplicationSettings.ConfigurationSettings.Classes.GetFirstKey());
			}
			return new SelectList(ApplicationSettings.ConfigurationSettings.Classes[selectedClass].OrderBy((string e) => e));
		}

		public static SelectList GetRaceSelectList()
		{
			return new SelectList(ApplicationSettings.ConfigurationSettings.Races.Keys);
		}

		public static SelectList GetSubraceSelectList(string selectedRace)
		{
			if (string.IsNullOrEmpty(selectedRace))
			{
				return GetSubraceSelectList(ApplicationSettings.ConfigurationSettings.Races.GetFirstKey());
			}
			return new SelectList(ApplicationSettings.ConfigurationSettings.Races[selectedRace].OrderBy((string e) => e));
		}

		public static SelectList GetBackgroundSelectList()
		{
			return new SelectList(ApplicationSettings.BackgroundDataContext.OrderBy((BackgroundModel b) => b.Name), "ID", "Name");
		}

		public static SelectList GetAlignmentSelectList()
		{
			return new SelectList(ApplicationSettings.ConfigurationSettings.Alignments.GetFirstValue());
		}

		public static SelectList GetSkillSelectList()
		{
			return new SelectList(Constants.SKILL_LEVELS.Keys);
		}

		public static SelectList GetSaveProficiencySelectList()
		{
			return new SelectList(SAVE_PROFICIENCIES.Keys);
		}

		public static SelectList GetWeaponWeightClassSelectList()
		{
			return new SelectList(Constants.WEAPON_WEIGHT_CLASS.Keys);
		}

		public static SelectList GetItemTypesSelectList()
		{
			Type type = typeof(ItemType);
			FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
			IEnumerable<string> enumerableList = fields.Select(f => f.GetValue(null).ToString());
			return new SelectList(enumerableList);
		}

		public static SelectList GetDamageTypeSelectList()
		{
			return new SelectList(typeof(DamageType).EnumToStringArray());
		}

		public static SelectList GetDamageDieSelectList()
		{
			return new SelectList(typeof(Die).EnumToStringArray("_", ""));
		}

		public static SelectList GetRoleSelectList()
		{
			return new SelectList(typeof(Constants.ROLES).GetEnumValues());
		}

		public static SelectList GetMagicSchoolSelectList()
		{
			return new SelectList(typeof(Constants.MAGIC_SCHOOLS).GetEnumValues());
		}

		private static string GetFirstKey(this Dictionary<string, string[]> Dict)
		{
			return Dict.Keys.First();
		}

		private static IEnumerable<string> GetFirstValue(this Dictionary<string, string[]> Dict)
		{
			return Dict[Dict.GetFirstKey()];
		}
	}
}
