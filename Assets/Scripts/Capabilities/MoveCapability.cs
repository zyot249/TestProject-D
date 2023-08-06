using Checks;
using Controllers;
using UnityEngine;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))]
    public class MoveCapability : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int HorizontalVelocity = Animator.StringToHash("HorizontalVelocity");

        [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
        [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
        [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

        private Controller _controller;
        private Vector2 _direction, _desiredVelocity, _velocity;
        private Rigidbody2D _body;
        private GroundCheck _ground;

        private float _maxSpeedChange, _acceleration;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<GroundCheck>();
            _controller = GetComponent<Controller>();
        }

        private void Update()
        {
            //Retrieve input
            _direction.x = _controller.input.RetrieveHorizontalMoveInput();

            //Update speed for animator
            animator.SetFloat(HorizontalVelocity, Mathf.Abs(_body.velocity.x));

            //Update direction of character
            if (_direction.x < 0)
                transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            else if (_direction.x > 0)
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }

        private void FixedUpdate()
        {
            _velocity = _body.velocity;

            _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(maxSpeed - _ground.Friction, 0f);
            _velocity.x = _desiredVelocity.x;

            _body.velocity = _velocity;
        }
    }
}