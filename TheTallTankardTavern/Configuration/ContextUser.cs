using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.Constants;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using System.Linq;

namespace TheTallTankardTavern.Configuration
{
	public static class ContextUser
	{
		private const string IS_AUTHENTICATED = "IsAuthenticated";

		private const string CONTEXT_USER = "ContextUser";

		private static ISession Session => ApplicationSettings.HttpContextAccessor.HttpContext.Session;

		public static bool IsAuthenticated => Session.Get<bool>(IS_AUTHENTICATED);

		public static UserModel Current => Session.Get<UserModel>(CONTEXT_USER);

		public static void Set(UserModel User)
		{
			Session.Set(CONTEXT_USER, User);
		}

		public static void RemoveContextUser()
		{
			Session.Remove(CONTEXT_USER);
		}

		public static void SetAuthentication(bool isAuthenticated)
		{
			Session.Set(IS_AUTHENTICATED, isAuthenticated);
		}

		public static bool HasRole(ROLES role)
		{
			return (Current != null) ? (IsAdministrator || Current.Role.Equals(role)) : false;
		}

		public static bool HasAnyRoles(params ROLES[] roles)
		{
			foreach (ROLES role in roles)
			{
				if (HasRole(role))
				{
					return true;
				}
			}
			return false;
		}

		public static bool HasAllRoles(params ROLES[] roles)
		{
			foreach (ROLES role in roles)
			{
				if (!HasRole(role))
				{
					return false;
				}
			}
			return true;
		}

		public static bool HasUsername(string username)
		{
			return Current.Username.Equals(username);
		}

		public static List<CharacterModel> Characters
		{
			get
			{
				if (HasAnyRoles(ROLES.Administrator, ROLES.Dungeon_Master, ROLES.Dungeon_Master_Readonly))
				{
					return CharacterDataContext.OrderBy(c => c.Name).ToList();
				}
				else
				{
					return CharacterDataContext.FindAll(c => HasUsername(c.Player_Name)).OrderBy(c => c.Name).ToList();
				}
			}
		}

		public static bool IsNull { get { return Current == null; } }

		public static bool IsAdministrator { get { return Current?.Role.Equals(ROLES.Administrator) ?? false; } }

		public static bool IsDungeonMaster { get { return HasAnyRoles(ROLES.Dungeon_Master, ROLES.Dungeon_Master_Readonly); } }

		public static bool IsAdminOrDM { get { return IsAdministrator || IsDungeonMaster; } }
	}
}
