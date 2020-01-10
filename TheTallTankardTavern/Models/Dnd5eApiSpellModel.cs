namespace TheTallTankardTavern.Models
{
	public class Dnd5eApiSpellModel
	{
		public int Index
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string[] Desc
		{
			get;
			set;
		}

		public string[] Higher_Level
		{
			get;
			set;
		}

		public string Page
		{
			get;
			set;
		}

		public string Range
		{
			get;
			set;
		}

		public string[] Components
		{
			get;
			set;
		}

		public string Material
		{
			get;
			set;
		}

		public bool Ritual
		{
			get;
			set;
		}

		public string Duration
		{
			get;
			set;
		}

		public bool Concentration
		{
			get;
			set;
		}

		public string Casting_Time
		{
			get;
			set;
		}

		public int Level
		{
			get;
			set;
		}

		public NameAndUrlModel School
		{
			get;
			set;
		}

		public NameAndUrlModel[] Classes
		{
			get;
			set;
		}

		public NameAndUrlModel[] Subclasses
		{
			get;
			set;
		}

		public string Url
		{
			get;
			set;
		}
	}
}
