using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;
using System;
using TheTallTankardTavern.Models;
using TTT.Enumerator;

namespace TheTallTankardTavern.Helpers
{
	public static class ControllerHelper
	{
		public static void InitializeCheckBoxes(this BaseListModel<CheckBoxModel> CheckBoxList, Type enumType)
		{
			string[] array = enumType.EnumToStringArray();
			foreach (string s in array)
			{
				CheckBoxList.Add(new CheckBoxModel
				{
					Name = s,
					IsChecked = false
				});
			}
		}

		/* 
		 * ViewExists shamelessly stolen from:
		 * https://stackoverflow.com/questions/946990/does-a-view-exist-in-asp-net-mvc
		 * https://gist.github.com/ygrenier/4e46b5de3e4a77ea62fe5f0d6488fa2a
		 */
		public static bool ViewExists(this ControllerBase controller, string name)
		{
			var services = controller.HttpContext.RequestServices;
			var viewEngine = services.GetRequiredService<ICompositeViewEngine>();
			var result = viewEngine.GetView(null, name, true);
			if (!result.Success)
			{
				result = viewEngine.FindView(controller.ControllerContext, name, true);
			}
			return result.Success;
		}

		public static JsonResult JsonSuccessTrue(this Controller controller)
		{
			return controller.Json(new { success = true });
		}

		public static JsonResult JsonSuccessFalse(this Controller controller)
		{
			return controller.Json(new { success = false });
		}
	}
}
