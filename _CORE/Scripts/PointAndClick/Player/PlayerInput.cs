using UnityEngine;
using Pathfinding;
using UnityEngine.EventSystems;
using PointAndClick.Interactable;
using PointAndClick.Conversation;
using SystemManagement;
using Inventory;
using System;
using System.Collections.Generic;

namespace PointAndClick.Player {
	public class PlayerInput : MonoBehaviour {
        [SerializeField] private Transform _movementTargetPrefab;
        [SerializeField] private AIDestinationSetter _destinationSetter;
		[SerializeField] private AILerp _playerAi;

        private Camera _cam;
		private InteractableController _currentInteractable;
        private Transform _movementTarget;

        // todo - move this to a subcomponent
        private InventorySystem _inventorySystem;

        public void Start () {
			_cam = Camera.main;
            _inventorySystem = GameSystemManager.Instance.GetSystem<InventorySystem>("Inventory");
            _movementTarget = Instantiate(_movementTargetPrefab, transform.root);
            _destinationSetter.target = _movementTarget;
		}

		void Update ()
        {
            var InputType = GetInputType();
            HandleInput(InputType);
            HandleInventoryCursor();
        }

        private void HandleInventoryCursor()
        {
            if (_inventorySystem.IsItemSelected)
            {
                _inventorySystem.UpdateCursor(Input.mousePosition);
            }
        }

        private PlayerInputType GetInputType()
        {

            if (Input.GetMouseButtonDown(0))
            {
                //_inventorySystem.ClearQueuedItem();
                if (_inventorySystem.IsItemSelected)
                {
                    _inventorySystem.QueueSelectedItemForUse();
                    return PlayerInputType.Inventory;
                }
                return PlayerInputType.Interact;
            }

            if (Input.GetMouseButtonDown(1))
            {
                //_inventorySystem.ClearQueuedItem();
                if (_inventorySystem.IsItemSelected)
                {
                    _inventorySystem.DeselectItem();
                }
                return PlayerInputType.LookAt;
            }

            return PlayerInputType.NoInput;
        }

        private void HandleInput(PlayerInputType inputType)
        {
            if (CheckForEarlyExits(inputType))
            {
                return;
            }

            GetClickMetrics(out Vector3 newPosition, out RaycastHit2D hit);

            var foundInteractable = HandleInteractableHit(inputType, hit);

            if (!foundInteractable && inputType != PlayerInputType.LookAt)
            {
                HandleWalkToHit(newPosition, hit);
            }
        }

        private bool HandleInteractableHit(PlayerInputType inputType, RaycastHit2D hit)
        {
            if (hit && hit.collider.gameObject.layer == 6)
            {
                _currentInteractable = hit.collider.gameObject.GetComponent<InteractableController>();
                _currentInteractable.OnInteractionBegin(inputType);

                if (_inventorySystem.IsItemSelected)
                {
                    _inventorySystem.DeselectItem();
                }

                if (inputType != PlayerInputType.LookAt &&
                    Vector3.Distance(_currentInteractable.WalkToPosition, transform.position) > Mathf.Epsilon)
                {
                    UpdateTargetPosition(_currentInteractable.WalkToPosition);
                    _playerAi.OnDestinationReached += OnDestinationReached;
                }
                else
                {
                    OnDestinationReached();
                }
                return true;
            }
            return false;
        }

        private void HandleWalkToHit(Vector3 newPosition, RaycastHit2D hit)
        {
            if (!hit || hit && hit.collider.gameObject.layer == 3)
            {
                CancelInteraction();

                UpdateTargetPosition(newPosition);
            }
        }


        private void GetClickMetrics(out Vector3 newPosition, out RaycastHit2D hit)
        {
            newPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = _cam.transform.position.z;

            hit = Physics2D.Raycast(newPosition, Vector2.zero);
        }

        private bool CheckForEarlyExits(PlayerInputType inputType)
        {
            if (inputType == PlayerInputType.NoInput)
            {
                return true;
            }

            if (PlayerInputLock.IsPlayerInputLocked  // block input if minigame is loaded - todo - probably should exit if we're clicking off the UI
                || EventSystem.current.IsPointerOverGameObject()    // early exit if over UI
                )
            {
                return true;
            }

            return false;
        }


        private void CancelInteraction()
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.OnInteractionAbandoned();
                _playerAi.OnDestinationReached -= OnDestinationReached;
                _currentInteractable = null;
            }
        }

        private void OnDestinationReached()
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.OnInteractionExecute();
                _playerAi.OnDestinationReached -= OnDestinationReached;
                _currentInteractable = null;
            }
        }

        public void UpdateTargetPosition (Vector3 newPosition) {

            newPosition.z = 0f;
            if (newPosition != _movementTarget.position) {
                _movementTarget.position = newPosition;
                _playerAi.SearchPath();
			}
		}
	}
}
