using System;
using System.Collections.Generic;

namespace TTT.Enumerator
{
	public static class EnumeratorExtensions
	{
		public static string EnumToString(this Enum Enumerator)
		{
			return Enumerator.ToString().Replace('_', ' ');
		}

		public static string EnumToString(this Enum Enumerator, char oldChar, char newChar)
		{
			return Enumerator.ToString().Replace(oldChar, newChar);
		}

		public static string EnumToString(this Enum Enumerator, string oldString, string newString)
		{
			return Enumerator.ToString().Replace(oldString, newString);
		}

		public static string[] EnumToStringArray(this Type enumType)
		{
			List<string> EnumStringList = new List<string>();
			string[] names = Enum.GetNames(enumType);
			foreach (string s in names)
			{
				EnumStringList.Add(s.Replace('_', ' '));
			}
			return EnumStringList.ToArray();
		}

		public static string[] EnumToStringArray(this Type enumType, char oldChar, char newChar)
		{
			List<string> EnumStringList = new List<string>();
			string[] names = Enum.GetNames(enumType);
			foreach (string s in names)
			{
				EnumStringList.Add(s.Replace(oldChar, newChar));
			}
			return EnumStringList.ToArray();
		}

		public static string[] EnumToStringArray(this Type enumType, string oldString, string newString)
		{
			List<string> EnumStringList = new List<string>();
			string[] names = Enum.GetNames(enumType);
			foreach (string s in names)
			{
				EnumStringList.Add(s.Replace(oldString, newString));
			}
			return EnumStringList.ToArray();
		}

		public static TEnum[] EnumToEnumArray<TEnum>(this Type enumType)
		{
			return (TEnum[])Enum.GetValues(enumType);
		}

		public static int EnumCount(this Type enumType)
		{
			return enumType.EnumToStringArray().Length;
		}

		public static bool Equals(this Enum @enum, string str)
		{
			return @enum.EnumToString() == str;
		}

		public static bool Equals(this Enum @enum, char oldChar, char newChar, string str)
		{
			return @enum.EnumToString(oldChar, newChar) == str;
		}

		public static bool Equals(this Enum @enum, string oldString, string newString, string str)
		{
			return @enum.EnumToString(oldString, newString) == str;
		}

		public static bool Equals(this Enum @enum, int num)
		{
			return (int)Enum.ToObject(typeof(int), @enum) == num;
		}
	}
}
