using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : State
{
    private StateMachine _stateMachine;
    private Enemy _enemy;

    public EnemyDeathState(StateMachine stateMachine, Enemy enemy) : base(stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.TurnAttackComponentsOff();
        _enemy.ClearAnimationState();
        _enemy.Death();
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
