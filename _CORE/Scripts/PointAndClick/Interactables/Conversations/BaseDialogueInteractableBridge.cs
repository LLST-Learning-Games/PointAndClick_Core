using Conversation;
using PointAndClick.Interactables;
using SystemManagement;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

namespace PointAndClick.Conversation
{
    public abstract class BaseDialogueInteractableBridge : BaseInteractableBridge
    {
        [SerializeField] protected DialogueRunner _dialogueRunner;
        [SerializeField] protected string _dialogueSystemId = "Dialogue";
        [SerializeField] protected string _dialogueKey;

        protected virtual void Start()
        {
            if (_dialogueRunner is null)
            {
                FindDialogueRunner();
            }
        }

        protected virtual void FindDialogueRunner()
        {
            _dialogueRunner = GameSystemManager.Instance.GetSystem<DialogueGameSystem>(_dialogueSystemId)?.GetDialogueRunner();
        }

        protected virtual void Reset()
        {
            FindDialogueRunner();
        }

        public override void OnInteractionExecute()
        {
            StartDialogueByKey(_dialogueKey);
        }

        public void StartDialogueByKey(string key)
        {
            _dialogueRunner.StartDialogue(key);
        }
    }
}
