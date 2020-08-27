using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TTT.Common.Abstractions
{
	[JsonObject]
	public abstract class BaseListModel<T> : IEnumerable<T>, IEnumerable, ICollection<T>
	{
		[JsonProperty]
		protected virtual List<T> InnerCollection {	get; set; } = new List<T>();


		[JsonIgnore]
		public virtual int Count => InnerCollection.Count;

		[JsonIgnore]
		public virtual bool IsReadOnly => ((ICollection<T>)InnerCollection).IsReadOnly;

		public virtual T this[int index]
		{
			get
			{
				return InnerCollection[index];
			}
			set
			{
				InnerCollection[index] = value;
			}
		}

		public virtual IEnumerator<T> GetEnumerator()
		{
			return InnerCollection.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return InnerCollection.GetEnumerator();
		}

		public virtual void Add(T element)
		{
			InnerCollection.Add(element);
		}

		/// <summary>
		/// Adds an element only if it does not already exist in the collection
		/// </summary>
		/// <param name="element">The element to be added to the collection</param>
		/// <returns>True if the element was added, false if the element was not added.</returns>
		public virtual bool AddSingle(T element)
		{
			if (!InnerCollection.Contains(element))
			{
				this.Add(element);
				return true;
			}
			return false;
		}

		public virtual void Clear()
		{
			InnerCollection.Clear();
		}

		public virtual bool Contains(T element)
		{
			return InnerCollection.Contains(element);
		}

		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			InnerCollection.CopyTo(array, arrayIndex);
		}

		public virtual bool Remove(T element)
		{
			return InnerCollection.Remove(element);
		}

		public virtual bool Exists(Predicate<T> match)
		{
			return InnerCollection.Exists(match);
		}

		public void Overwrite(ICollection<T> NewCollection)
		{
			InnerCollection.Clear();
			InnerCollection.AddRange(NewCollection);
		}
	}
}
