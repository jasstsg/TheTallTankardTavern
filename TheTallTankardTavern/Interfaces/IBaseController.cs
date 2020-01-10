using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheTallTankardTavern.Enumerators;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Interfaces
{
	public interface IBaseController
	{
		IActionResult Index<T>(IEnumerable<T> DataContext) where T : BaseModel;

		IActionResult Details<T>(IEnumerable<T> DataContext, string ID) where T : BaseModel;

		IActionResult Edit<T>(List<T> DataContext, string ID) where T : BaseModel;

		[HttpGet]
		IActionResult PreDelete<T>(List<T> DataContext, string ID, string message = "") where T : BaseModel;

		[HttpPost]
		IActionResult Delete<T>(List<T> DataContext, FOLDER folder, string ID, string modelObjectName) where T : BaseModel;
	}
}
