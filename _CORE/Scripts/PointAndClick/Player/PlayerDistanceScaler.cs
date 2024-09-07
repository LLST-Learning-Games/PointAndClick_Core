using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PointAndClick.Player 
{
    public class PlayerDistanceScaler : MonoBehaviour
    {
        [SerializeField] private Transform _objectToScale;
        [SerializeField] private float _minimumScale;
        [SerializeField] private float _maximumScale;
        [SerializeField] private float _closestY = -1.3f;
        [SerializeField] private float _furthestY = 0.0f;

        private Vector3 _lastPos = Vector3.zero;

        private void Awake()
        {
            if(!_objectToScale)
            {
                var player = (PlayerInput)FindAnyObjectByType(typeof(PlayerInput));
                _objectToScale = player.transform;
            }    
        }

        void Update()
        {
            if (_lastPos != _objectToScale.position)
            {
                var yPos = _objectToScale.position.y;

                var rawScale = (yPos - _furthestY) / (_closestY - _furthestY);
                
                var scale = rawScale * (_maximumScale - _minimumScale) + _minimumScale;
                _objectToScale.localScale = new Vector3(scale, scale, scale);

                _lastPos = _objectToScale.position;

            }
        } 
    }
}
