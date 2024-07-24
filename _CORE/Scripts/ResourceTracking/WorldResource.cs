using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldResource : MonoBehaviour
{
    [SerializeField] public string ResourceName;
    [SerializeField] private Slider _resourceSlider;
    [SerializeField] private float _resourceBurnRate = 0.005f;
    [SerializeField] [Range(0f,1f)] private float _currentResource = 1.0f;

    void Update()
    {
        _currentResource = Mathf.Clamp01(_currentResource);
        _currentResource -= _resourceBurnRate * Time.deltaTime;
        _resourceSlider.value = _currentResource;
    }

    public void AddResource(float addAmount)
    {
        _currentResource += addAmount;
    }
    public void RemoveResource(float removeAmount)
    {
        _currentResource -= removeAmount;
    }
    public float GetResourceAmount => _currentResource;
}
