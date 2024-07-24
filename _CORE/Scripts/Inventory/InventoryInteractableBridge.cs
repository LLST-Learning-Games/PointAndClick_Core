using PointAndClick.Interactables;
using System;
using UnityEngine;

namespace Inventory
{
    internal class InventoryInteractableBridge : BaseInteractableBridge
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private InventoryItem _item;
        [SerializeField] private InventorySystem _inventory;

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

        private void Start()
        {
            if (!_inventory)
            {
                _inventory = FindFirstObjectByType<InventorySystem>();
            }
        }

        public override void OnInteractionExecute()
        {
            _inventory.AddItem(_item);
            Destroy(this.gameObject);
        }
    }
}
