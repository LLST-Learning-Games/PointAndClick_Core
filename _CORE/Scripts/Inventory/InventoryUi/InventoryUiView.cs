using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI 
{
    public class InventoryUiView : MonoBehaviour
    {
        [SerializeField] private InventorySystem _inventorySystem;
        [SerializeField] private Transform _itemGrid;
        [SerializeField] private InventoryUiElement _inventoryItemPrefab;

        private List<InventoryUiElement> _uiElements = new();

        private void Start()
        {
            if (!_inventorySystem)
            {
                _inventorySystem = FindFirstObjectByType<InventorySystem>();
            }
        }

        public void Initialize()
        {
            if(_inventorySystem.IsDirty)
            {
                ClearInventoryUi();
                InitializeInventoryUi();
                _inventorySystem.IsDirty = false;
            }
        }

        private void InitializeInventoryUi()
        {
            foreach (var item in _inventorySystem.Inventory.Values)
            {
                var inventoryItem = Instantiate(_inventoryItemPrefab, _itemGrid);
                inventoryItem.Initialize(item);
                _uiElements.Add(inventoryItem);
            }
        }

        private void ClearInventoryUi()
        {
            foreach (var uiElement in _uiElements)
            {
                Destroy(uiElement.gameObject);
            }
            _uiElements.Clear();
        }
    }
}
