using System.Collections;
using System.Collections.Generic;
using GLTFast.Schema;
using UnityEngine;

public class EnemySteeringState : State
{
    private StateMachine _stateMachine;
    private Enemy _enemy;

    public EnemySteeringState(StateMachine stateMachine, Enemy enemy) : base(stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();        
        _enemy.Animator.SetBool("Walk", true);       
        _enemy.Agent.isStopped = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        _enemy.Agent.destination = _enemy.CurrentTargetTransform.position;
        if (_enemy.EnemySight.isPlayerInAttackDistance)
        {
            stateMachine.SetState<EnemyAttackState>();
        }
        else if (!_enemy.EnemySight.isPlayerInSteeringDistance)
        {
            stateMachine.SetState<EnemyWanderingState>();
        }
    }
}
