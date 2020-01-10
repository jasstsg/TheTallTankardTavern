using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Controllers
{
	[Authenticated]
	public class SpellController : BaseController<SpellModel>
	{
        public SpellController() : base(SpellDataContext, FOLDER.Spells) { }

		public override IActionResult Create()
		{
			SpellModel Spell = new SpellModel()
			{
				ID = NewGUID,
				Classes = new List<string>(new string[] { "Bard", "Cleric", "Druid", "Sorcerer", "Warlock", "Wizard" }),
				Verbal_Components = true,
				Somatic_Components = true,
				Material_Components = true,
				IsHomebrew = true

			};
			return View("Create", Spell);
		}

		public IActionResult FilteredIndex(string filtertext)
		{
			if (filtertext.Equals("All"))
			{
				return Index();
			}

			return View("Index", DataContext.Where(s => s.Classes.Contains(filtertext)).OrderBy(s => s.Name).ToList());
		}
	}
}
