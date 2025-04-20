using UnityEngine;
using UnityEngine.AI;

public class BossSteeringState : BossState
{
    private BossSMMono _bossSM;

    public BossSteeringState(BossStateMachine stateMachine, BossSMMono bossSM) : base(stateMachine)
    {
        _bossSM = bossSM;
    }

    public override void Enter()
    {
        _bossSM.BossAnimator.SetBool("Walk", true);
    }

    public override void Update()
    {
        _bossSM.Agent.SetDestination(_bossSM.target.position);
        if (_bossSM.EnemySight.isPlayerInAttackDistance)
        {
            stateMachine.SetState<BossAttackState>();
        }
        else if (!_bossSM.EnemySight.isPlayerInSteeringDistance)
        {
            stateMachine.SetState<BossWanderingState>();
        }
    }

    public override void Exit()
    {
        _bossSM.BossAnimator.SetBool("Walk", false);
    }
}
