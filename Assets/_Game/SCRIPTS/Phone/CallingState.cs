using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;



public class CallingState : PhoneState
{    
    private Smartphone _smartphone;

    
    //public void Construct(Smartphone smartphone)
    //{
    //    _smartphone = smartphone;
    //}



    public CallingState(PhoneStateMachine stateMachine, Smartphone smartphone) : base(stateMachine)
    {
        _smartphone = smartphone;
    }

    public override void Enter()
    {

        Debug.Log($"Accessed to  {_smartphone.name}");
        Debug.Log("Phone Calling Enter");
        _smartphone.Call();
    }

    public override void Exit()
    {
        Debug.Log("Phone Calling Exit");
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
        Debug.Log("Phone Calling Update");
    }
}
