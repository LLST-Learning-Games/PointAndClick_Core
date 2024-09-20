using UnityEngine;
using UnityEngine.Events;

public class DoThingAfterSeconds : MonoBehaviour
{
    [SerializeField] private float _timeUntilDoThing = 1f;
    [SerializeField] private UnityEvent _thingToDo;
    private float _timeLeft;
    private bool _isCounting = false;

    public void ResetCounter()
    {
        _timeLeft = _timeUntilDoThing;
        _isCounting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCounting && _timeLeft < 0)
        {
            _thingToDo?.Invoke();
            _isCounting = false;
        }
        _timeLeft -= Time.deltaTime;
    }
}
