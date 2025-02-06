using System.Collections;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(AudioSource))]

public class Lighter : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean grabAction;
    [SerializeField] private Collider fireTrigger;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private bool isEmpty = false;
    private AudioSource soundFX;
    private FireSpreading fireSpreading;
    private ParticleSystem FireFX;
    private int previousLayer;
    private bool isOnFire = false;
    [SerializeField] private LayerMask targetLayer;



    /// <summary>
    /// çàæèãàëêà íå ææåò!!¨!
    /// </summary>
    private void Start()
    {
        soundFX = GetComponent<AudioSource>();
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
        if (grabAction.stateDown)
        {
            soundFX.clip = clickSound;
            soundFX.loop = false;
            soundFX.Play();
        }
        if (grabAction.state)
        {
            if (!isOnFire)
            {
                LightTheLighter();
            }
        }
        else
        {
            if (isOnFire)
            {
                TurnFireOff();
            }
        }
    }

    private void TurnFireOff()
    {
        isOnFire = false;
        FireFX.Stop();
        soundFX.Stop();
        Destroy(fireSpreading);
    }
    
    public void LightTheLighter()
    {   
        isOnFire = true;
        Debug.Log($"ClickSound {this}");
        if (!isEmpty)
        {
            StartCoroutine(PlayFireSoundCoroutine(clickSound.length));            
            fireSpreading = gameObject.AddComponent<FireSpreading>();
            fireTrigger.enabled = true;
            fireSpreading.flamableCoeff = 6f; 
        }
    }

    IEnumerator PlayFireSoundCoroutine(float clickLenght)
    {       
        yield return new WaitForSeconds(clickLenght);
        soundFX.clip = fireSound;
        soundFX.loop = true;
        soundFX.Play();
        FireFX.Play();
    }

}
