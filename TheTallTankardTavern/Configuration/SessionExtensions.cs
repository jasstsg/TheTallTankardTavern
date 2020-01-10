using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TheTallTankardTavern.Configuration
{
	public static class SessionExtensions
	{
		public static void Set<T>(this ISession Session, string key, T value)
		{
			Session.SetString(key, JsonConvert.SerializeObject(value));
		}

		public static T Get<T>(this ISession Session, string key)
		{
			string value = Session.GetString(key);
			if (string.IsNullOrEmpty(value))
			{
				return default(T);
			}
			return JsonConvert.DeserializeObject<T>(value);
		}
	}
}
