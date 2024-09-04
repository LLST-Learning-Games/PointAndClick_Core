using System;
using UnityEngine;

namespace Inventory
{
    public class InventoryItemController
    {
        private InventoryItemModel _itemModel;
        private bool _isSelected = false;

        public InventoryItemModel ItemModel { get { return _itemModel; } }
        public bool IsSelected { get { return _isSelected; } }
        private Action<InventoryItemController> _selectItemCallback;

        public InventoryItemController(InventoryItemModel itemModel, Action<InventoryItemController> selectItemCallback)
        {
            _itemModel = itemModel;
            _selectItemCallback = selectItemCallback;
        }

        public void SetSelected(bool isSelected) 
        { 
            _isSelected = isSelected; 
            _selectItemCallback.Invoke(this);
        }

    }
}