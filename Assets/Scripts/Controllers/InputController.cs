using UnityEngine;

namespace Controllers
{
    public abstract class InputController : ScriptableObject
    {
        public abstract float RetrieveHorizontalMoveInput();
        public abstract bool RetrieveJumpInput();
        public abstract bool RetrieveDashInput();
    }
}