using Cutscenes;
using Persistence;
using PointAndClick.Interactable;
using PointAndClick.Player;
using SystemManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PointAndClick.Interactables
{
    public class SceneChangeInteractable : BaseInteractableBridge
    {
        [SerializeField] private string _sceneName;

        private CutsceneControlSystem _cutsceneControl;

        private void Start()
        {
            _cutsceneControl = GameSystemManager.Instance.GetSystem<CutsceneControlSystem>("CutsceneControl");
            var persistence = GameSystemManager.Instance.GetSystem<ScenePersistenceSystem>("ScenePersistence");
            if(_sceneName == persistence.LastSceneName)
            {
                var position = GetComponent<InteractableController>().WalkToPosition;
                var player = FindObjectOfType<PlayerInput>().gameObject;
                player.transform.position = position;
            }
        }

        public override void OnInteractionExecute()
        {
            _cutsceneControl.FadeOut(() =>
                SceneManager.LoadScene(_sceneName)
            );
        }
    }
}
