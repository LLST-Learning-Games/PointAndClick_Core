using Pathfinding;
using UnityEngine;

namespace PointAndClick.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _is8Dir = false;
        [SerializeField] private float _updateFreq = 0.1f;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private AILerp _aiLerp;
        private bool _isMoving = false;
        private float _lastUpdateTick = 0;

        // Start is called before the first frame update
        void Start()
        {
            BindComponents();

            _aiLerp.OnDestinationReached += OnPathEnd;
            _aiLerp.OnSearchPath += OnPathStarted;
        }

        [ContextMenu("Bind Components")]
        public void BindComponents()
        {
            if (_animator is null)
            {
                _animator = GetComponent<Animator>();
            }

            if (_aiLerp is null)
            {
                _aiLerp = GetComponent<AILerp>();
            }

            if (_sprite is null)
            {
                _sprite = GetComponent<SpriteRenderer>();
            }
        }

        private void Update()
        {
            if (_is8Dir)
            {
                _lastUpdateTick += Time.deltaTime;
                if(_lastUpdateTick > _updateFreq)
                {
                    _animator.SetFloat("DirX", _aiLerp.velocity.normalized.x);
                    _animator.SetFloat("DirY", _aiLerp.velocity.normalized.y);
                    _lastUpdateTick = 0.0f;
                }
                //_animator.SetFloat("DirX", _aiLerp.destination.x - transform.position.x);
                //_animator.SetFloat("DirY", _aiLerp.destination.y - transform.position.y);
                return;
            }
            if (_isMoving)
            {
                if (_aiLerp.destination.x > transform.position.x)
                {
                    _sprite.flipX = false;
                }
                else
                {
                    _sprite.flipX = true;
                }
            }
        }

        private void OnPathStarted()
        {
            if (!_isMoving)
            {
                if (_is8Dir)
                {
                    // todo
                }
                else
                {
                    _animator.SetTrigger("StartWalk");
                }
                _isMoving = true;
            }
        }

        private void OnPathEnd()
        {
            if (_isMoving)
            {
                if (_is8Dir)
                {
                    // todo
                }
                else
                {
                    _animator.SetTrigger("StartIdle");
                }
                _isMoving = false;
            }
        }
    }
}