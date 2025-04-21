using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;

public class HandLaser : MonoBehaviour
{
    private SteamVR_LaserPointer pointer;

    private void Start()
    {
        pointer = GetComponent<SteamVR_LaserPointer>();
        pointer.PointerClick += OnLaserClick;
    }

    private void OnLaserClick(object sender, PointerEventArgs e)
    {
        if(e.target.TryGetComponent<Button>(out Button button ))
        {
            button.onClick.Invoke();
        }
    }
}
