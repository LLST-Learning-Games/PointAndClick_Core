using Persistence;
using PointAndClick.Conversation;
using PointAndClick.Interactables;
using SystemManagement;
using UnityEngine;

namespace Inventory
{
    internal class InventoryInteractableBridge : BaseInteractableBridge
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private InventoryItemModel _item;
        [SerializeField] private EnabledPersistence _enabledPersistence;

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

        [ExecuteInEditMode]
        [ContextMenu("Set LookAt")]
        private void SetLookat()
        {
            var lookAtInteractable = GetComponent<LookAtInteractableBridge>();
            lookAtInteractable.SetDialogueKey(_item.Description);

        }

        [ExecuteInEditMode]
        private void Reset()
        {
            SetSprite();
            SetLookat();
        }

        public override void OnInteractionExecute()
        {
            var inventory = GameSystemManager.Instance.GetSystem<InventorySystem>("Inventory");
            inventory?.AddItem(_item);
            _enabledPersistence?.RegisterActiveState(false);
            Destroy(this.gameObject);
        }
    }
}
