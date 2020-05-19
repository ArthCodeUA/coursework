using System.Collections;
using System.Collections.Generic;

namespace Customer
{
    public class Inventory<T> : IEnumerable
    {
        private readonly List<T> _inventory;

        public Inventory()
        {
            _inventory = new List<T>();
        }
        
        public void AddItem(T item)
        {
            _inventory.Add(item);
        }
        
        public void RemoveItem(T item)
        {
            _inventory.Remove(item);
        }
        
        public T this[int index] => _inventory[index];

        public IEnumerator GetEnumerator()
        {
            return _inventory.GetEnumerator();
        }
    }
}