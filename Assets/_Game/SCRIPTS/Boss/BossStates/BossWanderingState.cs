using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossWanderingState : BossState
{
    private BossEnemy _boss;
    private Animator _animator;
    private Transform firstPointTransform;
    private Transform secondPointTransform;
    private NavMeshAgent _agent;
    private Transform currentDestination;
    private EnemySight _enemySight;
    private BossSMMono _bossSM;
    public BossWanderingState(BossStateMachine stateMachine, BossSMMono bossSM) : base(stateMachine)
    {
        _bossSM = bossSM;
        //firstPointTransform = first;
        //secondPointTransform = second;
        //_boss = boss;
        //_animator = _boss.GetComponentInChildren<Animator>();
        //_agent = agent;
        //_enemySight = enemySight;
        
    }
    public override void Enter()
    {
        _bossSM.BossAnimator.SetBool("Walk", true);
        currentDestination = _bossSM.startPoint;
        _bossSM.Agent.SetDestination(currentDestination.position);
    }
    public override void Exit()
    {
        _bossSM.BossAnimator.SetBool("Walk", false);
    }

    public override void Update()
    {        
        if (_bossSM.Agent.remainingDistance != 0 & _bossSM.Agent.remainingDistance <= 0.3f & _bossSM.Agent.remainingDistance != float.PositiveInfinity & _bossSM.Agent.remainingDistance != float.NegativeInfinity)
        {
            ChangeDestination();
        }

        if (_bossSM.EnemySight.isPlayerInSteeringDistance)
        {
            stateMachine.SetState<BossSteeringState>();
        }
    }

    private void ChangeDestination()
    {
        if (currentDestination == _bossSM.startPoint)
        {
            currentDestination = _bossSM.finishPoint;
        }
        else 
        {
            currentDestination = _bossSM.startPoint;
        }
        _bossSM.Agent.SetDestination(currentDestination.position);
    }
}
