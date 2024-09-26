using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Inventory.UI 
{
    public class InventoryUiView : MonoBehaviour
    {
        [SerializeField] private InventorySystem _inventorySystem;
        [SerializeField] private Transform _itemGrid;
        [SerializeField] private InventoryUiElement _inventoryItemPrefab;
        [SerializeField] private UnityEvent _onExitCallback;

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

        private void Update()
        {
            if((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) 
                && !EventSystem.current.IsPointerOverGameObject())
            {
                _onExitCallback?.Invoke();
                gameObject.SetActive(false);
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
