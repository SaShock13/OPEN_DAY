using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PhoneButton : MonoBehaviour
{
    [SerializeField] private UnityEvent onButtonClick;
    private Smartphone _smartphone;

    [Inject]
    public void Construct(Smartphone smartphone)
    {
        _smartphone = smartphone;
    }

    public void SetIddle()
    {
        _smartphone.machineMono.SetState<IddleState>();
    }

    public void SetTalk()
    {
        _smartphone.machineMono.SetState<TalkState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            onButtonClick.Invoke(); 
        }
    }
}
