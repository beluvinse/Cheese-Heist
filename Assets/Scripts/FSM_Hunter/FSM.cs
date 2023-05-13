using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    State _currentState;

    Dictionary<HunterStates, State> _allStates = new Dictionary<HunterStates, State>();


    public void AddState(HunterStates key, State state)
    {
        if (state == null) return;

        _allStates.Add(key, state);
        state.fsm = this;
    }

    public void ChangeState(HunterStates key)
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
