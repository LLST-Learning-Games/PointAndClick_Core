using Conversation;
using Cutscenes;
using System;
using System.Threading.Tasks;
using SystemManagement;
using UnityEngine;

namespace Custscenes
{
    public class CutsceneStep_Dialogue : BaseCutsceneStep
    {
        [SerializeField] private string _conversationKey;
        private DialogueGameSystem _dialogueSystem;

        private TaskCompletionSource<bool> _isDialogueRunning = new TaskCompletionSource<bool>();
        public override Task StepBehavior()
        {
            if(!_dialogueSystem){
                _dialogueSystem = GameSystemManager.Instance.GetSystem<DialogueGameSystem>("Dialogue");
            }
            _dialogueSystem.StartDialogue(_conversationKey);
            _dialogueSystem.GetDialogueRunner().onDialogueComplete.AddListener(OnDialogueComplete);
            return _isDialogueRunning.Task;
        }

        private void OnDialogueComplete()
        {
            _dialogueSystem.GetDialogueRunner().onDialogueComplete.RemoveListener(OnDialogueComplete);
            _isDialogueRunning.SetResult(true);
        }
    }
}