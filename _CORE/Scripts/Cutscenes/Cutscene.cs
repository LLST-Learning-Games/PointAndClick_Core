using System;
using System.Collections.Generic;
using SystemManagement;
using UnityEngine;

namespace Cutscenes
{
    public class Cutscene : MonoBehaviour
    {
        [SerializeField] private string _cutsceneName;
        [SerializeField] private List<BaseCutsceneStep> _cutsceneSteps;
        [SerializeField] private bool _blockInput = true;
        [SerializeField] private bool _overrideSceneFadeIn = false;

        private BaseCutsceneStep _currentStep;
        public Action OnCutsceneStepComplete;

        public async void RunCutscene(CutsceneControlSystem parentSystem)
        {
            Debug.Log($"[{GetType().Name}] Running cutscene with id: {_cutsceneName}");            
            if (_blockInput)
            {
                PlayerInputLock.RegisterLock(_cutsceneName);
            }

            parentSystem.SetManualFadeControl(_overrideSceneFadeIn);

            foreach (var step in _cutsceneSteps) 
            { 
                _currentStep = step;
                await _currentStep.TriggerStep(parentSystem);
            }
            OnCutsceneStepComplete?.Invoke();
            if (_blockInput)
            {
                PlayerInputLock.ClearLock(_cutsceneName);
            }

            parentSystem.SetManualFadeControl(false);

            Debug.Log($"[{GetType().Name}] End of cutscene with id: {_cutsceneName}");
        }
    }
}