using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Lighter : MonoBehaviour
{

    //todo Поправить работу зажигалки, при отпускании зажженой, остается огонь
    //todo Поджиг идет только при вхождении, если уже в триггере зажигаешь не идет
    //todo Скоротсть поджига уменьшается от каждой попытки!
    [SerializeField] private SteamVR_Action_Boolean grabAction;
    private FireSpreading fireSpreading;
    private ParticleSystem FireFX;
    private int previousLayer;
    [SerializeField] private LayerMask targetLayer;

    /// <summary>
    /// çàæèãàëêà íå ææåò!!¨!
    /// </summary>
    private void Start()
    {
        FireFX = GetComponentInChildren<ParticleSystem>();
        //fireSpreading = GetComponentInChildren<FireSpreading>();
        //fireSpreading.enabled = false;
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
        if (grabAction.state)
        {
            if (!FireFX.isPlaying)
            {
                FireFX.Play();
                fireSpreading = gameObject.AddComponent<FireSpreading>();
                fireSpreading.flamableCoeff = 3f;
                //fireSpreading.enabled = true;
            }
        }
        else
        {
            if (FireFX.isPlaying)
            {
                FireFX.Stop();
                Destroy( fireSpreading );
                //fireSpreading.enabled = false;
            }
            
        }
    }
}
