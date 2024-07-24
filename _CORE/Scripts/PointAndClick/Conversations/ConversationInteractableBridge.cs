using PointAndClick.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class ConversationInteractableBridge : BaseInteractableBridge
{
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private string _conversationId = "Start";

    private UnityAction _onDialogueComplete;
    private void Start()
    {
        if (_dialogueRunner is null)
        {
            FindDialogueRunner();
        }
            
        _onDialogueComplete += OnDialogueCompete;
        _dialogueRunner.onDialogueComplete.AddListener(_onDialogueComplete);
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
        _dialogueRunner.StartDialogue(_conversationId);
        PlayerInputLock.RegisterLock(_conversationId);
    }

    private void OnDialogueCompete()
    {
        PlayerInputLock.ClearLock(_conversationId);
    }
}
