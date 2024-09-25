using PointAndClick.Interactables;
using System.Collections.Generic;
using SystemManagement;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    internal class UseInventoryOnInteractableBridge : BaseInteractableBridge
    {
        [SerializeField] private List<UseInventoryOnRelationship> _inventoryItemRelationships;
        [SerializeField] private UnityEvent _fallbackAction;
        private readonly Dictionary<string, List<UseInventoryOnRelationship>> _relationships = new();
        private InventorySystem _inventory;


        private void Start()
        {
            _inventory = GameSystemManager.Instance.GetSystem<InventorySystem>("Inventory");

            _relationships.Clear();
            foreach (var typeRelationship in _inventoryItemRelationships)
            {
                List<UseInventoryOnRelationship> relationshipList;
                if (!_relationships.TryGetValue(typeRelationship.ItemId, out relationshipList))
                {
                    relationshipList = new List<UseInventoryOnRelationship>();
                    _relationships.Add(typeRelationship.ItemId, relationshipList);
                }

                relationshipList.Add(typeRelationship);
            }
        }

        public override void OnInteractionExecute()
        {
            if (!_inventory.IsItemQueued)
            {
                return;
            }

            if (_relationships.TryGetValue(_inventory.QueuedForUseItem.ItemModel.Name, out var relationshipList))
            {
                foreach (var relationship in relationshipList)
                {
                    relationship.Action?.Invoke();
                    if (relationship.OnActionConsumeItem)
                    {
                        _inventory.RemoveItem(_inventory.QueuedForUseItem.ItemModel);
                    }
                }
            }
            else
            {
                _fallbackAction?.Invoke();
            }
            _inventory.ClearQueuedItem();
        }
    }
}