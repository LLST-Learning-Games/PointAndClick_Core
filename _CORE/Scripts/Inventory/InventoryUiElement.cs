using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
