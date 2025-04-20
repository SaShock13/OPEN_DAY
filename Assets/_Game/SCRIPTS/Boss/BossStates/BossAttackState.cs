using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class BossAttackState : BossState
{
    private string[] _attacksNames;
    private int rndAttackIndex;
    private BossSMMono _bossSM;

    public BossAttackState(BossStateMachine stateMachine, BossSMMono bossSM) : base(stateMachine)
    {
        _bossSM = bossSM;
        _attacksNames = new string[] { "Attack2", "KickAttack2" };
    }

    // todo как остановить при атаке?
    public override void Enter()
    {
        _bossSM.BossAttack.EndAttackEvent += EndAttack;
        _bossSM.Agent.isStopped = true;
        RandomAttack();
    }

    private void RandomAttack()
    {        
        rndAttackIndex = Random.Range(0, 2);
        _bossSM.BossAnimator.SetTrigger(_attacksNames[rndAttackIndex]);
    }

    private void EndAttack()
    {
        if (!_bossSM.EnemySight.isPlayerInAttackDistance)
        {
            stateMachine.SetState<BossWanderingState>();
        }
        else
        {
            RandomAttack();
        }
    }

    public override void Exit()
    {
        _bossSM.Agent.isStopped = false;
    }

    public override void Update()
    {
        base.Update();
    }
}
