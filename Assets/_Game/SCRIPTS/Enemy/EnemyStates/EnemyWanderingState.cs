using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderingState : State
{
    private StateMachine _stateMachine;
    private Enemy _enemy;

    public EnemyWanderingState(StateMachine stateMachine, Enemy enemy): base(stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.TurnAttackComponentsOff();
        //agent.isStopped = false;
        _enemy.Animator.SetBool("Attack", false);
        _enemy.Animator.SetBool("Walk", true);
        _enemy.Agent.destination = _enemy.CurrentDestination.position;
        if(!_enemy.IsBlind) _enemy.StateHandler.StartCheck();

        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        _enemy.RemDist = _enemy.Agent.remainingDistance;

        if (_enemy.Agent.remainingDistance != 0 & _enemy.Agent.remainingDistance <= 0.8f & _enemy.Agent.remainingDistance != float.PositiveInfinity & _enemy.Agent.remainingDistance != float.NegativeInfinity)
        {
            _enemy.CurrentDestination = _enemy.CurrentDestination == _enemy.endTransform ? _enemy.startTransform : _enemy.endTransform;
            _enemy.Agent.destination = _enemy.CurrentDestination.position;
        }

        if (_enemy.EnemySight.isPlayerInSteeringDistance)
        {
            stateMachine.SetState<EnemySteeringState>();
        }
    }
}
