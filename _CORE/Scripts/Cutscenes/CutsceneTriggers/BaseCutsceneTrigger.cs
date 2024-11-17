
using System;
using UnityEngine;

namespace Cutscenes
{
    public abstract class BaseCutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private Cutscene _cutsceneToTrigger;
        [SerializeField] private string _requiredSceneName; 
        public abstract void RegisterTrigger();
        public abstract void DeregisterTrigger();
        public abstract void OnTriggerApplyRules();

        private void OnDestroy()
        {
            DeregisterTrigger();
        }
    }
}