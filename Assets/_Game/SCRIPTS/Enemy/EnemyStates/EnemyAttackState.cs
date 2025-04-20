using System;
using System.Collections;
using System.Collections.Generic;
using GLTFast.Schema;
using UnityEngine;

public class EnemyAttackState : State
{
    private StateMachine _stateMachine;
    private Enemy _enemy;

    public EnemyAttackState(StateMachine stateMachine, Enemy enemy) : base(stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
       // _enemy.EndAttackEvent += EndAttack;
        _enemy.Agent.isStopped = true;        
        _enemy.transform.rotation = Quaternion.LookRotation(new Vector3(_enemy.CurrentTargetTransform.position.x, _enemy.transform.position.y, _enemy.CurrentTargetTransform.position.z) - _enemy.transform.position, Vector3.up);
        _enemy.TurnAttackComponentsOn();
        _enemy.Animator.SetTrigger("Attack");
        _enemy.GetComponentInChildren<SlimAttack>().EndAttackEvent += EndAttack;
    }

    private void EndAttack()
    {
        if (!_enemy.EnemySight.isPlayerInAttackDistance)
        {
            stateMachine.SetState<EnemySteeringState>();
        }
        else
        {
            _enemy.Animator.SetTrigger("Attack");
        }
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.Agent.isStopped = false;
        _enemy.TurnAttackComponentsOff();
    }

    public override void Update()
    {
        base.Update();       
    }
}
