using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class InventoryUiElement : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private InventoryItemController _item;

        private readonly Color _selectedColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);

        public void Initialize(InventoryItemController item)
        {
            _item = item;
            _image.sprite = _item.ItemModel.InventorySprite;
        }

        public void OnClick()
        {
            Debug.Log($"[{GetType().Name}] You clicked on a {_item.ItemModel.Name}");
            _item.SetSelected(true);
            _image.color = _selectedColor;
        }
    }
}
