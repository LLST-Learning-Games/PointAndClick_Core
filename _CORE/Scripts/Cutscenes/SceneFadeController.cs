
using System;
using System.Collections;
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
        [SerializeField] private bool _manuallyControlSceneFade = false;

        private void Awake()
        {
            SceneManager.sceneLoaded += FadeInOnSceneLoad;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= FadeInOnSceneLoad;
        }

        public void SetManualFadeControl(bool manualFadeControl)
            => _manuallyControlSceneFade = manualFadeControl;

        private void FadeInOnSceneLoad(Scene _, LoadSceneMode mode)
        {
            if (_manuallyControlSceneFade)
            {
                return;
            }

            if (mode != LoadSceneMode.Additive)
            {
                FadeIn();
            }
        }

        public void FadeIn(Action callback = null) => StartCoroutine(FadeInCoroutine(callback));
        public void FadeOut(Action callback = null) => StartCoroutine(FadeOutCoroutine(callback));

        private IEnumerator FadeInCoroutine(Action callback = null)
        {
            SetFaderAlpha(1f);

            _faderImage.gameObject.SetActive(true);
            yield return StartCoroutine(PauseFader(_pauseBeforeFadeInTime));
            yield return StartCoroutine(Fade(0f, _fadeInTime, callback));
        }

        private IEnumerator FadeOutCoroutine(Action callback = null)
        {
            if (!_manuallyControlSceneFade)
            {
                _faderImage.gameObject.SetActive(false);
                yield return null;
            }

            SetFaderAlpha(0f);

            _faderImage.gameObject.SetActive(true);
            yield return StartCoroutine(Fade(1f, _fadeOutTime));
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