using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneStateMachine
{
    public PhoneState CurrentState { get; private set; }

    private Dictionary<Type, PhoneState> _states = new Dictionary<Type, PhoneState>();


    public void AddState(PhoneState state)
    {
        _states.Add(state.GetType(),state);
    }

    public void SetState<T>() where T : PhoneState
    {
        Type type = typeof(T);

        if(CurrentState!=null && CurrentState.GetType() == type)
        {
            return;
        }
        if (_states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
    public void Update()
    {
        CurrentState?.Update();
    }



}
