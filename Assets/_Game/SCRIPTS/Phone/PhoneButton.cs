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
        Debug.Log($"Constract method injected : {smartphone} to {_smartphone}");
    }

    public void SetIddle()
    {

        Debug.Log($"pushed button Cancel ");
        _smartphone.machineMono.SetState<IddleState>();
    }

    public void SetTalk()
    {
        Debug.Log($"pushed button Answer ");
        _smartphone.machineMono.SetState<TalkState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        onButtonClick.Invoke();
    }
}
