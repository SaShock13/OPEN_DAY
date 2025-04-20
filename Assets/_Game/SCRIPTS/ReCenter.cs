using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ReCenter : MonoBehaviour
{
    List<XRInputSubsystem> xrInputSubsystems = new List<XRInputSubsystem>();

    void Awake()
    {
        SubsystemManager.GetInstances(xrInputSubsystems);
        Recenter();
    }


    /// <summary>
    /// Центрирование головы. Не очень работает)
    /// </summary>
    public void Recenter()
    {
        foreach (var xr in xrInputSubsystems)
        {
            xr.TryRecenter();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Recenter();
    }
}
