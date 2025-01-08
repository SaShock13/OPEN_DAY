using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Door : MonoBehaviour
{
    private SphereCollider doorCollider;
    [SerializeField] private bool codeLocked = false; // для полного блокирования/разблокирования двери кодом или картой и т.д.
    private CircularDrive circularDrive;
    [SerializeField] private float doorMaxAngle = 165;
    private float doorRotationPlay = 0.5f;
    private AudioSource lockSound;
    [SerializeField] AudioClip unlockAudioClip;
    [SerializeField] AudioClip lockedAudioClip;


    private void Start()
    {
        doorCollider = GetComponent<SphereCollider>();
        circularDrive = GetComponent<CircularDrive>();
        lockSound = GetComponent<AudioSource>();
    }

    //private void Update()
    //{
    //    
    //}

    public void UnlockDoor()
    {
        if (codeLocked)
        {
            codeLocked = false;
            circularDrive.maxAngle = doorMaxAngle;
            lockSound.clip = unlockAudioClip;
            lockSound.Play();
        }
    }

    public void LockDoor()
    {
            codeLocked = true;
            circularDrive.maxAngle = circularDrive.minAngle + doorRotationPlay;
            Debug.Log($"Мин угол {circularDrive.minAngle}");
            Debug.Log($"Макс угол {circularDrive.maxAngle}");
    }

    public void OnMAxAngle()
    {
        if(codeLocked)
        {
            lockSound.clip = lockedAudioClip;
            lockSound.Play();
        }
    }
}
