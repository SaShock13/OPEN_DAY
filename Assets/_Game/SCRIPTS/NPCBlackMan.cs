using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NPCBlackMan : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] AudioClip talkClip;
    [SerializeField] AudioClip scaredClip;
    private bool isTalking = false;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = scaredClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartTalkCoroutine()); 
        }
    }

    IEnumerator StartTalkCoroutine()
    {
        if (talkClip!=null && !isTalking)
        {
            audioSource.clip = talkClip;
            audioSource.Play(); 
        }
        isTalking = true;
        animator.SetBool("Talk",true);
        Debug.Log($"Nigga Talking Shit {this}");
        float talkLenght = talkClip != null ? talkClip.length : 5f;
        yield return new WaitForSeconds(talkLenght);
        isTalking=false;
        animator.SetBool("Talk", false);
        audioSource.clip= scaredClip;
        audioSource.Play();
    }
}
