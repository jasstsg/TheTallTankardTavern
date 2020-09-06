using System;
using System.Collections.Generic;
using System.Linq;

namespace TTT.Collections.Generic.Extensions
{
	public static class DictionaryExtensions
	{
		/// <summary>
		/// Removes dictionary elements based on the predicate
		/// </summary>
		/// <typeparam name="K">Key type</typeparam>
		/// <typeparam name="V">Value type</typeparam>
		/// <param name="Collection">The Dictionary</param>
		/// <param name="Predicate">Usage: (k, v) => {expression}</param>
		public static void RemoveAll<K, V>(this Dictionary<K, V> Collection, Func<K, V, bool> Predicate)
        {
			IEnumerable<K> KeysToRemove = Collection.Keys.Where(k => Predicate(k, Collection[k]));
			foreach (K key in KeysToRemove)
            {
				Collection.Remove(key);
            }
        }
	}
}
