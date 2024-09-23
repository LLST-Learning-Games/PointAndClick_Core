using UnityEngine;
using Yarn.Unity;

namespace Conversation
{
    public class DialogueGameSystem : GameSystem
    {
        [SerializeField] private DialogueRunner _dialogueRunner;

        public DialogueRunner GetDialogueRunner() => _dialogueRunner;

        private void Reset()
        {
             FindDialogueRunner();
        }

        [ContextMenu("Find Dialogue Runner")]
        [ExecuteInEditMode] 
        private void FindDialogueRunner()
        {
            _dialogueRunner = gameObject.GetComponent<DialogueRunner>();
        }
    }
}