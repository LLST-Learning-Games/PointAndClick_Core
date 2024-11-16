using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cutscenes
{
    public class CutsceneControlSystem : GameSystem
    {
        [SerializeField] private SceneFadeController _sceneFadeController;

        public void FadeIn(Action callback = null) => _sceneFadeController.FadeIn(callback);
        public void FadeOut(Action callback = null) => _sceneFadeController.FadeOut(callback);
        public void SetManualFadeControl(bool manualFadeControl) 
            => _sceneFadeController.SetManualFadeControl(manualFadeControl);

    }
}