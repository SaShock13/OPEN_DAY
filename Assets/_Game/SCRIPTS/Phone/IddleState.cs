using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IddleState : State
{
    private Smartphone phone;
    public IddleState(StateMachine stateMachine, Smartphone phone) : base(stateMachine)
    {
        this.phone = phone;
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            stateMachine.SetState<CallingState>();
        }
        //Debug.Log("Phone Iddle Update");
    }
}
