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

    public BossWanderingState(BossStateMachine stateMachine, BossEnemy boss, Transform first, Transform second,NavMeshAgent agent) : base(stateMachine)
    {
        firstPointTransform = first;
        secondPointTransform = second;
        _boss = boss;
        _animator = _boss.GetComponentInChildren<Animator>();
        _agent = agent;
        
    }
    public override void Enter()
    {
        _animator.SetBool("Walk", true); 
        currentDestination = firstPointTransform;
        _agent.SetDestination(currentDestination.position);
    }
    public override void Exit()
    {
        _animator.SetBool("Walk", false);
    }

    public override void Update()
    {        
        if (_agent.remainingDistance != 0 & _agent.remainingDistance <= 0.3f & _agent.remainingDistance != float.PositiveInfinity & _agent.remainingDistance != float.NegativeInfinity)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        if (currentDestination == firstPointTransform)
        {
            currentDestination = secondPointTransform;
        }
        else 
        {
            currentDestination = firstPointTransform;
        }
        _agent.SetDestination(currentDestination.position);
    }
}
