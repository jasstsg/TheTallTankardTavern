using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TTT.Common
{
    public abstract class BaseEnumerableCollection<T> : IEnumerable<T>, ICollection<T>
    {
        private List<T> InnerCollection = new List<T>();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InnerCollection.GetEnumerator();
        }

        public T this[int index]
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

        public bool Remove(T item)
        {
            return InnerCollection.Remove(item);
        }

        public int Count => InnerCollection.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            InnerCollection.Add(item);
        }

        public void Clear()
        {
            InnerCollection.Clear();
        }

        public bool Contains(T item)
        {
            return InnerCollection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            InnerCollection.CopyTo(array, arrayIndex);
        }
    }
}
