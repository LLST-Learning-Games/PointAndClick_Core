using PointAndClick.Interactables;
using PointAndClick.Player;
using System;
using UnityEngine;

namespace PointAndClick.Interactable
{
    [Serializable]
    public class InteractionInputTypeRelationship 
    {
        [SerializeField] public PlayerInputType InputType;
        [SerializeField] public BaseInteractableBridge Interactable;
    }
}