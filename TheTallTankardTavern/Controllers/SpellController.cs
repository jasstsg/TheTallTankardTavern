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

		[HttpPost]
		public IActionResult FilteredIndex(string searchtext)
		{
			IEnumerable<SpellModel> Spells = !string.IsNullOrEmpty(searchtext) ?
				DataContext.Where(s => s.IsMatch(searchtext)).ToList() :
				Spells = DataContext.Where(s => true);

			ViewData["searchtext"] = searchtext;

			return View("Index", Spells.OrderBy(s => s.Name).ToList());
		}
	}

	public static class SpellControllerExtensions
	{
		public static bool IsMatch(this SpellModel spell, string searchtext)
		{
			return spell.Name.SafeContains(searchtext) ||
				spell.School.SafeContains(searchtext) ||
				spell.Classes.Any(c => c.SafeContains(searchtext));
		}

		private static bool SafeContains(this string searchProperty, string searchtext)
		{
			return string.IsNullOrEmpty(searchProperty) ? false : searchProperty.ToLower().Contains(searchtext);
		}
	}
}
