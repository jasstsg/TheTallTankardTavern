using Microsoft.AspNetCore.Mvc;
using TheTallTankardTavern.Helpers;

namespace TheTallTankardTavern.Configuration
{
	public class ConfigurationApiController : Controller
	{
		[HttpGet]
		public JsonResult GetSubclasses(string selected)
		{
			return Json(SelectListHelper.GetSubclassSelectList(selected));
		}

		[HttpGet]
		public JsonResult GetSubraces(string selected)
		{
			return Json(SelectListHelper.GetSubraceSelectList(selected));
		}
	}
}
