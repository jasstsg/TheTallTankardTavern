using Newtonsoft.Json;
using System.ComponentModel;
using TTT.Common.Abstractions;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Models
{
	public class CreatureModel : BaseModel
	{
		[DisplayName("STR")]
		public Stat Strength { get; set; } = new Stat(10);


		[DisplayName("DEX")]
		public Stat Dexterity { get; set; } = new Stat(10);


        [DisplayName("CON")]
		public Stat Constitution { get; set; } = new Stat(10);


        [DisplayName("INT")]
		public Stat Intelligence { get; set; } = new Stat(10);


        [DisplayName("WIS")]
		public Stat Wisdom { get; set; } = new Stat(10);


        [DisplayName("CHA")]
		public Stat Charisma { get; set; } = new Stat(10);


        [DisplayName("Strength")]
		public string Strength_Save { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Dexterity")]
		public string Dexterity_Save { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Constitution")]
		public string Constitution_Save { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Intelligence")]
		public string Intelligence_Save { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Wisdom")]
		public string Wisdom_Save { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("CHARISMA")]
		public string Charisma_Save { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Acrobatics")]
		public string Acrobatics { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Animal Handling")]
		public string Animal_Handling { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Arcana")]
		public string Arcana { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Athletics")]
		public string Athletics { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Deception")]
		public string Deception { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("History")]
		public string History { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Insight")]
		public string Insight { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Intimidation")]
		public string Intimidation { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Investigation")]
		public string Investigation { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Medicine")]
		public string Medicine { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Nature")]
		public string Nature { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Perception")]
		public string Perception { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Performance")]
		public string Performance { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Persuasion")]
		public string Persuasion { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Religion")]
		public string Religion { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Sleight Of Hand")]
		public string Sleight_Of_Hand { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Stealth")]
		public string Stealth { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Survival")]
		public string Survival { get; set; } = PROFICIENCY.NOT_PROFICIENT;


		[DisplayName("Passive Perception")]
		public virtual int Passive_Perception { get; set; }

		[DisplayName("Armour Class")]
		public virtual int Armour_Class => 10;

		[DisplayName("Initiative Bonus")]
		public int Initiative => Dexterity.Modifier;

		[DisplayName("Speed")]
		public string Speed { get; set; } = "30 ft.";

        [DisplayName("Languages")]
        public CheckboxEnumListModel<LANGUAGES> Languages { get; set; }

		public class Stat
		{
			public Stat(int score)
			{
				this.Score = score;
			}

			public int Score { get; set; } = 10;

			[JsonIgnore]
			public int Modifier { get { return (this.Score / 2) - 5; } }
		}
	}
}
