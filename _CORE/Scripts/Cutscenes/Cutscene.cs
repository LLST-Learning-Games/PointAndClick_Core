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

        private BaseCutsceneStep _currentStep;
        public Action OnCutsceneStepComplete;

        public async void RunCutscene(CutsceneControlSystem parentSystem)
        {
            Debug.Log($"[{GetType().Name}] Running cutscene with id: {_cutsceneName}");            


            foreach (var step in _cutsceneSteps) 
            {             
                if (step.BlockInput)
                {
                    PlayerInputLock.RegisterLock(_cutsceneName);
                }
                _currentStep = step;
                await _currentStep.TriggerStep(parentSystem);            
                if (step.BlockInput)
                {
                    PlayerInputLock.ClearLock(_cutsceneName);
                }
            }
            OnCutsceneStepComplete?.Invoke();


            Debug.Log($"[{GetType().Name}] End of cutscene with id: {_cutsceneName}");
        }
    }
}