using UnityEngine;
using Valve.VR;

public class Lighter : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean grabAction;
    [SerializeField] private Collider fireTrigger;
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
        fireTrigger.enabled = false;
    }

    public void OnTake()
    {
        previousLayer = gameObject.layer;
        gameObject.layer = 7;
    }

    public void OnDrop()
    {
        gameObject.layer = previousLayer;
        TurnFireOff();
    }

    public void OnLighterHeld()
    {
        if (grabAction.state)
        {
            if (!FireFX.isPlaying)
            {
                LightTheLighter();
            }
        }
        else
        {
            if (FireFX.isPlaying)
            {
                TurnFireOff();
            }
        }
    }

    private void TurnFireOff()
    {
        FireFX.Stop();
        Destroy(fireSpreading);
    }
    
    public void LightTheLighter()
    {
        FireFX.Play();
        fireSpreading = gameObject.AddComponent<FireSpreading>();
        fireTrigger.enabled = true;
        fireSpreading.flamableCoeff = 6f;
    }

}
