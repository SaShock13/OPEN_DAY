using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;



public class CallingState : State
{    
    private Smartphone _smartphone;

    public CallingState(StateMachine stateMachine, Smartphone smartphone) : base(stateMachine)
    {
        _smartphone = smartphone;
    }

    public override void Enter()
    {
        _smartphone.Call();
    }

    public override void Exit()
    {
        _smartphone.StoptCall();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            stateMachine.SetState<IddleState>();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            stateMachine.SetState<TalkState>();
        }
    }
}
