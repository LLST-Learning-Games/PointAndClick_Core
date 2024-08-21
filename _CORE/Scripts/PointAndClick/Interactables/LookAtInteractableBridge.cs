using PointAndClick.Interactables;
using UnityEngine;
using Yarn.Unity;

namespace PointAndClick.Interactable
{
    public class LookAtInteractableBridge : BaseInteractableBridge
    {
        [SerializeField] private DialogueRunner _dialogueRunner;
        [SerializeField] private string _lookAtKey;

        private void Start()
        {
            if (_dialogueRunner is null)
            {
                FindDialogueRunner();
            }

            //_onDialogueComplete += OnDialogueCompete;
            //_dialogueRunner.onDialogueComplete.AddListener(_onDialogueComplete);
        }

        [ContextMenu("Find Minigame Scene Manager")]
        private void FindDialogueRunner()
        {
            _dialogueRunner = FindFirstObjectByType<DialogueRunner>();
        }
        private void Reset()
        {
            FindDialogueRunner();
        }

        public override void OnInteractionExecute()
        {
            _dialogueRunner.StartDialogue(_lookAtKey);
            Debug.Log($"[{GetType().Name}] You looked at this {_lookAtKey} thing");
        }
    }
}
