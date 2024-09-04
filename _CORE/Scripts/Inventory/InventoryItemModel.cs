using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "NewInventoryItem", menuName = "ScriptableObjects/InventoryItem", order = 1)]
    public class InventoryItemModel : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite EnvironmentSprite;
        public Sprite InventorySprite;
    }
}
