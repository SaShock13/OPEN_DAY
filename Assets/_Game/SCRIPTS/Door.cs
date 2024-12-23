using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private SphereCollider doorCollider;

    private void Start()
    {
        doorCollider = GetComponent<SphereCollider>();
    }

    public void UnlockDoor()
    {
        doorCollider.enabled = false;
    }

    public void LockDoor()
    {
        {
            doorCollider.enabled = true;
        }
    }
}
