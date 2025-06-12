using System;
using UnityEngine;

namespace Player
{
    public class Idle : PlayerState
    {
        #region CONSTRUCTOR
        public Idle(PlayerContext context, PlayerFSM.EPlayerState key) : base(context, key){ }
        #endregion

        #region STATE IMPLEMENTATION
        public override void EnterState() 
        {
            _context.Anim.SetFloat("Movement", _context.Inputs.Movement.magnitude);
        }
        public override void UpdateState() { }
        public override void FixedUpdateState() { }
        public override void ExitState() { }
        public override PlayerFSM.EPlayerState GetNextState() 
        { 
            if (_context.Inputs.Back)
            {
                return PlayerFSM.EPlayerState.Interacting;
            }

            if (_context.Inputs.Movement.magnitude > 0.01f)
            {
                return PlayerFSM.EPlayerState.Move;
            }

            return StateKey; 
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