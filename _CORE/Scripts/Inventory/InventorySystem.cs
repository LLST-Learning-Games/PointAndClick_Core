using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventory
{
    public class InventorySystem : GameSystem
    {
        private Dictionary<string, InventoryItem> _inventory = new();
        public IReadOnlyDictionary<string, InventoryItem> Inventory => _inventory;
        public bool IsDirty { get; internal set; } = true;

        public void AddItem(InventoryItem item)
        {
            _inventory.Add(item.Name, item);
            IsDirty = true;
            Debug.Log($"[{GetType().Name}] Added {item.Name} to inventory.");
        }

        public void RemoveItem(InventoryItem item)
        {
            _inventory.Remove(item.Name);
            IsDirty = true;
        }
    }
}
