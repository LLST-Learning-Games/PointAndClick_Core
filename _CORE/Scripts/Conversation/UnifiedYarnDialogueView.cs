using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

        // todo - temp solution. create character objects to store this information.
        [SerializeField] protected Color _npcHistoryColor;
        [SerializeField] protected Color _playerHistoryColor;

        [SerializeField] protected OptionView optionViewPrefab;
        [SerializeField] protected Transform _optionViewContainer;
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected UnityEvent _onDialogueStarted;

        protected List<OptionView> optionViews = new List<OptionView>();
        protected Action<int> OnOptionSelected;
        protected string _lastOptionTextSelected = string.Empty;

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
            if (_shouldShowDialogueHistory)
            {
                CreatePastDialogueRecord(lineText.text, _npcHistoryColor);
                if (!String.IsNullOrEmpty(_lastOptionTextSelected))
                {
                    CreatePastDialogueRecord(_lastOptionTextSelected, _playerHistoryColor);
                    _lastOptionTextSelected = String.Empty;
                }
            }
            lineText.text = dialogueLine.Text.Text;
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
            while (dialogueOptions.Length > optionViews.Count)
            {
                Debug.Log("Creating new option view");
                var optionView = CreateNewOptionView();
                optionView.gameObject.SetActive(false);
            }

            // Set up all of the option views
            int optionViewsCreated = 0;

            for (int i = 0; i < dialogueOptions.Length; i++)
            {

                var optionView = optionViews[i];
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
            OnOptionSelected = onOptionSelected;

            // sometimes (not always) the TMP layout in conjunction with the
            // content size fitters doesn't update the rect transform
            // until the next frame, and you get a weird pop as it resizes
            // just forcing this to happen now instead of then
            // Relayout();

            /// <summary>
            /// Creates and configures a new <see cref="OptionView"/>, and adds
            /// it to <see cref="optionViews"/>.
            /// </summary>
            OptionView CreateNewOptionView()
            {
                var optionView = Instantiate(optionViewPrefab);
                optionView.transform.SetParent(_optionViewContainer, false);
                optionView.transform.SetAsLastSibling();

                optionView.OnOptionSelected = OptionViewWasSelected;
                optionViews.Add(optionView);

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
                foreach (var optionView in optionViews)
                {
                    optionView.gameObject.SetActive(false);
                }

                OnOptionSelected(option.DialogueOptionID);
            }
        }

        public override void DialogueComplete()
        {
            if (_shouldBlockClicks)
            {
                PlayerInputLock.ClearLock(DIALOGUE_LOCK_KEY);
            }

            Debug.Log("Dialogue complete");
            OnOptionSelected = null;
            foreach (var optionView in optionViews)
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