using System;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;
using static TheTallTankardTavern.Configuration.Constants.SpecialFeatures;
using TTT.Items;
using System.Linq;

namespace TheTallTankardTavern.Helpers
{
	public static class CharacterModelHelper
	{
		private static readonly int SPELL_LEVELS = SPELL_SLOTS.FULL_CASTER.GetLength(1);

        /// <summary>
        /// How to calculate total HP
        /// =========================
        /// 
        /// Variables
        /// ---------
        /// n = Character level
        /// HP = Total hit points
		/// 
        /// Formula
        /// -------
        /// HP = (Hit Die Total) + (CON Mod) + (n-1)*[(Hit Die Avg) + (CON Mod)] + <Bonuses>
		/// 
		/// Possible Bonuses
		/// ----------------
		/// - 2 x n (if character has the 'Tough' Feature
		/// 
        /// </summary>
        public static int GetTotalHP(this CharacterModel Character)
		{
			string hitDie = Character.GetHitDice();
			int hitDieValue = int.Parse(hitDie.Substring(hitDie.IndexOf("D") + 1));
			int hitDieAvg = (hitDieValue / 2) + 1;

			int totalHP = 0;
			totalHP += hitDieValue + Character.Constitution.Modifier;							// Starting HP at level 1
			totalHP += (Character.Level - 1) * (hitDieAvg + Character.Constitution.Modifier);   // HP for level 2+
			totalHP += Character.HasFeature(TOUGH) ? (Character.Level * 2) : 0;					// Bonus from 'Tough' feat

            return totalHP;
		}

		public static int GetBaseAC(this CharacterModel Character)
		{
			int AC = Character.Armour_Class + Character.Dexterity.Modifier;

			//SPECIAL RULES

			//1. Warforged get +1 AC
			//AC += Character.Race.Equals("Warforged") ? Character.Proficiency_Bonus : 0; //Old WGtE style
			AC += Character.Race.Equals("Warforged") ? 1 : 0; //New Eberron source book style

			//2. Unarmoured defense features add mods based on class when not wearing armour
			if (Character.HasFeature(UNARMOURED_DEFENSE_BARBARIAN) && Character.Equipment.Armour == null)
			{
				AC += Character.Constitution.Modifier;
			}
			else if (Character.HasFeature(UNARMOURED_DEFENSE_MONK) && Character.Equipment.Armour == null)
			{
				AC += Character.Wisdom.Modifier;
			}
			return AC;
		}

		private static int GetArmourBonus(this CharacterModel Character)
        {
			int AC = Character.Equipment.Armour.Armour.ArmourClass - 10;
			int DEX = Character.Dexterity.Modifier;
			ItemType ArmourType = Character.Equipment.Armour.Type;

			if (ArmourType.Equals(ItemType.LightArmour))
			{
				return AC + DEX;
			}
			else if (ArmourType.Equals(ItemType.MediumArmour))
            {
				return AC + ((DEX <= 2) ? DEX : 2);
			}
			else if (ArmourType.Equals(ItemType.HeavyArmour))
			{
				return AC;
			}
			else
            {
				return -100;
            }
		}

		public static int GetTotalAC(this CharacterModel Character)
		{
			int totalAC = Character.GetBaseAC();

			if (Character.Equipment.Armour != null)
			{
				//Subtract the DEX mod to remove it from the base AC, then add the correct amount based on the armour.
				totalAC += Character.GetArmourBonus() - Character.Dexterity.Modifier;
			}

			//SPECIAL RULES

			//1. Using a shield
			totalAC += Character.Equipment.Shield != null ? Character.Equipment.Shield.Armour.ArmourClass : 0;

			//2. Dual wielders get +1 AC while dual wielding melee weapons in each hand
			totalAC += (Character.HasFeature(SpecialFeatures.DUAL_WIELDER) && Character.Equipment.IsDualWielding()) ? 1 : 0;

			return totalAC;
		}

		private static bool IsDualWielding(this EquipmentModel Equipment)
        {
			return (Equipment.OffHand?.Type.IsMelee() ?? false) && (Equipment.MainHand?.Type.IsMelee() ?? false);
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
			Character.ResetAllHealthPoints();
			Character.ResetSpellSlots();
			Character.ResetUniquePoints();
		}

		private static void ResetAllHealthPoints(this CharacterModel Character)
        {
			Character.Hit_Points_Remaining = Character.GetTotalHP();    //Reset HP
			Character.Hit_Dice_Remaining = Character.Level;             //Reset Hit Dice
			Character.Temp_Hit_Points = 0;								//Reset Temporary Hit Points
		}

		private static void ResetSpellSlots(this CharacterModel Character)
		{
			for (int i = 0; i < Character.SpellSlots.Length; i++)       //Reset Spell Slots
			{
				Character.SpellSlots[i] = Character.SpellSlotsMax[i];
			}
		}

		public static void ResetUniquePoints(this CharacterModel Character)
		{
			Character.Lay_On_Hands_Pool = Character.Level * 5;	//Reset Lay On Hands Pool
			Character.Ki_Points = Character.Level;				//Reset Ki Points
			Character.Sorcery_Points = Character.Level;			//Reset Sorcery Points
			Character.Wild_Shapes = 2;							//Reset wild shapes
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

		public static int GetHitDieValue(this CharacterModel Character)
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

			if (Weapon.Weapon.Properties.Finesse.Enabled || Character.IsMonkAndHoldingMonkWeapon(Weapon))
            {
				damage += Math.Max(Character.Strength.Modifier, Character.Dexterity.Modifier);
            }
			else if (Weapon.Type.Is(ItemType.SimpleRangedWeapon) || Weapon.Type.Is(ItemType.MartialRangedWeapon))
			{
				damage += Character.Dexterity.Modifier;
			}
			else if (Weapon.Type.Is(ItemType.SimpleMeleeWeapon) || Weapon.Type.Is(ItemType.MartialMeleeWeapon))
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

		public static int GetSpellCasterAttackModifier(this CharacterModel Character)
        {
			int modifier = Character.Proficiency_Bonus;
            switch (Character.Class)
            {
				case "Wizard":
					modifier += Character.Intelligence.Modifier; break;
				case "Cleric":
				case "Druid":
				case "Ranger":
					modifier += Character.Wisdom.Modifier; break;
				case "Bard":
				case "Warlock":
				case "Paladin":
				case "Sorcerer":
					modifier += Character.Charisma.Modifier; break;
				default:
					modifier += 0; break;
			}
			return modifier;
        }

		public static int GetSpellCasterSaveDC(this CharacterModel Character)
        {
			return Character.GetSpellCasterAttackModifier() + 8;
        }

		public static bool IsSpellCaster(this CharacterModel Character)
        {
			switch (Character.Class)
			{
				case "Bard":
				case "Cleric":
				case "Druid":
				case "Paladin":
				case "Ranger":
				case "Sorcerer":
				case "Warlock":
				case "Wizard":
					return true;
				default:
					return false;
			}
		}

		public static int UnarmedStrikeAttack(this CharacterModel Character)
        {
			if (Character.Class == "Monk" && Character.Dexterity.Score > Character.Strength.Score)
            {
				return Character.Proficiency_Bonus + Character.Dexterity.Modifier;
            }
			return  Character.Proficiency_Bonus + Character.Strength.Modifier;
        }

		public static string UnarmedStrikeDamageString(this CharacterModel Character)
        {
			if (Character.Class == "Monk")
            {
				string damageDie;
				int damageBonus = Character.Dexterity.Score > Character.Strength.Score ?
					Character.Dexterity.Modifier : Character.Strength.Modifier;
				if (Character.Level <= 4) { damageDie = "1D4"; }
				else if (Character.Level <= 10) { damageDie = "1D6"; }
				else if (Character.Level <= 16) { damageDie = "1D8"; }
				else if (Character.Level <= 20) { damageDie = "1D10"; }
				else { damageDie = "1D12"; }

				return $"{damageDie} + {damageBonus} Bludgeoning";
            }
			return $"{1 + Character.Strength.Modifier} Bludgeoning";
        }

		public static int GetEldritchInvocations(this CharacterModel Character)
        {
			switch (Character.Level)
            {
				case 2:
				case 3:
				case 4:
					return 2;
				case 5:
				case 6:
					return 3;
				case 7:
				case 8:
					return 4;
				case 9:
				case 10:
				case 11:
					return 5;
				case 12:
				case 13:
				case 14:
					return 6;
				case 15:
				case 16:
				case 17:
					return 7;
				case 18:
				case 19:
				case 20:
					return 8;
				case 21:
				case 22:
				case 23:
				case 24:
					return 9;
				case 25:
				case 26:
				case 27:
				case 28:
				case 29:
				case 30:
					return 10;
				default:
					return 0;
            }
        }

		public static string GetWildShapeCR(this CharacterModel Character)
        {
			if (Character.Level >= 8)
            {
				return "1";
			}
			if (Character.Level >= 4)
			{
				return "1/2";
			}
			else
            {
				return "1/4";
            }
		}

		public static string GetWildShapeLimits(this CharacterModel Character)
		{
			if (Character.Level >= 8)
			{
				return "";
			}
			if (Character.Level >= 4)
			{
				return "(No fly speed)";
			}
			else
			{
				return "(No fly/swim speed)";
			}
		}

		public static string GetPreparedSpellsCount(this CharacterModel Character)
        {
			int preparedSpells = Character.Spells.Where(s => SpellDataContext.GetModelFromID(s).Level > 0).Count();
			switch (Character.Class.ToLower())
            {
				case "cleric":
				case "druid":
					return $" (Spells prepared: {preparedSpells} / {Character.Wisdom.Modifier + Character.Level})";
				case "paladin":
					return $" (Spells prepared: {preparedSpells} / {Character.Charisma.Modifier + (Character.Level / 2)})";
				case "wizard":
					return $" (Spells prepared: {preparedSpells} / {Character.Intelligence.Modifier + Character.Level})";
				default:
					return "";


			}
        }
	}
}
