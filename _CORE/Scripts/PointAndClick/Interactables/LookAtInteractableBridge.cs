using PointAndClick.Interactables;
using UnityEngine;

namespace PointAndClick.Interactable
{
    internal class LookAtInteractableBridge : BaseInteractableBridge
    {
        [SerializeField] private string _lookAtKey;
        public override void OnInteractionExecute()
        {
            Debug.Log($"[{GetType().Name}] You looked at this {_lookAtKey} thing");
        }
    }
}
