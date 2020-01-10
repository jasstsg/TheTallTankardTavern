namespace TheTallTankardTavern.Models
{
	public class Dnd5eApiFeatureModel
	{
		public NameAndUrlModel Class
		{
			get;
			set;
		}

		public NameAndUrlModel Subclass
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int Level
		{
			get;
			set;
		}

		public string[] Desc
		{
			get;
			set;
		}
	}
}
