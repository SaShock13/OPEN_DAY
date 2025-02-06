using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class BossAttackState : BossState
{
    private BossEnemy _boss;
    private Animator _animator;
    private Player _player;
    private NavMeshAgent _agent;
    private string[] attacksNames;
    private int rndAttackIndex;

    public BossAttackState(BossStateMachine stateMachine, BossEnemy boss, Player player, NavMeshAgent agent) : base(stateMachine)
    {
        _boss = boss;
        _animator= _boss.GetComponentInChildren<Animator>();

        _player = player;
        _agent = agent;
        attacksNames = new string[] { "Attack", "KickAttack" };
    }
    public override void Enter()
    {
        _agent.isStopped = true;
        rndAttackIndex = Random.Range(0,2);
        _animator.SetBool(attacksNames[rndAttackIndex],true);
        Debug.Log($"Enter BossAttackState {this}");        
    }

    public override void Exit()
    {
        _animator.SetBool("Attack", false);
        _animator.SetBool("KickAttack", false);
        _agent.isStopped = false;
    }

}
