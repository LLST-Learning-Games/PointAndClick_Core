using Cutscenes;
using System.Threading.Tasks;
using SystemManagement;
using UnityEngine;

namespace Custscenes
{
    public class CutsceneStep_FadeInOrOut : BaseCutsceneStep
    {
        [SerializeField] private float _fadeTime = 1f;
        [SerializeField] private bool _fadeOut = false;
        public override Task StepBehavior()
        {
            if (_fadeOut)
            {
                _parentSystem.FadeOut(null, _fadeTime);
            }
            else
            {
                _parentSystem.FadeIn(null, _fadeTime);
            }
            return Task.Delay((int)(_fadeTime * 1000));
        }
    }
}