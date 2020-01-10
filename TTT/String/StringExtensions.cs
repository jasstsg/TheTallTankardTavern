using System;
using System.Collections.Generic;
using System.Text;

namespace TTT.String
{
    public static class StringExtensions
    {
        public static bool EqualsAny(this string str, params object[] objs)
        {
            foreach (object obj in objs)
            {
                if (str.Equals(obj))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool EqualsAny(this string str, params string[] values)
        {
            foreach (string value in values)
            {
                if (str.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool EqualsAny(this string str, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (str.Equals(value, comparisonType))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool EqualsAll(this string str, params object[] objs)
        {
            foreach (object obj in objs)
            {
                if (!str.Equals(obj))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool EqualsAll(this string str, params string[] values)
        {
            foreach (string value in values)
            {
                if (!str.Equals(value))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool EqualsAll(this string str, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (!str.Equals(value, comparisonType))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
