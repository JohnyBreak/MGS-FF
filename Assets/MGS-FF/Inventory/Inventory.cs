using System.Collections.Generic;

namespace InventorySystem
{
    public class Inventory
    {
        private readonly Dictionary<int, IItem> _itemsMap = new();

        public void Add(IItem item)
        {
            _itemsMap[item.GetKey()] = item;
        }

        public void Remove(IItem item)
        {
            Remove(item.GetKey());
        }
        
        public void Remove(int key)
        {
            _itemsMap.Remove(key);
        }

        public bool HasItem(int key)
        {
            return _itemsMap.ContainsKey(key);
        }

        public IItem Get(int key)
        {
            if (!HasItem(key))
            {
                return null;
            }

            return _itemsMap[key];
        }
    }
}


