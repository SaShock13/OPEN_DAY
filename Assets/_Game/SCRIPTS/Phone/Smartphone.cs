using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Game.SCRIPTS;
using TMPro;
using UnityEngine;
using Zenject;

public class Smartphone : MonoBehaviour
{
    [SerializeField] private AudioClip ringSound;
    [SerializeField] private AudioClip talkSound;
    [SerializeField] private GameObject inCallPanel;
    [SerializeField] private GameObject callPanel;

    public bool call = false;
    public PhoneStateMachineMono machineMono;

    private bool isCalling = false;
    private bool isTalking = false;
    private AudioSource phoneSound;

    private void Start()
    {
        phoneSound = GetComponent<AudioSource>();
        machineMono = GetComponent<PhoneStateMachineMono>();
    }

    public void Call()
    {
        inCallPanel.SetActive(true);
        phoneSound.clip = ringSound;
        phoneSound.loop = true;
        phoneSound.Play();
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
        callPanel.SetActive(true);
        phoneSound.clip = talkSound;
        phoneSound.loop = false;
        phoneSound.Play();
        StartCoroutine(TalkCoroutine());
    }

    public void StopTalk()
    {
        callPanel.SetActive(false);
        if(phoneSound.isPlaying)phoneSound.Stop();
    }

    IEnumerator TalkCoroutine()
    {
        yield return new WaitForSeconds(talkSound.length);
        machineMono.SetState<IddleState>();
    }
}
