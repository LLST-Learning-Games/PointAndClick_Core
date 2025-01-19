using Cutscenes;
using System.Threading.Tasks;
using UnityEngine;

namespace Custscenes
{
    // Turns off an animator so it relinquishes control of an animated object. 
    // This is a hacky solution - let's find something better.
    public class CutsceneStep_DeactivateAnimator : BaseCutsceneStep
    {
        [SerializeField] private Animator _animatorToDeactivate;
        public override Task StepBehavior()
        {
            _animatorToDeactivate.enabled = false;
            return Task.CompletedTask;
        }
    }
}