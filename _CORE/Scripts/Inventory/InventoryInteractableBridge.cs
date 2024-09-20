using PointAndClick.Interactables;
using System;
using UnityEngine;

namespace Inventory
{
    internal class InventoryInteractableBridge : BaseInteractableBridge
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private InventoryItemModel _item;
        private InventorySystem _inventory;

        [ExecuteInEditMode]
        [ContextMenu("Update Sprite")]
        private void SetSprite()
        {
            if (!_spriteRenderer || !_item)
            {
                return;
            }

            _spriteRenderer.sprite = _item.EnvironmentSprite;
        }

        private void Reset()
        {
            SetSprite();
        }

        public override void OnInteractionExecute()
        {
            _inventory = FindFirstObjectByType<InventorySystem>();
            _inventory.AddItem(_item);
            Destroy(this.gameObject);
        }
    }
}
