using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhoneState
{
    protected PhoneStateMachine stateMachine;

    protected PhoneState(PhoneStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }
    public virtual void Update()
    {

    }

}
