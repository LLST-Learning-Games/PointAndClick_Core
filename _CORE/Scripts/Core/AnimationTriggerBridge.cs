using UnityEngine;

public class AnimationTriggerBridge : MonoBehaviour
{
    [SerializeField] private Animation _animation;

    public void PlayAnimation() => _animation.Play();
}
