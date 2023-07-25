using UnityEngine;

namespace Controllers
{
    public class Character2DController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float horizontalSpeed = 12f;
        [SerializeField] private float gravity = -98f;
        [SerializeField] private float jumpVelocity = 25f;
        [SerializeField] private float dashDistance = 15f;

        private float _lastDirection = 0;
        private float _horizontalVelocity = 0;
        private float _verticalVelocity = 0;
        private bool _isOnGround = false;
        private bool _isOnJump = false;
        private bool _isOnDash = false;
        private static readonly int Speed = Animator.StringToHash("Speed");

        void Start()
        {
        }

        void Update()
        {
            _horizontalVelocity = _lastDirection = InputSystem.HorizontalRaw() * horizontalSpeed;
            animator.SetFloat(Speed, Mathf.Abs(_horizontalVelocity));

            if (_horizontalVelocity < 0)
                transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            else if (_horizontalVelocity > 0)
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            
            if (InputSystem.Jump() && !_isOnJump)
            {
                _verticalVelocity += jumpVelocity;
                _isOnJump = true;
            }
            else if (InputSystem.Dash() && !_isOnDash)
            {
                _isOnDash = true;
            }
        }

        void FixedUpdate()
        {
            if (_isOnDash)
            {
                _horizontalVelocity *= dashDistance;
                _isOnDash = false;
            }
            var x = _horizontalVelocity * Time.fixedDeltaTime;
            _verticalVelocity += gravity * Time.fixedDeltaTime;
            
            if (_isOnGround && !_isOnJump)
                _verticalVelocity = 0;
            
            var y = _verticalVelocity * Time.fixedDeltaTime;
            
            transform.position += new Vector3(x, y, 0);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _isOnGround = true;
            _isOnJump = false;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            _isOnGround = true;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _isOnGround = false;
        }
    }
}