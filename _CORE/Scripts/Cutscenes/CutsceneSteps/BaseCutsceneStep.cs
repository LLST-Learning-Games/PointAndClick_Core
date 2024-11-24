using System.Threading.Tasks;
using UnityEngine;

namespace Cutscenes
{
    public abstract class BaseCutsceneStep : MonoBehaviour
    {
        [SerializeField] private bool _waitForStepToComplete = true;
        [SerializeField] public bool BlockInput = true;

        public CutsceneStepState StepState { get; private set; } = CutsceneStepState.WaitingToStart;

        protected CutsceneControlSystem _parentSystem;

        public async Task TriggerStep(CutsceneControlSystem parentSystem)
        {
            _parentSystem = parentSystem;
            Debug.Log($"[{GetType().Name}] Starting custcene step...");
            StepState = CutsceneStepState.Running;
            await StepBehavior();
            StepState = CutsceneStepState.Completed;
            Debug.Log($"[{GetType().Name}] Finished custcene step!");
        }

        public abstract Task StepBehavior();

    }

    public enum CutsceneStepState
    {
        None,
        WaitingToStart,
        Running,
        Completed
    }
}