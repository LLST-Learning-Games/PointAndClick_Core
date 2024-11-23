using System;
using UnityEngine.SceneManagement;

namespace Cutscenes
{
    public class SceneChangeTrigger : BaseCutsceneTrigger
    {
        public override void RegisterTrigger()
        {
            SceneManager.sceneLoaded += SceneLoadWrapper;
            _parentSystem.SetManualFadeControl(_requiredSceneName);
        }

        private void SceneLoadWrapper(Scene _, LoadSceneMode mode)
        {
            if (mode == LoadSceneMode.Additive)
            {
                return;
            }

            OnTrigger();
        }

        public override void DeregisterTrigger()
        {
            SceneManager.sceneLoaded -= SceneLoadWrapper;
            _parentSystem.ReleaseManualFadeControl(_requiredSceneName);
        }

    }
}