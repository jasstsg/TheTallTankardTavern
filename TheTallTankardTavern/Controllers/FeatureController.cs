using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Helpers;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Controllers
{
	[Authenticated]
	public class FeatureController : BaseController<FeatureModel>
    {
        public FeatureController() : base(FeatureDataContext, FOLDER.Features) { }
        public override IActionResult Create()
        {
            return View("Create", new FeatureModel
            {
                ID = NewGUID
            });
        }

        public IActionResult FilteredIndex(string filtertext)
        {
            IEnumerable<FeatureModel> Features;

            switch (filtertext)
            {
                case "All": return Index();
                case "ClassFeatures": Features = DataContext.Where(f => f.IsClassFeature); break;
                case "NonClassFeatures": Features = DataContext.Where(f => !f.IsClassFeature); break;
                default: Features = DataContext.Where(f => f.Class.Equals(filtertext)); break;
            }
            return View("Index", Features.OrderBy(f => f.Name).ToList());
        }
	}
}
