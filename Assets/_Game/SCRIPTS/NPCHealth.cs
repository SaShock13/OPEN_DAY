using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    [SerializeField] private AudioClip heartSoundClip;
    private AudioSource audioSource;
    private Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage()
    {

        Debug.Log($"NPC Hearted!!! {this}");
        animator.SetTrigger("Hit");
        audioSource.clip = heartSoundClip;
        audioSource.Play();
    }
}
