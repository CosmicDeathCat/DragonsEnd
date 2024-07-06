using System;
using System.Collections.Generic;
using System.Linq;
using DragonsEnd.Items.Interfaces;

namespace DragonsEnd.Items.Lists
{
    [Serializable]
    public class ItemList<T> : List<T> where T : IItem
    {
        public new void Add(T item)
        {
            var existingItem = this.FirstOrDefault(predicate: i => i.Name == item.Name);
            if (existingItem != null && item.Stackable)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                base.Add(item: item);
            }
        }

        public void Remove(T item, long quantity)
        {
            var existingItem = this.FirstOrDefault(predicate: i => i.Name == item.Name);
            if (existingItem != null && item.Stackable)
            {
                existingItem.Quantity -= quantity;
                if (existingItem.Quantity <= 0)
                {
                    base.Remove(item: existingItem);
                }
            }
            else if (existingItem != null)
            {
                base.Remove(item: existingItem);
            }
        }

        public new bool Remove(T item)
        {
            var existingItem = this.FirstOrDefault(predicate: i => i.Name == item.Name);
            if (existingItem != null && item.Stackable)
            {
                existingItem.Quantity -= item.Quantity;
                if (existingItem.Quantity <= 0)
                {
                    base.Remove(item: existingItem);
                    return true;
                }
            }
            else if (existingItem != null)
            {
                base.Remove(item: existingItem);
                return true;
            }

            return false;
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Add(item: item);
            }
        }

        public void RemoveRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Remove(item: item, quantity: item.Quantity);
            }
        }

        public new void Clear()
        {
            base.Clear();
        }

        public new bool Contains(T item)
        {
            return this.Any(predicate: i => i.Name == item.Name);
        }

        public new void CopyTo(T[] array, int arrayIndex)
        {
            base.CopyTo(array: array, arrayIndex: arrayIndex);
        }

        public new int IndexOf(T item)
        {
            return FindIndex(match: i => i.Name == item.Name);
        }

        public new void Insert(int index, T item)
        {
            var existingItem = this.FirstOrDefault(predicate: i => i.Name == item.Name);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                base.Insert(index: index, item: item);
            }
        }

        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Insert(index: index++, item: item);
            }
        }

        public bool RemoveAt(T item)
        {
            var index = IndexOf(item: item);
            if (index >= 0)
            {
                RemoveAt(index: index);
                return true;
            }

            return false;
        }

        public new void RemoveRange(int index, int count)
        {
            base.RemoveRange(index: index, count: count);
        }
    }
}