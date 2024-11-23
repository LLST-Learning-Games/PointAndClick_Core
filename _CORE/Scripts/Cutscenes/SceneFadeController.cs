
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cutscenes
{
    public class SceneFadeController : MonoBehaviour
    {
        [SerializeField] private Image _faderImage;
        [SerializeField] private float _pauseBeforeFadeInTime = 0.1f;
        [SerializeField] private float _fadeInTime = 0.3f;
        [SerializeField] private float _pauseAfterFadeOutTime = 0.1f;
        [SerializeField] private float _fadeOutTime = 0.2f;
        [SerializeField] private HashSet<string> _manuallyControlSceneFade = new();

        public void RegisterListeners()
        {
            SceneManager.sceneLoaded += FadeInOnSceneLoad;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= FadeInOnSceneLoad;
        }

        public void SetManualFadeControl(string sceneName)
        {
            _manuallyControlSceneFade.Add(sceneName);
        }

        public void ReleaseManualFadeControl(string sceneName)
        {
            _manuallyControlSceneFade.Remove(sceneName);
        }

        private void FadeInOnSceneLoad(Scene newScene, LoadSceneMode mode)
        {
            if (_manuallyControlSceneFade.Contains(newScene.name)
                || mode == LoadSceneMode.Additive)
            {
                return;
            }

            FadeIn();
        }

        public void FadeIn(Action callback = null, float? overrideTime = null) => StartCoroutine(FadeInCoroutine(callback, overrideTime));
        public void FadeOut(Action callback = null, float? overrideTime = null) => StartCoroutine(FadeOutCoroutine(callback, overrideTime));

        private IEnumerator FadeInCoroutine(Action callback = null, float? overrideTime = null)
        {
            SetFaderAlpha(1f);

            _faderImage.gameObject.SetActive(true);
            yield return StartCoroutine(PauseFader(_pauseBeforeFadeInTime));
            yield return StartCoroutine(Fade(0f, overrideTime ?? _fadeInTime, callback));
        }

        private IEnumerator FadeOutCoroutine(Action callback = null, float? overrideTime = null)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (!_manuallyControlSceneFade.Contains(sceneName))
            {
                _faderImage.gameObject.SetActive(false);
                yield return null;
            }

            SetFaderAlpha(0f);

            _faderImage.gameObject.SetActive(true);
            yield return StartCoroutine(Fade(1f, overrideTime ?? _fadeOutTime));
            yield return StartCoroutine(PauseFader(_pauseAfterFadeOutTime, callback));
        }

        private void SetFaderAlpha(float alpha)
        {
            Color currentColor = _faderImage.color;
            currentColor.a = alpha;
            _faderImage.color = currentColor;
        }

        private IEnumerator PauseFader(float time, Action callback = null)
        {
            yield return new WaitForSeconds(time);
            callback?.Invoke();
        }

        private IEnumerator Fade(float targetAlpha, float fadeTime, Action callback = null)
        {
            Color currentColor = _faderImage.color;
            Color targetColor = _faderImage.color;
            targetColor.a = targetAlpha;

            float counter = 0f;

            while (counter < fadeTime)
            {
                counter += Time.deltaTime;
                _faderImage.color = Color.Lerp(currentColor, targetColor, counter / fadeTime);
                yield return null;
            }

            callback?.Invoke();
        }

        [ContextMenu("Fade In Test")]
        private void TestFadeIn() => FadeIn();

        [ContextMenu("Fade Out Test")]
        private void TestFadeOut() => FadeOut();
    }
}