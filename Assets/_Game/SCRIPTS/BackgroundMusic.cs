using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip relaxMusic;
    [SerializeField] AudioClip thrillMusic;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = relaxMusic;
        audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeMusic();
        }
    }

    private void ChangeMusic()
    {
        audioSource.clip = audioSource.clip == relaxMusic ? thrillMusic: relaxMusic;
        audioSource.Play();
    }
}
