using Cutscenes;
using System.Threading.Tasks;
using UnityEngine;

namespace Custscenes
{
    public class CutsceneStep_Animation : BaseCutsceneStep
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _triggerId;

        private TaskCompletionSource<bool> _isAnimationRunning = new TaskCompletionSource<bool>();
        public override Task StepBehavior()
        {
            _animator.SetTrigger(_triggerId);
            _animator.Update(0);
            var clips = _animator.GetCurrentAnimatorClipInfo(0);
            int delayTime = (int)(clips[0].clip.length * 1000);
            return Task.Delay(delayTime);
        }
    }
}