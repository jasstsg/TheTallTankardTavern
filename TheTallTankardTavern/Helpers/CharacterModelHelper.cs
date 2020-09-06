using System;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.Constants;
using static TheTallTankardTavern.Configuration.Constants.SpecialFeatures;
using TTT.Items;

namespace TheTallTankardTavern.Helpers
{
	public static class CharacterModelHelper
	{
		private static readonly int SPELL_LEVELS = SPELL_SLOTS.FULL_CASTER.GetLength(1);

		public static int GetTotalHP(this CharacterModel Character)
		{
			//n = Character Level
			//HP = (Hit Die Total) + (CON Mod) + (n-1)*[(Hit Die Avg) + (CON Mod)]
			string hitDie = Character.GetHitDice();
			int hitDieValue = int.Parse(hitDie.Substring(hitDie.IndexOf("D") + 1));
			int hitDieAvg = (hitDieValue / 2) + 1;
			return hitDieValue + Character.Constitution.Modifier + ((Character.Level - 1) * (hitDieAvg + Character.Constitution.Modifier));
		}

		public static int GetBaseAC(this CharacterModel Character)
		{
			int AC = Character.Armour_Class + Character.Dexterity.Modifier + (Character.Race.Equals("Warforged") ? 1 : 0);
			if (Character.HasFeature(UNARMOURED_DEFENSE_BARBARIAN))
			{
				AC += Character.Constitution.Modifier;
			}
			else if (Character.HasFeature(UNARMOURED_DEFENSE_MONK))
			{
				AC += Character.Wisdom.Modifier;
			}
			return AC;
		}

		public static int GetTotalAC(this CharacterModel Character)
		{
			int totalAC = 0;
			if (Character.Equipment.Armour != null)
			{
                int AC = Character.Armour_Class + (Character.Equipment.Armour.Armour.ArmourClass - 10);

                //Special rules
                //AC += Character.Race.Equals("Warforged") ? Character.Proficiency_Bonus : 0; //Old WGtE style
                AC += Character.Race.Equals("Warforged") ? 1 : 0; //New Eberron source book style

				int DEX = Character.Dexterity.Modifier;
				if (Character.Equipment.Armour.Type.Equals(ItemType.LightArmour)) { totalAC = AC + DEX; }
				else if (Character.Equipment.Armour.Type.Equals(ItemType.MediumArmour)) { totalAC = AC + ((DEX <= 2) ? DEX : 2); }
				else if (Character.Equipment.Armour.Type.Equals(ItemType.HeavyArmour)) { totalAC = AC; }
				else { totalAC = 0; }
			}
			else
            {
				totalAC = Character.GetBaseAC();

			}
			return totalAC + (Character.Equipment.Shield != null ? Character.Equipment.Shield.Armour.ArmourClass : 0);
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
			Character.Hit_Points_Remaining = Character.GetTotalHP();    //Reset HP
			Character.Hit_Dice_Remaining = Character.Level;             //Reset Hit Dice
			for (int i = 0; i < Character.SpellSlots.Length; i++)       //Reset Spell Slots
			{
				Character.SpellSlots[i] = Character.SpellSlotsMax[i];
			}
            Character.Temp_Hit_Points = 0;	//Reset Temporary Hit Points
			Character.RestorePoints();		//Restore class specific points
		}

		public static void RestorePoints(this CharacterModel Character)
		{
			Character.Ki_Points = Character.Level;                      //Reset Ki Points
			Character.Sorcery_Points = Character.Level;                 //Reset Sorcery Points
			Character.Wild_Shapes = 2;					                //Reset wild shapes
		}

		public static bool HasFeature(this CharacterModel Character, FeatureModel Feature)
        {
			return Character.Features.Contains(Feature.ID);
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

		public static int CarryCapacity(this CharacterModel Character)
        {
			return (Character.Strength.Score + Character.Constitution.Score) * 5;
        }

		public static bool IsEncumbered(this CharacterModel Character)
        {
			return Character.Inventory.CurrentWeight > Character.CarryCapacity();
        }

		public static bool IsProficientWith(this CharacterModel Character, ItemModel Item)
        {
			if (Item != null && 
				(Item.Type.Category.Equals(ItemTypeCategory.Shield) || 
				Item.Type.Category.Equals(ItemTypeCategory.Armour) ||
                Item.Type.Category.Equals(ItemTypeCategory.Weapon)))
            {
				return Character.IsProficientWith(Item.Type);
			}
			else
            {
				return false;
            }
        }

		public static bool IsProficientWith(this CharacterModel Character, ItemType itemType)
        {
			bool isProficient;
			Character.WeaponArmourProficiencies.TryGetValue(itemType, out isProficient);
			return isProficient;
		}

		public static int GetAttackBonus(this CharacterModel Character, ItemModel Weapon)
        {
			return Character.GetDamageBonus(Weapon) + (Character.IsProficientWith(Weapon) ? Character.Proficiency_Bonus : 0);
        }

		private static int GetDamageBonus(this CharacterModel Character, ItemModel Weapon)
		{
			int damage = Weapon.Weapon.Plus;

			if (Weapon.Type.Equals(ItemType.SimpleRangedWeapon) || Weapon.Type.Equals(ItemType.MartialRangedWeapon) ||
				Weapon.Weapon.Properties.Finesse.Enabled || Character.IsMonkAndHoldingMonkWeapon(Weapon))
			{
				damage += (Character.Dexterity.Modifier > Character.Strength.Modifier) ? 
					Character.Dexterity.Modifier : Character.Strength.Modifier;
			}
			else if (Weapon.Type.Equals(ItemType.SimpleMeleeWeapon) || Weapon.Type.Equals(ItemType.MartialMeleeWeapon))
			{
				damage += Character.Strength.Modifier;
			}
			return damage;
		}

		public static string GetDamageString(this CharacterModel Character, ItemModel Weapon, bool isOffHand = false)
        {
			if (isOffHand)
            {
				return $"{Weapon.Weapon.Damage} + {Weapon.Weapon.Plus}";
			}
            else
			{
				return $"{Weapon.Weapon.Damage} + {Character.GetDamageBonus(Weapon)}";
			}
		}

		public static bool IsArmourTooHeavy(this CharacterModel Character, ItemModel Armour)
        {
			return Armour != null && Character.Strength.Score < Armour.Armour.StrengthRequired;
        }

		public static bool IsMonkAndHoldingMonkWeapon(this CharacterModel Character, ItemModel Item)
        {
			return Character.Class.ToLower().Contains("monk") && MONK_WEAPONS.Contains(Item.Type);
        }
	}
}
