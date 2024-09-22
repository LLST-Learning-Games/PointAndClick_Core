using PointAndClick.Interactables;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    internal class UseInventoryOnInteractableBridge : BaseInteractableBridge
    {
        [SerializeField] private UnityEvent _actionOnUseInventory;
        public override void OnInteractionExecute()
        {
            _actionOnUseInventory?.Invoke();
        }
    }
}