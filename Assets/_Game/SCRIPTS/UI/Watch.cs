using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Zenject;

public class Watch : MonoBehaviour
{

    // todo на rendermodel не обновляются показания
    [SerializeField] TMP_Text livesText;

    private IEnumerator Start()
    {
        while (true)
        {
            UpdateTime();
            yield return new WaitForSeconds(1); 
        }
    }

    private void UpdateTime()
    {
        livesText.text = DateTime.Now.ToString("T");
    }
}
