using Inventory;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUiView : MonoBehaviour
{
    [SerializeField] InventorySystem _inventorySystem;
    [SerializeField] Transform _itemGrid;
    [SerializeField] InventoryUiElement _inventoryItemPrefab;

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
        ClearInventoryUi();
        InitializeInventoryUi();
    }

    private void InitializeInventoryUi()
    {
        foreach (var item in _inventorySystem.Inventory.Values)
        {
            var inventoryItem = Instantiate(_inventoryItemPrefab,_itemGrid);
            inventoryItem.Initialize(item);
        }
    }

    private void ClearInventoryUi()
    {
     
        foreach (var uiElement in _uiElements)
        {
            Destroy(uiElement);
        }
        _uiElements.Clear();
    }
}
