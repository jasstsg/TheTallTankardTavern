using System;
using System.Linq;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.Constants;
using static TheTallTankardTavern.Configuration.Constants.SpecialFeatures;
using static TheTallTankardTavern.Configuration.ApplicationSettings;

namespace TheTallTankardTavern.Helpers
{
	public static class CharacterModelHelper
	{
		private static readonly int SPELL_LEVELS = SPELL_SLOTS.FULL_CASTER.GetLength(1);

		public static int GetTotalHP(this CharacterModel Character)
		{
			string hitDie = Character.GetHitDice();
			int hitDieValue = int.Parse(hitDie.Substring(hitDie.IndexOf("D") + 1));
			return hitDieValue + (hitDieValue / 2 + 1 + Character.Constitution.Modifier) * (Character.Level - 1);
		}

		public static int GetBaseAC(this CharacterModel Character)
		{
			int AC = Character.Armour_Class + Character.Dexterity.Modifier;
			if (Character.Features.Contains(UNARMOURED_DEFENSE_BARBARIAN.ID))
			{
				AC += Character.Constitution.Modifier;
			}
			else if (Character.Features.Contains(UNARMOURED_DEFENSE_MONK.ID))
			{
				AC += Character.Wisdom.Modifier;
			}
			return AC;
		}

		public static int GetTotalAC(this CharacterModel Character)
		{
			string ArmourItemID = Character.Equipment.FirstOrDefault((string ItemID) => ItemDataContext.GetModelFromID(ItemID)?.Item_Type == "Armour");
			if (!string.IsNullOrEmpty(ArmourItemID))
			{
				ItemModel Armour = ItemDataContext.GetModelFromID(ArmourItemID);
                int AC = Character.Armour_Class + (Armour.Armour_Class_Value - 10);

                //Special rules
                AC += Character.Race.Equals("Warforged") ? Character.Proficiency_Bonus : 0;

				int DEX = Character.Dexterity.Modifier;
				switch (Armour.Weight_Class)
				{
				    case "Light": return AC + DEX;
				    case "Medium": return AC + ((DEX <= 2) ? DEX : 2);
				    case "Heavy": return AC;
				    default: return -1;
				}
			}
			return Character.GetBaseAC();
		}

		public static void GetMaxSpellSlots(this CharacterModel Character)
		{
			switch (Character.Class)
			{
			case "Bard":
			case "Cleric":
			case "Druid":
			case "Sorcerer":
			case "Wizard":
				Character.GetMaxSpellSlotsFromLevel(SPELL_SLOTS.FULL_CASTER);
				break;
			case "Paladin":
			case "Ranger":
				Character.GetMaxSpellSlotsFromLevel(SPELL_SLOTS.HALF_CASTER);
				break;
			case "Fighter":
				Character.GetMaxSpellSlotsFromLevel((Character.Subclass != "Elderich Knight") ? null : SPELL_SLOTS.THIRD_CASTER);
				break;
			case "Rogue":
				Character.GetMaxSpellSlotsFromLevel((Character.Subclass != "Arcane Trickster") ? null : SPELL_SLOTS.THIRD_CASTER);
				break;
			case "Warlock":
				Character.GetMaxSpellSlotsFromLevel(SPELL_SLOTS.PACT_CASTER);
				break;
			default:
				Character.GetMaxSpellSlotsFromLevel(null);
				break;
			}
		}

		public static string GetHitDice(this CharacterModel Character)
		{
			return $"{Character.Level}D{Character.GetHitDieValue()}";
		}

		public static void TakeLongRest(this CharacterModel Character)
		{
			Character.Hit_Points_Remaining = Character.GetTotalHP();
			Character.Hit_Dice_Remaining = Character.Level;
			for (int i = 0; i < Character.SpellSlots.Length; i++)
			{
				Character.SpellSlots[i] = Character.SpellSlotsMax[i];
			}
            Character.Temp_Hit_Points = 0;
		}

		public static void UseHitDie(this CharacterModel Character)
		{
			if (Character.Hit_Dice_Remaining > 0 && Character.Hit_Points_Remaining < Character.GetTotalHP())
			{
				Character.Hit_Dice_Remaining--;
				Character.Hit_Points_Remaining += Math.Min(Character.GetHitDieValue(), Character.GetTotalHP() - Character.Hit_Points_Remaining);
			}
		}

		private static void GetMaxSpellSlotsFromLevel(this CharacterModel Character, int[,] SpellSlotTable)
		{
			int[] maxSpellSlots = new int[SPELL_LEVELS];
			if (SpellSlotTable != null)
			{
				for (int i = 0; i < SPELL_LEVELS; i++)
				{
					maxSpellSlots[i] = SpellSlotTable[Character.Level - 1, i];
				}
			}
			Character.SpellSlotsMax = maxSpellSlots;
		}

		private static int GetHitDieValue(this CharacterModel Character)
		{
			switch (Character.Class)
			{
			case "Barbarian":
				return 12;
			case "Bard":
			case "Cleric":
			case "Druid":
			case "Monk":
			case "Rogue":
			case "Warlock":
				return 8;
			case "Fighter":
			case "Paladin":
			case "Ranger":
				return 10;
			case "Sorcerer":
			case "Wizard":
				return 6;
			default:
				return 0;
			}
		}
	}
}
