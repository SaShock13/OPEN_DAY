using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Chainsaw : MonoBehaviour
{
    Animator animator;
    AudioSource sawSoundSource;
    [SerializeField] SteamVR_Action_Vibration vibrationAction;

    private void Start()
    {
        sawSoundSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Проверить вибрацию на контроллере.
    /// </summary>
    public void OnPickUp()
    {
        vibrationAction.Execute(0,2,150,75,SteamVR_Input_Sources.RightHand);
        animator.SetBool("Working", true);
        sawSoundSource.Play();

    }
    public void OnDetach()
    {
        sawSoundSource.Stop();
        animator.SetBool("Working", false);
    }

}
