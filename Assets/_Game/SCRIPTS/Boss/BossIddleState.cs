using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIddleState : BossState
{
    private BossEnemy _boss;
    public BossIddleState(BossStateMachine stateMachine,BossEnemy boss) : base(stateMachine)
    {
        _boss = boss;
    }

    public override void Enter()
    {
        //_boss.GetComponentInChildren<Animator>().SetBool("Iddle",true);

        Debug.Log($"Enter BossIddleState {this}");
    }
}
