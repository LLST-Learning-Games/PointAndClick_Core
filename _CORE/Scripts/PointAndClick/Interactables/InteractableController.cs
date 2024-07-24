using PointAndClick.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PointAndClick.Interactable
{
    public class InteractableController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _highlightedSpriteRenderer;
        [SerializeField] private GameObject _walkToPosition;
        [SerializeField] private BaseInteractableBridge _interactableBridge;

        private Color _startingColor;
        private Color _highlightColor = Color.yellow;
        private bool _interactionActive = false;

        public Vector3 WalkToPosition => _walkToPosition.transform.position;

        private void Start()
        {
            _startingColor = _highlightedSpriteRenderer.color;
        }
        public void OnInteractionBegin()
        {
            _interactionActive = true;
            _highlightedSpriteRenderer.color = _highlightColor;
        }

        public void OnInteractionExecute()
        {
            Debug.Log("You've arrived at your interactable!");
            _highlightedSpriteRenderer.color = _startingColor;
            _interactionActive = false;
            _highlightedSpriteRenderer.enabled = false;
            _interactableBridge.OnInteractionExecute();
        }

        public void OnInteractionAbandoned()
        {
            Debug.Log("You've abandonded your interactable!");
            _highlightedSpriteRenderer.color = _startingColor;
            _interactionActive = false;
            _highlightedSpriteRenderer.enabled = false;
        }

        private void OnMouseEnter()
        {
            _highlightedSpriteRenderer.enabled = true;
        }

        private void OnMouseExit()
        {
            if (!_interactionActive)
            {
                _highlightedSpriteRenderer.enabled = false;
            }
        }
    }
}
