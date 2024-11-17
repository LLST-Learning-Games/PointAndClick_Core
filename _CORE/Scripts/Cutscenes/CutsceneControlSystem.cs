using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cutscenes
{
    public class CutsceneControlSystem : GameSystem
    {
        [SerializeField] private List<Cutscene> _cutscenes = new();
        [SerializeField] private SceneFadeController _sceneFadeController;
        
        [ContextMenu("Test First Cutscene")]
        private void TestRunFirstCutscene()
        {
            _cutscenes[0].RunCutscene(this);
        }

        public void FadeIn(Action callback = null, float? overrideTime = null) => _sceneFadeController.FadeIn(callback, overrideTime);
        public void FadeOut(Action callback = null, float? overrideTime = null) => _sceneFadeController.FadeOut(callback, overrideTime);
        public void SetManualFadeControl(bool manualFadeControl) 
            => _sceneFadeController.SetManualFadeControl(manualFadeControl);

    }
}