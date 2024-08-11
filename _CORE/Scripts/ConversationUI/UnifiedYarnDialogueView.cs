using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace Conversation.UI
{
    public class UnifiedYarnDialogueView : DialogueViewBase
    {
        [SerializeField] protected TextMeshProUGUI lineText; 
        [SerializeField] protected GameObject continueButton;

        [SerializeField] protected OptionView optionViewPrefab;
        protected List<OptionView> optionViews = new List<OptionView>();
        protected Action<int> OnOptionSelected;

        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            Debug.Log("Running line - " + dialogueLine.Text.Text);
            lineText.text = dialogueLine.Text.Text;
            //onDialogueLineFinished?.Invoke();
        }

        public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
        {
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
                optionView.transform.SetParent(transform, false);
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

            Debug.Log("Dialogue complete");
            OnOptionSelected = null;
            foreach (var optionView in optionViews)
            {
                optionView.gameObject.SetActive(false);
            }
        }

        public void OnContinueClicked()
        {
            requestInterrupt?.Invoke();
        }
    }
}