
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cutscenes
{
    public abstract class BaseCutsceneTrigger : MonoBehaviour
    {
        [SerializeField] protected Cutscene _cutsceneToTrigger;
        [SerializeField] protected string _requiredSceneName;
        [SerializeField] protected bool _runOnce;

        protected CutsceneControlSystem _parentSystem;

        public void RegisterParent(CutsceneControlSystem parentSystem) => _parentSystem = parentSystem;
        public abstract void RegisterTrigger();
        public abstract void DeregisterTrigger();
        public virtual void OnTrigger()
        {
            if (SceneManager.GetActiveScene().name != _requiredSceneName) 
            {
                return;
            }

            _cutsceneToTrigger.RunCutscene(_parentSystem);
            
            if( _runOnce)
            {
                DeregisterTrigger();
            }
        }

        private void OnDestroy()
        {
            DeregisterTrigger();
        }
    }
}