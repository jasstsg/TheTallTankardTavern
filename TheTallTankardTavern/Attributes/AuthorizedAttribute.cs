using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class AuthorizedAttribute : Attribute, IAuthorizationFilter, IFilterMetadata
	{
		public ROLES[] AllowedRoles { private get; set; }

		public AuthorizedAttribute(params ROLES[] allowedRoles)
		{
			this.AllowedRoles = allowedRoles;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (ContextUser.IsNull || !ContextUser.HasAnyRoles(this.AllowedRoles))
			{
				context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
			}
		}
	}
}
