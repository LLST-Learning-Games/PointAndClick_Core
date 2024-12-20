using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySystem : GameSystem
    {
        [SerializeField] private Image _cursor;

        private Dictionary<string, InventoryItemController> _inventory = new();
        public IReadOnlyDictionary<string, InventoryItemController> Inventory => _inventory;
        public bool IsDirty { get; internal set; } = true;

        private InventoryItemController _selectedItem;
        private InventoryItemController _queuedForUseItem;
        public bool IsItemSelected => _selectedItem != null;
        public bool IsItemQueued => _queuedForUseItem != null;
        public InventoryItemController SelectedItem { get { return _selectedItem; } }
        public InventoryItemController QueuedForUseItem { get { return _queuedForUseItem; } }
        [SerializeField] private bool _verboseLogging;


        public void AddItem(InventoryItemModel item)
        {
            var controller = new InventoryItemController(item, SetSelectedItem);
            _inventory.Add(item.Name, controller);
            IsDirty = true;
            Debug.Log($"[{GetType().Name}] Added {item.Name} to inventory.");
        }

        public void SetSelectedItem(InventoryItemController selectedItem)
        {
            _selectedItem = selectedItem;
            _cursor.sprite = _selectedItem.ItemModel.InventorySprite;
            _cursor.gameObject.SetActive(true);
        }

        public void QueueSelectedItemForUse() => _queuedForUseItem = _selectedItem;
        public void ClearQueuedItem() => _queuedForUseItem = null;

        internal void DeselectItem()
        {
            _selectedItem.SetSelected(false);
            _cursor.gameObject.SetActive(false);
            _selectedItem = null;
            IsDirty = true;
        }

        public void UpdateCursor(Vector3 position)
        {
            _cursor.transform.position = position;
        }

        public void RemoveItem(InventoryItemModel item)
        {
            _inventory.Remove(item.Name);
            IsDirty = true;
        }

        public override void Initialize()
        {
            //..
        }
    }
}
