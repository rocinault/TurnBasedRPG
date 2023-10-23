using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    public class StateMachine<T> : IEqualityComparer<T> where T : Enum
    {
        private readonly Dictionary<T, IState<T>> _states;
        private IState<T> _currentState => _states[_currentStateID];

        private T _currentStateID;

        public StateMachine(IState<T>[] states, T stateID)
        {
            _states = new Dictionary<T, IState<T>>(this);

            Bind(states);

            _currentStateID = stateID;
        }

        private void Bind(IState<T>[] states)
        {
            foreach (var state in states)
            {
                _states.Add(state.UniqueID, state);
            }
        }

        public void OnEnter()
        {
            _currentState.OnEnter();
        }

        public void OnUpdate()
        {
            _currentState.OnUpdate();
        }

        public void ChangeState(T stateID)
        {
            if (_states.ContainsKey(stateID) && !Equals(_currentStateID, stateID))
            {
                _currentState.OnExit();
                _currentStateID = stateID;
                _currentState.OnEnter();
            }
        }

        public void Dispose()
        {
            _currentState.OnExit();
            _states.Clear();
        }

        public bool Equals(T x, T y)
        {
            return x.GetHashCode() == y.GetHashCode();
        }

        public int GetHashCode(T obj)
        {
            return Convert.ToInt32(obj);
        }
    }
}
