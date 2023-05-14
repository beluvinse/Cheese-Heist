using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    State _currentState;

    Dictionary<States, State> _allStates = new Dictionary<States, State>();


    public void AddState(States key, State state)
    {
        if (state == null) return;

        _allStates.Add(key, state);
        state.fsm = this;
    }

    public void ChangeState(States key)
    {
        if (!_allStates.ContainsKey(key)) return;

        if (_currentState != null)
            _currentState.OnExit();

        _currentState = _allStates[key];
        _currentState.OnEnter();
    }

    public void Update()
    {
        _currentState.Update();
    }

    //Agregar FixedUpdate si se lo necesita
}
