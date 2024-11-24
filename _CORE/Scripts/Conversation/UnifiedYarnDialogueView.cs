using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using SystemManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

namespace Conversation.UI
{
    public class UnifiedYarnDialogueView : DialogueViewBase
    {
        private const string DIALOGUE_LOCK_KEY = "Unified_Dialogue";
        

        [SerializeField] protected bool _shouldBlockClicks;
        [SerializeField] protected TextMeshProUGUI lineText; 
        [SerializeField] protected GameObject continueButton;

        [SerializeField] protected bool _shouldShowDialogueHistory = true;
        [SerializeField] protected GameObject _dialogueHistoryPrefab;
        [SerializeField] protected Transform _dialogueContainer;

        [SerializeField] protected OptionView optionViewPrefab;
        [SerializeField] protected Transform _optionViewContainer;
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected UnityEvent _onDialogueStarted;

        protected List<OptionView> _optionViews = new List<OptionView>();
        protected Action<int> _onOptionSelected;
        protected string _lastOptionTextSelected = string.Empty;
        protected LocalizedLine _lastLine;

        protected ResourceLibrarySystem ResourceLibrarySystem => _resourceLibrarySystem ??= GameSystemManager.Instance.GetSystem<ResourceLibrarySystem>("ResourceLibrary");
        protected ResourceLibrarySystem _resourceLibrarySystem;

        private void Awake()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            lineText.text = string.Empty;
        }

        public override void DialogueStarted()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            if (_shouldBlockClicks)
            {
                PlayerInputLock.RegisterLock(DIALOGUE_LOCK_KEY);
            }
            _onDialogueStarted?.Invoke();
        }

        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            DisplayNextLine(dialogueLine,onDialogueLineFinished);
        }

        private void DisplayNextLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            Debug.Log("Running line - " + dialogueLine.Text.Text);
            if (_shouldShowDialogueHistory && _lastLine != null)
            {
                Color npcHistoryColor = ResourceLibrarySystem.GetCharacterData(_lastLine.CharacterName)?.HistoricTextColor ?? Color.grey;
                CreatePastDialogueRecord(lineText.text, npcHistoryColor);
                if (!String.IsNullOrEmpty(_lastOptionTextSelected))
                {
                    Color playerHistoryColor = ResourceLibrarySystem.GetCharacterData("Blue")?.HistoricTextColor ?? Color.grey;
                    CreatePastDialogueRecord(_lastOptionTextSelected, playerHistoryColor);
                    _lastOptionTextSelected = String.Empty;
                }
            }
            lineText.text = dialogueLine.Text.Text;
            lineText.color = ResourceLibrarySystem.GetCharacterData(dialogueLine.CharacterName)?.ActiveTextColor ?? Color.white;
            _lastLine = dialogueLine;
        }

        private void CreatePastDialogueRecord(string text, Color color)
        {
            var pastDialogueObject = Instantiate(_dialogueHistoryPrefab, _dialogueContainer);
            var pastDialogueText = pastDialogueObject.GetComponent<TextMeshProUGUI>();
            pastDialogueText.text = text;
            pastDialogueText.color = color;
            lineText.transform.SetAsLastSibling();
        }

        public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
        {
            continueButton.SetActive(false);

            Debug.Log("Running options...");
            // If we don't already have enough option views, create more
            while (dialogueOptions.Length > _optionViews.Count)
            {
                Debug.Log("Creating new option view");
                var optionView = CreateNewOptionView();
                optionView.gameObject.SetActive(false);
            }

            // Set up all of the option views
            int optionViewsCreated = 0;

            for (int i = 0; i < dialogueOptions.Length; i++)
            {

                var optionView = _optionViews[i];
                var option = dialogueOptions[i];
                Debug.Log("Init dialogue option " + i + " text: " + option.Line.Text.Text);

                if (option.IsAvailable == false)
                {
                    // Don't show this option.
                    continue;
                }

                optionView.gameObject.SetActive(true);

                optionView.Option = option;

                // The first available option is selected by default
                if (optionViewsCreated == 0)
                {
                    optionView.Select();
                }

                optionViewsCreated += 1;
            }

            // Note the delegate to call when an option is selected
            _onOptionSelected = onOptionSelected;

            // sometimes (not always) the TMP layout in conjunction with the
            // content size fitters doesn't update the rect transform
            // until the next frame, and you get a weird pop as it resizes
            // just forcing this to happen now instead of then
            // Relayout();

            /// <summary>
            /// Creates and configures a new <see cref="OptionView"/>, and adds
            /// it to <see cref="_optionViews"/>.
            /// </summary>
            OptionView CreateNewOptionView()
            {
                var optionView = Instantiate(optionViewPrefab);
                optionView.transform.SetParent(_optionViewContainer, false);
                optionView.transform.SetAsLastSibling();

                optionView.OnOptionSelected = OptionViewWasSelected;
                _optionViews.Add(optionView);

                return optionView;
            }

            /// <summary>
            /// Called by <see cref="OptionView"/> objects.
            /// </summary>
            void OptionViewWasSelected(DialogueOption option)
            {
                continueButton.SetActive(true);

                if (_shouldShowDialogueHistory) 
                {
                    _lastOptionTextSelected = option.Line.Text.Text;
                }

                Debug.Log("Selected option " + option.Line.Text.Text);
                foreach (var optionView in _optionViews)
                {
                    optionView.gameObject.SetActive(false);
                }

                _onOptionSelected(option.DialogueOptionID);
            }
        }

        public override void DialogueComplete()
        {
            if (_shouldBlockClicks)
            {
                PlayerInputLock.ClearLock(DIALOGUE_LOCK_KEY);
            }

            Debug.Log("Dialogue complete");
            _onOptionSelected = null;
            foreach (var optionView in _optionViews)
            {
                optionView.gameObject.SetActive(false);
            }
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnContinueClicked()
        {
            requestInterrupt?.Invoke();
        }
    }
}