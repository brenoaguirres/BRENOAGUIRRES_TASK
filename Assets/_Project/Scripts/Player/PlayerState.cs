using System;
using UnityEngine;
using Patterns.FSM;

namespace Player
{
    public class PlayerState : State<PlayerFSM.EPlayerState>
    {
        #region CONSTRUCTOR
        public PlayerState(PlayerContext context, PlayerFSM.EPlayerState key) : base(key)
        {
            _context = context;
        }
        #endregion

        #region FIELDS
        protected PlayerContext _context;
        #endregion

        #region BASESTATE OVERRIDES
        public override void EnterState(){ }
        public override void UpdateState(){ }
        public override void FixedUpdateState(){ }
        public override void ExitState(){ }
        public override PlayerFSM.EPlayerState GetNextState(){ throw new NotImplementedException(); }
        public override void OnTriggerEnter(Collider other){ }
        public override void OnTriggerStay(Collider other){ }
        public override void OnTriggerExit(Collider other){ }
        public override void OnCollisionEnter(Collision collision){ }
        public override void OnCollisionStay(Collision collision){ }
        public override void OnCollisionExit(Collision collision){ }
        #endregion
    }
}