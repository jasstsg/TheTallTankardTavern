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

        [HttpPost]
        public IActionResult Save(FeatureModel Model, string submit)
        {
            return SaveModel(Model, submit);
        }

        [HttpPost]
        public IActionResult FilteredIndex(string searchtext)
        {
            searchtext = searchtext?.ToLower();

            string selectedFilter = Request.Form["filter-dropdown"];

            IEnumerable<FeatureModel> Features = !string.IsNullOrEmpty(searchtext) ?
                DataContext.Where(f => f.IsMatch(searchtext)).ToList() :
                Features = DataContext.Where(f => true);

            switch (selectedFilter)
            {
                case "ClassFeatures": Features = Features.Where(f => f.IsClassFeature); break;
                case "NonClassFeatures": Features = Features.Where(f => !f.IsClassFeature); break;
                default: break;
            }

            ViewData["searchtext"] = searchtext;

            return View("Index", Features.OrderBy(f => f.Name).ToList());
        }
	}

    public static class FeatureControllerExtensions
    {
        public static bool IsMatch(this FeatureModel feature, string searchtext)
        {
            return feature.Name.SafeContains(searchtext) ||
                feature.Prerequisite.SafeContains(searchtext) ||
                feature.Class.SafeContains(searchtext) ||
                feature.Subclass.SafeContains(searchtext);
        }

        private static bool SafeContains(this string searchProperty, string searchtext)
        {
            return string.IsNullOrEmpty(searchProperty) ? false : searchProperty.ToLower().Contains(searchtext);
        }
    }
}
