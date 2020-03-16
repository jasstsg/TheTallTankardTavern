using Microsoft.AspNetCore.Html;

namespace TheTallTankardTavern.Helpers
{
	public static class DnDStringHelper
	{
		public static string AddBraces(this string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return "";
			}
			return "(" + str + ")";
		}

		public static string RemoveBraces(this string str)
		{
			str.Replace("(", "");
			str.Replace(")", "");
			return str;
		}

		public static string FormatNumber(this int num)
		{
			return $"{num:n0}";
		}

		public static string FormatMoney(this int num)
		{
			string numStr = num.ToString();
			if (numStr.Length > 2)
			{
				return numStr.Insert(numStr.Length - 2, ",");
			}
			else if (numStr.Length == 2)
			{
				return $"0,{numStr}";
			}
			else
			{
				return $"0,0{numStr}";
			}
		}

		public static string ToHtmlString(this string str)
		{
			return new HtmlString(str).Value;
		}

		public static string AddSign(this int num)
		{
			return (num > 0) ? $"+{num}" : $"{num}";
		}
	}
}
