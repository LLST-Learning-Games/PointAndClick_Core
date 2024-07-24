using UnityEngine;
using Pathfinding;
using UnityEngine.EventSystems;
using PointAndClick.Interactable;

namespace PointAndClick.Player {
	public class PlayerInput : MonoBehaviour {
        [SerializeField] private Transform _movementTarget;
		[SerializeField] private AILerp _playerAi;

        private Camera _cam;
		private InteractableController _currentInteractable;


        public void Start () {
			_cam = Camera.main;
		}


		void Update () {
            if (Input.GetMouseButtonDown(0))
            {
                if (PlayerInputLock.IsPlayerInputLocked  // block input if minigame is loaded - todo - probably should exit if we're clicking off the UI
                    || EventSystem.current.IsPointerOverGameObject()    // early exit if over UI
                    )
                {
                    return;     
                }

                Vector3 newPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
                newPosition.z = _cam.transform.position.z;

                var hit = Physics2D.Raycast(newPosition, Vector2.zero);
                if (hit && hit.collider.gameObject.layer == 6)
                {
                    _currentInteractable = hit.collider.gameObject.GetComponent<InteractableController>();
                    _currentInteractable.OnInteractionBegin();

                    if (Vector3.Distance(_currentInteractable.WalkToPosition, transform.position) > Mathf.Epsilon)
                    {
                        UpdateTargetPosition(_currentInteractable.WalkToPosition);
                        _playerAi.OnDestinationReached += OnDestinationReached;
                    }
                    else
                    {
                        OnDestinationReached();
                    }
                    return;
                }

                if (!hit || hit && hit.collider.gameObject.layer == 3)
                {
                    CancelInteraction();

                    UpdateTargetPosition(newPosition);
                }
            }
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
