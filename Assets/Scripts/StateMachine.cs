using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystem.GameStates
{
    public class StateMachine<TStateID>
    {
        private Dictionary<TStateID, State> _states
            = new Dictionary<TStateID,State>();

        private List<TStateID> _activeStateIDs
            = new List<TStateID>();

        public TStateID CurrentStateID => _activeStateIDs.Last();
        private State CurrentState => _states[CurrentStateID];

        public void SetInitialStateID(TStateID initialStateID)
        {
            _activeStateIDs.Add(initialStateID);
            CurrentState.OnEnter();
        }

        public void Register(TStateID stateID, State state)
        {
            state.StateMachine = this;
            _states[stateID] = state;
        }

        // exit the previous state, start a new one
        public void Transition(TStateID newStateID)
        {
            CurrentState.OnExit();
            _activeStateIDs.Remove(CurrentStateID);
            _activeStateIDs.Add(newStateID);
        }
    }
}
