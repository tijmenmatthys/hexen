using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StateMachineLib
{
    public class StateMachine<TStateID>
    {
        private Dictionary<TStateID, State<TStateID>> _states
            = new Dictionary<TStateID,State<TStateID>>();

        private List<TStateID> _activeStateIDs
            = new List<TStateID>();

        private TStateID _currentStateID;
        private State<TStateID> CurrentState => _states[_currentStateID];

        public void SetInitialStateID(TStateID initialStateID)
        {
            _currentStateID = initialStateID;
            _activeStateIDs.Add(initialStateID);
            CurrentState.OnEnter();
        }

        public void Register(TStateID stateID, State<TStateID> state)
        {
            state.StateMachine = this;
            _states[stateID] = state;
        }

        public void Transition(TStateID newStateID, bool exitOldState = false)
        {
            EndOldState(exitOldState);
            StartNewState(newStateID);
        }

        private void StartNewState(TStateID newStateID)
        {
            _currentStateID = newStateID;
            if (!_activeStateIDs.Contains(newStateID))
            {
                _activeStateIDs.Add(newStateID);
                CurrentState.OnEnter();
            }
            else
                CurrentState.OnResume();
        }

        private void EndOldState(bool exitOldState)
        {
            if (exitOldState)
            {
                CurrentState.OnExit();
                _activeStateIDs.Remove(_currentStateID);
            }
            else
                CurrentState.OnSuspend();
        }
    }
}
