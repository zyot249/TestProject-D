using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(menuName = "InputController/PlayerController")]
    public class PlayerController : InputController
    {
        private const string HorizontalInput = "Horizontal";
        private const string JumpInput = "Jump";
        private const string DashInput = "Dash";
        
        public override float RetrieveHorizontalMoveInput()
        {
            return Input.GetAxisRaw(HorizontalInput);
        }

        public override bool RetrieveJumpInput()
        {
            return Input.GetButtonDown(JumpInput);
        }

        public override bool RetrieveDashInput()
        {
            return Input.GetButtonDown(DashInput);
        }
    }
}