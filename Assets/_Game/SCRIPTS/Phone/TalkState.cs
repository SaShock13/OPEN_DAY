using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkState : PhoneState
{
    private Smartphone _phone;            
    public TalkState(PhoneStateMachine stateMachine, Smartphone phone) : base(stateMachine)
    {
        _phone = phone;
    }

    public override void Enter()
    {
        _phone.AnswerCall();
        Debug.Log("Phone Talk Enter");
    }

    public override void Exit()
    {
        Debug.Log("Phone Talk Exit");
        _phone.StopTalk();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            stateMachine.SetState<IddleState>();
        }
        Debug.Log("Phone Talk Update");
    }
}
