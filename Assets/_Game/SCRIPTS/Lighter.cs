using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Lighter : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean grabAction;
    //private FireSpreading fireSpreading;
    private ParticleSystem FireFX;
    private int previousLayer;
    [SerializeField] private LayerMask targetLayer;

    /// <summary>
    /// зажигалка не жжет!!Ё!
    /// </summary>
    private void Start()
    {
        FireFX = GetComponentInChildren<ParticleSystem>();
        //fireSpreading = GetComponent<FireSpreading>();
    }

    public void OnTake()
    {
        previousLayer = gameObject.layer;
        print($"PrevLayer {previousLayer}");
        print($"targetLayer {targetLayer}");
        gameObject.layer = 7;

    }

    public void OnDrop()
    {
        gameObject.layer = previousLayer;
    }

    public void OnLighterHeld()
    {
        FireSpreading fireSpreading;
        if (grabAction.state)
        {
            if (!FireFX.isPlaying)
                FireFX.Play();
            fireSpreading = gameObject.AddComponent<FireSpreading>();
            //fireSpreading.enabled = true;
        }
        else
        {
            if (FireFX.isPlaying)
            {
                FireFX.Stop();
            }
             if (TryGetComponent<FireSpreading> (out fireSpreading)) Destroy(fireSpreading);
            //fireSpreading.enabled = false;
        }
    }
}
