using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using TheTallTankardTavern.Configuration;

namespace TheTallTankardTavern.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class AuthenticatedAttribute : Attribute, IAuthorizationFilter, IFilterMetadata
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (ContextUser.IsNull)
			{
				context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
			}
		}
	}
}
