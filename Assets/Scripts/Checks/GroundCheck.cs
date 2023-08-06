using UnityEngine;

namespace Checks
{
    public class GroundCheck : MonoBehaviour
    {
        public bool OnGround { get; private set; }
        public float Friction { get; private set; }

        private void OnCollisionEnter2D(Collision2D other)
        {
            EvaluateCollision(other);
            RetrieveFriction(other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            OnGround = false;
            Friction = 0;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            EvaluateCollision(other);
            RetrieveFriction(other);
        }

        private void EvaluateCollision(Collision2D collision)
        {
            foreach (var contact in collision.contacts)
            {
                var normal = contact.normal;
                OnGround |= normal.y >= 0.9f;
            }
        }

        private void RetrieveFriction(Collision2D collision)
        {
            var material = collision.rigidbody.sharedMaterial;
            if (material != null)
                Friction = material.friction;
        }
    }
}