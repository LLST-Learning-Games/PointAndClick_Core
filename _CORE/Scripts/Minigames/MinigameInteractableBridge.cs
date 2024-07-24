

using PointAndClick.Interactables;
using UnityEngine;

namespace Minigames
{
    public class MinigameInteractableBridge : BaseInteractableBridge
    {
        [SerializeField] private int _minigameSceneIndex;
        [SerializeField] private MinigameSceneManager _sceneManager;

        private void Start()
        {
            if (_sceneManager is null)
            {
                FindMinigameSceneManager();
            }
        }

        [ContextMenu("Find Minigame Scene Manager")]
        private void FindMinigameSceneManager()
        {
            _sceneManager = FindFirstObjectByType<MinigameSceneManager>();
        }
        private void Reset()
        {
            FindMinigameSceneManager();
        }

        public override void OnInteractionExecute()
        {
            _sceneManager.RequestSceneLoad(_minigameSceneIndex);
        }
    }
}
