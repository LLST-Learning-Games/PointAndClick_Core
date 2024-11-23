using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscenes
{
    public class CutsceneControlSystem : GameSystem
    {
        [SerializeField] private List<Cutscene> _cutscenes = new();
        [SerializeField] private List<BaseCutsceneTrigger> _triggers = new();
        [SerializeField] private SceneFadeController _sceneFadeController;
        
        [ContextMenu("Test First Cutscene")]
        private void TestRunFirstCutscene()
        {
            _cutscenes[0].RunCutscene(this);
        }

        public override void Initialize()
        {
            _sceneFadeController.RegisterListeners();
            foreach (var trigger in _triggers)
            {
                trigger.RegisterParent(this);
                trigger.RegisterTrigger();
            }
        }

        private void OnDestroy()
        {
            foreach(var trigger in _triggers)
            {
                trigger.DeregisterTrigger();
            }
        }

        public void FadeIn(Action callback = null, float? overrideTime = null) => _sceneFadeController.FadeIn(callback, overrideTime);
        public void FadeOut(Action callback = null, float? overrideTime = null) => _sceneFadeController.FadeOut(callback, overrideTime);
        public void SetManualFadeControl(string sceneName) => _sceneFadeController.SetManualFadeControl(sceneName);
        public void ReleaseManualFadeControl(string sceneName) => _sceneFadeController.ReleaseManualFadeControl(sceneName);


    }
}