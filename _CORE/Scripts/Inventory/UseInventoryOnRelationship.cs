using System;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    [Serializable]
    public class UseInventoryOnRelationship
    {
        [SerializeField] public string ItemId;
        [SerializeField] public bool OnActionConsumeItem;
        [SerializeField] public UnityEvent Action;
    }
}