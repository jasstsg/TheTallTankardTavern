using System.Collections.Generic;

namespace TTT.Collections.Generic.Extensions
{
	public static class ListExtensions
	{
		public static void AddRange<T>(this List<T> GenericList, params IEnumerable<T>[] Ranges)
		{
			foreach (IEnumerable<T> range in Ranges)
			{
				GenericList.AddRange(range);
			}
		}
	}
}
