using Checks;
using Controllers;
using UnityEngine;

namespace Capabilities
{
    public class JumpCapability : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
        
        [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
        [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
        [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
        [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;

        private Controller _controller;
        private Rigidbody2D _body;
        private GroundCheck _ground;
        private Vector2 _velocity;

        private int _jumpPhase;
        private float _defaultGravityScale, _jumpSpeed;

        private bool _desiredJump, _onGround;


        // Start is called before the first frame update
        void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<GroundCheck>();
            _controller = GetComponent<Controller>();

            _defaultGravityScale = 1f;
        }

        // Update is called once per frame
        void Update()
        {
            _desiredJump |= _controller.input.RetrieveJumpInput();
            
            //Update vertical velocity for animator
            animator.SetFloat(VerticalVelocity, _body.velocity.y);
        }

        private void FixedUpdate()
        {
            _onGround = _ground.OnGround;
            _velocity = _body.velocity;

            if (_onGround)
                _jumpPhase = 0;

            if (_desiredJump)
            {
                _desiredJump = false;
                JumpAction();
            }

            if (_body.velocity.y > 0)
            {
                _body.gravityScale = upwardMovementMultiplier;
            }
            else if (_body.velocity.y < 0)
            {
                _body.gravityScale = downwardMovementMultiplier;
            }
            else if (_body.velocity.y == 0)
            {
                _body.gravityScale = _defaultGravityScale;
            }

            _body.velocity = _velocity;
        }

        private void JumpAction()
        {
            if (_onGround || _jumpPhase < maxAirJumps)
            {
                _jumpPhase += 1;

                _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);

                if (_velocity.y > 0f)
                {
                    _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
                }
                else if (_velocity.y < 0f)
                {
                    _jumpSpeed += Mathf.Abs(_body.velocity.y);
                }

                _velocity.y += _jumpSpeed;
            }
        }
    }
}