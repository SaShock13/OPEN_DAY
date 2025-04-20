using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIddleState : BossState
{
    private BossSMMono _bossSM;
    public BossIddleState(BossStateMachine stateMachine,BossSMMono bossSM) : base(stateMachine)
    {
        _bossSM = bossSM;
    }

    public override void Enter()
    {
        //_boss.GetComponentInChildren<Animator>().SetBool("Iddle",true);

        Debug.Log($"Enter BossIddleState {this}");
    }
}
