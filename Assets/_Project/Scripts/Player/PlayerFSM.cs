using Patterns.FSM;
using UnityEngine;
using UnityEngine.Assertions;

namespace Player
{
    public class PlayerFSM : StateMachine<PlayerFSM.EPlayerState>
    {
        #region STATES ENUM
        public enum EPlayerState
        {
            Idle,
            Move,
            Interacting,
        }
        #endregion

        #region FIELDS
        private PlayerContext _context;
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            InitializeContext();
            InitializeStates();
        }
        #endregion

        #region CUSTOM METHODS
        private void InitializeContext()
        {
            _context = GetComponent<PlayerContext>();
            Assert.IsNotNull(_context, $"{nameof(PlayerContext)} context is not assigned.");
            _context.PlayerStateMachine = this;
        }

        private void InitializeStates()
        {
            States.Add(EPlayerState.Idle, new Idle(_context, EPlayerState.Idle));
            States.Add(EPlayerState.Move, new Moving(_context, EPlayerState.Move));
            States.Add(EPlayerState.Interacting, new Interacting(_context, EPlayerState.Interacting));

            CurrentState = States[EPlayerState.Idle];
        }
        #endregion
    }
}