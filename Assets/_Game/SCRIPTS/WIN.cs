using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WIN : MonoBehaviour
{
    [SerializeField] private UnityEvent onWinGame;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        onWinGame?.Invoke();
    }    
}
