using Newtonsoft.Json;
using System.ComponentModel;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Helpers;

namespace TheTallTankardTavern.Models
{
	public class CharacterModel : CreatureModel
	{
		public class CoinPurseModel
		{
            [DisplayName("CP")]
			public int Copper_Pieces { get; set; }

            [DisplayName("SP")]
            public int Silver_Pieces { get; set; }

            [DisplayName("GP")]
            public int Gold_Pieces { get; set; }

            [DisplayName("PP")]
            public int Platinum_Pieces { get; set; }

            [DisplayName("Total")]
            public int Total => Platinum_Pieces * 1000 + Gold_Pieces * 100 + Silver_Pieces * 10 + Copper_Pieces;
		}

		public class Biography
		{
			public string Personality { get; set; } = "";

			public string Ideals { get; set; } = "";

			public string Bonds { get; set; } = "";

			public string Flaws { get; set; } = "";

			public int Age { get; set; }

			public string Birthday { get; set; } = "";

            public string Height { get; set; } = "";

			public string Weight { get; set; } = "";

			[DisplayName("Skin Colour")]
			public string Skin_Colour { get; set; } = "";

			[DisplayName("Eye Colour")]
			public string Eye_Colour { get; set; } = "";

			[DisplayName("Hair Colour")]
			public string Hair_Colour { get; set; } = "";

			public string Clothing { get; set; } = "";

			public string Other { get; set; } = "";

		}

        #region Private Variables
        private string _player_name;

		private string _class;

		private string _subclass;

		private string _race;

		private string _subrace;

		private string _alignment;

		private int[] _spell_slot_max;
        #endregion

        [DisplayName("Player Name")]
		public string Player_Name
		{
			get
			{
				return _player_name ?? "";
			}
			set
			{
				_player_name = value;
			}
		}

        [JsonIgnore]
        [DisplayName("Character Name")]
        public string Character_Name { get { return this.Name; } set { this.Name = value; } }

		public Biography Bio { get; set; } = new Biography();


		[DisplayName("Backstory Link")]
		public string Backstory_Link { get; set; } = "";


		[DisplayName("Class")]
		public string Class
		{
			get
			{
				return _class ?? "";
			}
			set
			{
				_class = value;
			}
		}

		[DisplayName("Subclass")]
		public string Subclass
		{
			get
			{
				return _subclass ?? "";
			}
			set
			{
				_subclass = value;
			}
		}

		[DisplayName("Level")]
		public int Level => this.ExpToLevel();

        [JsonIgnore]
        public string NextLevelXP
        {
            get
            {
                return this.LevelToExp();
            }
        }

		[JsonIgnore]
		public int NextLevelXPPercentage
		{
			get
			{
				return (int)((this.Experience_Points / int.Parse(this.NextLevelXP.Replace(",", ""))) * 100);
			}
		}

		public string BackgroundID { get; set; } = "";

        [DisplayName("Background")]
        [JsonIgnore]
        public string Background
        {
            get
            {
                return ApplicationSettings.BackgroundDataContext.GetModelFromID(this.BackgroundID)?.Name ?? "";
            }
        }


		[DisplayName("Race")]
		public string Race
		{
			get
			{
				return _race ?? "";
			}
			set
			{
				_race = value;
			}
		}

		[DisplayName("Subrace")]
		public string Subrace
		{
			get
			{
				return _subrace ?? "";
			}
			set
			{
				_subrace = value;
			}
		}

		[DisplayName("Alignment")]
		public string Alignment
		{
			get
			{
				return _alignment ?? "";
			}
			set
			{
				_alignment = value;
			}
		}

		[DisplayName("Experience Points")]
		public int Experience_Points { get; set; }

        [JsonIgnore]
		[DisplayName("Proficiency Bonus")]
		public int Proficiency_Bonus => this.GetProfBonus();

		[JsonIgnore]
		[DisplayName("Passive Perception")]
		public override int Passive_Perception => 10 + this.GetSkillModifider(Constants.SKILL_CATEGORY.Wisdom, base.Perception);

		[DisplayName("Hit Points")]
		public int Hit_Points_Remaining { get; set; }

        [JsonIgnore]
        [DisplayName("Max Hit Points")]
        public int MaxHitPoints { get { return this.GetTotalHP(); } }

        [DisplayName("Hit Dice")]
		public int Hit_Dice_Remaining { get; set; }

        [JsonIgnore]
        [DisplayName("Total Hit Dice")]
        public string TotalHitDice { get { return this.GetHitDice(); } }

        [DisplayName("Temp Hit Points")]
		public int Temp_Hit_Points { get; set; }

		[DisplayName("Ki Points")]
		public int Ki_Points { get; set; } = 0;

        [JsonIgnore]
        [DisplayName("Base AC")]
        public int BaseAC { get { return this.GetBaseAC(); } }

        [JsonIgnore]
        [DisplayName("Total AC")]
        public int TotalAC { get { return this.GetTotalAC(); } }

        [DisplayName("Coin Purse")]
		public CoinPurseModel Coin_Purse { get; set; } = new CoinPurseModel();


		[DisplayName("Equipment")]
		public EquipmentModel Equipment { get; set; } = new EquipmentModel();


		[DisplayName("Inventory")]
		public InventoryModel Inventory { get; set; } = new InventoryModel();


		[DisplayName("Features")]
		public FeatureListModel Features { get; set; } = new FeatureListModel();


		[DisplayName("Spells")]
		public SpellListModel Spells { get; set; } = new SpellListModel();


		public int[] SpellSlots { get; set; } = new int[9];


		[JsonIgnore]
		public int[] SpellSlotsMax
		{
			get
			{
				if (_spell_slot_max == null)
				{
					this.GetMaxSpellSlots();
				}
				return _spell_slot_max;
			}
			set
			{
				_spell_slot_max = value;
			}
		}

		[JsonIgnore]
		public int MaxSpellLevel
		{
			get
			{
				for (int i = 0; i < SpellSlotsMax.Length; i++)
				{
					if (SpellSlotsMax[i] <= 0)
					{
						return i;
					}
				}
				return 9;
			}
		}

		[DisplayName("Notes")]
		public string Notes { get; set; } = "";
	}
}
