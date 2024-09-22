using PointAndClick.Interactables;
using PointAndClick.Player;
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
        [SerializeField] private List<InteractionInputTypeRelationship> _typeRelationships;

        private Color _startingColor;
        private Color _highlightColor = Color.yellow;
        private bool _interactionActive = false;
        private PlayerInputType _currentInputType = PlayerInputType.NoInput;
        private readonly Dictionary<PlayerInputType, BaseInteractableBridge> _interactableControllers = new();

        public Vector3 WalkToPosition => _walkToPosition.transform.position;

        private void Start()
        {
            _startingColor = _highlightedSpriteRenderer.color;

            _interactableControllers.Clear();
            foreach(var typeRelationship in _typeRelationships)
            {
                _interactableControllers.Add(typeRelationship.InputType, typeRelationship.Interactable);
            }
        }
        public void OnInteractionBegin(PlayerInputType inputType)
        {
            _interactionActive = true;
            _highlightedSpriteRenderer.color = _highlightColor;
            _currentInputType = inputType;
        }

        public void OnInteractionExecute()
        {
            Debug.Log("You've arrived at your interactable!");
            if (_interactableControllers.TryGetValue(_currentInputType, out var interactable))
            {
                interactable.OnInteractionExecute();
            }
            ClearCurrentInteraction();
        }

        public void OnInteractionAbandoned()
        {
            Debug.Log("You've abandonded your interactable!");
            ClearCurrentInteraction();
        }

        private void ClearCurrentInteraction()
        {
            _highlightedSpriteRenderer.color = _startingColor;
            _interactionActive = false;
            _highlightedSpriteRenderer.enabled = false;
            _currentInputType = PlayerInputType.NoInput;
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
