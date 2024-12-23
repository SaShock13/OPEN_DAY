using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IddleState : PhoneState
{
    private Smartphone phone;
    public IddleState(PhoneStateMachine stateMachine, Smartphone phone) : base(stateMachine)
    {
        this.phone = phone;
    }

    public override void Enter()
    {
        Debug.Log("Phone Iddle Enter");
    }

    public override void Exit()
    {
        Debug.Log("Phone Iddle Exit");
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            stateMachine.SetState<CallingState>();
        }
        Debug.Log("Phone Iddle Update");
    }
}
