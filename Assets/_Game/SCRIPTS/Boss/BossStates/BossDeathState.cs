
using UnityEngine;
using UnityEngine.AI;

public class BossDeathState : BossState
{
    private BossEnemy _boss;
    private Animator _animator;
    private NavMeshAgent _agent;
    private EnemySight _enemySight;
    private BossSMMono _bossSM;
    public BossDeathState(BossStateMachine stateMachine, BossSMMono bossSM) : base(stateMachine)
    {
        _bossSM = bossSM;
        //_boss = boss;
        //_animator = _boss.GetComponentInChildren<Animator>();
        //_agent = agent;
        //_enemySight = enemySight;
    }

    public override void Enter()
    {
        _bossSM.Agent.isStopped = true;
        _bossSM.BossAnimator.SetTrigger("Death");
        Debug.Log($"Enter DeathState {this}");
        _bossSM.EnemySight.StopCheck();        
    }
}
