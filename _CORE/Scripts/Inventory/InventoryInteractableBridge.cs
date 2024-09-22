using PointAndClick.Interactables;
using SystemManagement;
using UnityEngine;

namespace Inventory
{
    internal class InventoryInteractableBridge : BaseInteractableBridge
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private InventoryItemModel _item;

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
            var inventory = GameSystemManager.Instance.GetSystem<InventorySystem>("Inventory");
            inventory?.AddItem(_item);
            Destroy(this.gameObject);
        }
    }
}
