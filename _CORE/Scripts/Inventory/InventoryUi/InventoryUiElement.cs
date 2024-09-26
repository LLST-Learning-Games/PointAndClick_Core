using Conversation;
using SystemManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class InventoryUiElement : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _image;
        private InventoryItemController _item;
        private DialogueGameSystem _lookAtSystem;

        private readonly Color _selectedColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);

        private void Start()
        {
            _lookAtSystem = GameSystemManager.Instance.GetSystem<DialogueGameSystem>("LookAt");
        }

        public void Initialize(InventoryItemController item)
        {
            _item = item;
            _image.sprite = _item.ItemModel.InventorySprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {  
                OnLeftClick();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClick();
            }
        }

        public void OnLeftClick()
        {
            Debug.Log($"[{GetType().Name}] You clicked on a {_item.ItemModel.Name}");
            _item.SetSelected(true);
            _image.color = _selectedColor;
        }

        private void OnRightClick()
        {
            _lookAtSystem.StartDialogue(_item.ItemModel.Description);
        }


    }
}
