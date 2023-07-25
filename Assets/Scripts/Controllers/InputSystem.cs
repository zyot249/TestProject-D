using UnityEngine;

namespace Controllers
{
    public class InputSystem
    {
        private const string HorizontalInput = "Horizontal";
        private const string JumpInput = "Jump";
        private const string DashInput = "Dash";

        public static float HorizontalRaw()
        {
            return Input.GetAxisRaw(HorizontalInput);
        }

        public static bool Jump()
        {
            return Input.GetButtonDown(JumpInput);
        }

        public static bool Dash()
        {
            return Input.GetButtonDown(DashInput);
        }
    }
}
