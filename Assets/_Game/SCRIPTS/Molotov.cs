using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    private Explosive explosive;
    public bool isActive = false;
    [SerializeField] private float selfExplodeTime = 5;
    [SerializeField] private ParticleSystem flamedFX;
    [SerializeField] private float explosionDelay = 1;

    private void Start()
    {
        explosive = GetComponent<Explosive>();

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) ) // FOR TEST
        { 
            SetActive(); 
        }
    }

    public void SetActive()
    {
        isActive = true;
        flamedFX.Play();
        StartCoroutine(SelfExplodeCountdownCoroutine());

    }

    public void ThrowMolotov()
    {
        if (isActive)
        {
            explosive.ActivateGrenade(explosionDelay);
        }
    }

    IEnumerator SelfExplodeCountdownCoroutine()
    {
        yield return new WaitForSeconds(selfExplodeTime);
        ThrowMolotov();
        yield return new WaitForSeconds(explosionDelay);
        flamedFX.Stop();
    }
}
