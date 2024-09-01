using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class InventoryUiElement : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private InventoryItem _item;

        public void Initialize(InventoryItem item)
        {
            _item = item;
            _image.sprite = _item.InventorySprite;
        }
    }
}
