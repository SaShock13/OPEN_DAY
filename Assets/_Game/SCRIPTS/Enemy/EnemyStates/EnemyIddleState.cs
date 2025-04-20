using System.Collections;
using System.Collections.Generic;
using GLTFast.Schema;
using UnityEngine;

public class EnemyIddleState : State
{
    private StateMachine _stateMachine;
    private Enemy _enemy;
    
    public EnemyIddleState(StateMachine stateMachine, Enemy enemy) : base(stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.ClearAnimationState();
        _enemy.Animator.SetTrigger("Idle");
    }

    public override void Exit() 
    { 
        base.Exit(); 
    }

    public override void Update()
    {
        base.Update();
    }
}
