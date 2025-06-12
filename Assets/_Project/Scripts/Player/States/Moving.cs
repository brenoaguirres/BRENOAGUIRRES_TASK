using System;
using UnityEngine;

namespace Player
{
    public class Moving : PlayerState
    {
        #region CONSTRUCTOR
        public Moving(PlayerContext context, PlayerFSM.EPlayerState key) : base(context, key) { }
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
            if (_context.Inputs.Movement.magnitude <= 0.01f)
            {
                _context.Movement.SuddenStop(_context.Rb);
                return PlayerFSM.EPlayerState.Idle;
            }

            _context.Movement.MoveTowards(_context.Rb, _context.Inputs.Movement);
            _context.Movement.LookTowards(_context.Rb, _context.Inputs.Movement);

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