using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossDeathState : BossState
{
    private BossEnemy _boss;
    private Animator _animator;
    private NavMeshAgent _agent;
    private EnemySight _enemySight;

    public BossDeathState(BossStateMachine stateMachine, BossEnemy boss, NavMeshAgent agent, EnemySight enemySight) : base(stateMachine)
    {
        _boss = boss;
        _animator = _boss.GetComponentInChildren<Animator>();
        _agent = agent;
        _enemySight = enemySight;
    }

    public override void Enter()
    {
        _agent.isStopped = true;
        _animator.SetTrigger("Death");
        Debug.Log($"Enter DeathState {this}");
        _enemySight.StopCheck();
        
    }

}
