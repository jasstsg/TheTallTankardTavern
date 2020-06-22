using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TTT.Items.Weapons.Properties
{
    public class WeaponProperties : IEnumerable<AbstractWeaponProperty>,ICollection<AbstractWeaponProperty>
    {
        private List<AbstractWeaponProperty> InnerCollection = new List<AbstractWeaponProperty>();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<AbstractWeaponProperty> GetEnumerator()
        {
            return InnerCollection.GetEnumerator();
        }

        public AbstractWeaponProperty this[int index]
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

        public bool Remove(AbstractWeaponProperty item)
        {
            return InnerCollection.Remove(item);
        }

        public int Count => InnerCollection.Count;

        public bool IsReadOnly => false;

        public void Add(AbstractWeaponProperty item)
        {
            InnerCollection.Add(item);
        }

        public void Clear()
        {
            InnerCollection.Clear();
        }

        public bool Contains(AbstractWeaponProperty item)
        {
            return InnerCollection.Contains(item);
        }

        public void CopyTo(AbstractWeaponProperty[] array, int arrayIndex)
        {
            InnerCollection.CopyTo(array, arrayIndex);
        }


    }
}
