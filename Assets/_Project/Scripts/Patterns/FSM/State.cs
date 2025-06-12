/* Title: State.cs
 * Author: Breno Aguirres
 * Date: 2025-11-06
 * Description: Defines an abstract state class for a finite state machine (FSM). Extend this to create concrete logic for a specific concrete FSM.
*/

using UnityEngine;
using System;

namespace Patterns.FSM
{
    [Serializable]
    public abstract class State<EState> where EState : Enum
    {
        #region CONSTRUCTOR
        public State(EState key)
        {
            StateKey = key;
        }
        #endregion

        #region PROPERTIES
        public EState StateKey { get; private set; }
        #endregion

        #region CUSTOM METHODS
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void FixedUpdateState();
        public abstract void ExitState();
        public abstract EState GetNextState();
        public abstract void OnTriggerEnter(Collider other);
        public abstract void OnTriggerStay(Collider other);
        public abstract void OnTriggerExit(Collider other);
        public abstract void OnCollisionEnter(Collision collision);
        public abstract void OnCollisionStay(Collision collision);
        public abstract void OnCollisionExit(Collision collision);
        #endregion
    }
}