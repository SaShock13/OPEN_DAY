using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    protected BossStateMachine stateMachine;
    protected Animator _animator;

    protected BossState(BossStateMachine stateMachine)
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
