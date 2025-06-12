/* Title: StateMachine.cs
 * Author: Breno Aguirres
 * Date: 2025-11-06
 * Description: Defines an abstract FSM (Finite State Machine) class that manages states and transitions between them. Extend this class to create a concrete FSM implementation.
*/

using System.Collections.Generic;
using System;
using UnityEngine;

namespace Patterns.FSM
{
    public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
    {
        #region FIELDS
        protected Dictionary<EState, State<EState>> States = new Dictionary<EState, State<EState>>();
        [SerializeField] protected State<EState> CurrentState;

        protected bool IsTransitioningState = false;
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            CurrentState.EnterState();
        }

        private void Update()
        {
            UpdateCycle(CurrentState.UpdateState);
        }

        private void FixedUpdate()
        {
            UpdateCycle(CurrentState.FixedUpdateState);
        }

        private void OnTriggerEnter(Collider other)
        {
            CurrentState.OnTriggerEnter(other);
        }

        private void OnTriggerStay(Collider other)
        {
            CurrentState.OnTriggerStay(other);
        }

        private void OnTriggerExit(Collider other)
        {
            CurrentState.OnTriggerExit(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            CurrentState.OnCollisionEnter(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            CurrentState.OnCollisionStay(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            CurrentState.OnCollisionExit(collision);
        }
        #endregion

        #region CUSTOM METHODS
        private void TransitionToState(EState stateKey)
        {
            IsTransitioningState = true;
            CurrentState.ExitState();
            CurrentState = States[stateKey];
            CurrentState.EnterState();
            IsTransitioningState = false;
        }

        private void UpdateCycle(Action updateMethod)
        {
            EState nextStateKey = CurrentState.GetNextState();
            if (!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
            {
                updateMethod();
            }
            else if (!IsTransitioningState)
            {
                TransitionToState(nextStateKey);
            }
        }
        #endregion
    }
}