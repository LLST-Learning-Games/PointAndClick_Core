using System;
using UnityEngine;
using Yarn.Unity;

namespace Conversation
{
    public class DialogueGameSystem : GameSystem
    {
        [SerializeField] private DialogueRunner _dialogueRunner;

        public DialogueRunner GetDialogueRunner() => _dialogueRunner;

        public override void Initialize()
        {
            //..
        }

        public void StartDialogue(string key) 
        {
            if (_dialogueRunner.IsDialogueRunning)
            {
                _dialogueRunner.Stop();
            }
            _dialogueRunner.StartDialogue(key); 
        }

        private void Reset()
        {
            FindDialogueRunner();
        }

        [ContextMenu("Find Dialogue Runner")]
        private void FindDialogueRunner()
        {
            _dialogueRunner = gameObject.GetComponent<DialogueRunner>();
        }

    }
}