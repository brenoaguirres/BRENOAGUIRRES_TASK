using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class Pausing : PlayerState
    {
        #region CONSTRUCTOR
        public Pausing(PlayerContext context, PlayerFSM.EPlayerState key) : base(context, key) { }
        #endregion

        #region STATE IMPLEMENTATION
        public override void EnterState()
        {
            _context.Inventory.OpenMenu();
        }
        public override void UpdateState() { }
        public override void FixedUpdateState() { }
        public override void ExitState() 
        {
            _context.Inventory.CloseMenu();
        }
        public override PlayerFSM.EPlayerState GetNextState()
        {
            if (_context.Inputs.Menu)
            {
                return PlayerFSM.EPlayerState.Idle;
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