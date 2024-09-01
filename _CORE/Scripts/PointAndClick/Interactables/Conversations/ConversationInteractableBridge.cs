using PointAndClick.Interactables;
using SystemManagement;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

namespace PointAndClick.Conversation
{
    public class ConversationInteractableBridge : BaseDialogueInteractableBridge
    {
        private UnityAction _onDialogueComplete;

        protected override void Start()
        {
            base.Start();

            _onDialogueComplete += OnDialogueCompete;
            _dialogueRunner.onDialogueComplete.AddListener(_onDialogueComplete);
        }

        public override void OnInteractionExecute()
        {
            base.OnInteractionExecute();
            PlayerInputLock.RegisterLock(_dialogueKey);
        }

        private void OnDialogueCompete()
        {
            PlayerInputLock.ClearLock(_dialogueKey);
        }
    }
    
}
