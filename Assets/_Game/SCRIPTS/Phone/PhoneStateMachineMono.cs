using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PhoneStateMachineMono : MonoBehaviour
{
    private PhoneStateMachine _stateMachine;
    private Smartphone _smartphone;


    [Inject]
    public void Construct(Smartphone smartphone)
    {
        _smartphone = smartphone;
    }



    private void Start()
    {
        _stateMachine = new PhoneStateMachine();

        _stateMachine.AddState(new IddleState(_stateMachine, _smartphone));
        _stateMachine.AddState(new CallingState(_stateMachine, _smartphone));
        _stateMachine.AddState(new TalkState(_stateMachine, _smartphone));

        _stateMachine.SetState<IddleState>();
        StartCoroutine(DelayedCall());
    }


    private void Update()
    {
        _stateMachine.Update();
    }


    public void SetState<T>() where T : PhoneState
    {
        _stateMachine.SetState<T>();
    }

    IEnumerator DelayedCall()
    {
        yield return new WaitForSeconds(5);
        _stateMachine.SetState<CallingState>();
    }
}
