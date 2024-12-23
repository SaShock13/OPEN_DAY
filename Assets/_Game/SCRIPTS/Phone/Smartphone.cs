using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Smartphone : MonoBehaviour
{
    public bool call = false;
    private bool isCalling = false;
    private bool isTalking = false;
    private AudioSource phoneSound;
    [SerializeField] private AudioClip ringSound;
    [SerializeField] private AudioClip talkSound;
    [SerializeField] private GameObject inCallPanel;
    [SerializeField] private GameObject callPanel;

    public PhoneStateMachineMono machineMono;
    

    private void Start()
    {
        phoneSound = GetComponent<AudioSource>();
        machineMono = GetComponent<PhoneStateMachineMono>();
    }

    public void Call()
    {
        Debug.Log("Someone is Calling");
        inCallPanel.SetActive(true);
        Debug.Log($"inCallPanel {inCallPanel != null}");
        Debug.Log($"phonesound {phoneSound!=null}");
        Debug.Log($"ringSound {ringSound != null}");
        phoneSound.clip = ringSound;
        if (!phoneSound.isPlaying)
        {
            phoneSound.Play();
        }
    }
        
    public void StoptCall()
    {
        inCallPanel.SetActive(false);
        if (phoneSound.isPlaying)
        {
            phoneSound.Stop();
        }
    }

    public void AnswerCall()
    {        
        Debug.Log("Answer Call");
        callPanel.SetActive(true);
        phoneSound.clip = talkSound;
        phoneSound.Play();
    }

    public void StopTalk()
    {
        callPanel.SetActive(false);
        if(phoneSound.isPlaying)phoneSound.Stop();
    }
}
