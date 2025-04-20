using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine 
{
    public BossState CurrentState { get; private set; }

    private Dictionary<Type, BossState> _states = new Dictionary<Type, BossState>();

    public void AddState(BossState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : BossState
    {
        Type type = typeof(T);

        if (CurrentState != null && CurrentState.GetType() == type)
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
