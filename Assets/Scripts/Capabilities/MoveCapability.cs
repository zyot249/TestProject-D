using Checks;
using Controllers;
using UnityEngine;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))]
    public class MoveCapability : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
        [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
        [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

        private Controller _controller;
        private Vector2 _direction, _desiredVelocity, _velocity;
        private Rigidbody2D _body;
        private GroundCheck _ground;

        private float _maxSpeedChange, _acceleration;
        private bool _onGround;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<GroundCheck>();
            _controller = GetComponent<Controller>();
        }

        private void Update()
        {
            _direction.x = _controller.input.RetrieveHorizontalMoveInput();
            _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(maxSpeed - _ground.Friction, 0f);
            
            animator.SetFloat(Speed, Mathf.Abs(_direction.x));
            if (_direction.x < 0)
                transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            else if (_direction.x > 0)
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }

        private void FixedUpdate()
        {
            _onGround = _ground.OnGround;
            _velocity = _body.velocity;

            _acceleration = _onGround ? maxAcceleration : maxAirAcceleration;
            _maxSpeedChange = _acceleration * Time.deltaTime;
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

            _body.velocity = _velocity;
        }
    }
}