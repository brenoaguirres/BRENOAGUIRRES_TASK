using UnityEngine;

namespace Player
{
    public class Interacting : PlayerState
    {
        #region CONSTRUCTOR
        public Interacting(PlayerContext context, PlayerFSM.EPlayerState key) : base(context, key) { }
        #endregion

        #region STATE IMPLEMENTATION
        public override void EnterState()
        {
            Debug.Log("Interacting with object");
        }
        public override void UpdateState() { }
        public override void FixedUpdateState() { }
        public override void ExitState() { }
        public override PlayerFSM.EPlayerState GetNextState()
        {
            return PlayerFSM.EPlayerState.Idle;
        }
        public override void OnTriggerEnter(Collider other) { }
        public override void OnTriggerStay(Collider other) { }
        public override void OnTriggerExit(Collider other) { }
        public override void OnCollisionEnter(Collision collision) { }
        public override void OnCollisionStay(Collision collision) { }
        public override void OnCollisionExit(Collision collision) { }
        #endregion
    }
}
