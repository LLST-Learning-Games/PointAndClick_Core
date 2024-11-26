using PointAndClick.Interactables;
using UnityEngine;

namespace Minigames
{
    public class MinigameInteractableBridge_Prefab : BaseInteractableBridge
    {
        [SerializeField] private GameObject _minigamePrefab;
        [SerializeField] private RectTransform _parent;
        public override void OnInteractionExecute()
        {
            Instantiate(_minigamePrefab, _parent);
        }
    }
}
