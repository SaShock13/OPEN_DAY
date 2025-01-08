using UnityEngine;
using UnityEngine.Events;

public class DoorLocker : MonoBehaviour
{
    [SerializeField] UnityEvent DoorUnlockEvent;
    [SerializeField] UnityEvent DoorLockEvent;
    [SerializeField] Color unlockedColor;
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        DoorLockEvent.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CodeCard"))
        {
            DoorUnlockEvent.Invoke(); 
            renderer.material.color = unlockedColor;
        }
    }
}
