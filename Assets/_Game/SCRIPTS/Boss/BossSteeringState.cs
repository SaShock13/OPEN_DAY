using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;
using Zenject;

public class BossSteeringState : BossState
{
    private BossEnemy _boss;
    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform _target;       

    public BossSteeringState(BossStateMachine stateMachine, BossEnemy boss, Transform target,NavMeshAgent agent) : base(stateMachine)
    {
        _agent = agent;
        _boss = boss;
        _animator = boss.GetComponentInChildren<Animator>();
        _target = target;

    }

    public override void Enter()
    {
        _animator.SetBool("Walk", true);

        Debug.Log($"Enter Steering {this}");
    }

    public override void Update()
    {
        _agent.SetDestination(_target.position);
    }

    public override void Exit()
    {
        _animator.SetBool("Walk", false);
    }

}
