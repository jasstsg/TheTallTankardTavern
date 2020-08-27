using TTT.Common.Abstractions;

namespace TheTallTankardTavern.Models
{
	public class SpellListModel : BaseListModel<string>
	{
		public void Add(SpellModel Spell)
		{
			base.Add(Spell.ID);
		}
	}
}
