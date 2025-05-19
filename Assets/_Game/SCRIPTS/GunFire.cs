using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GunFire : MonoBehaviour
{
    private Interactable interactable;
    private SteamVR_Skeleton_Poser poser;
    private Animator animator;

    [SerializeField] private Transform shotOrigin;
    [SerializeField] private Transform shotAim;
    [SerializeField] AudioSource shotSound;
    [SerializeField] GameObject hitEffect;
    [SerializeField] private LayerMask hitLayerMask;
    [SerializeField] private int damage = 250;
    private ParticleSystem shotFx;

    [SerializeField] SteamVR_Action_Boolean fireAction;
    //[SerializeField] PostProcessVolume postProcessVolume;
    //[SerializeField] PostProcessProfile postProcessprofile1;
    //[SerializeField] PostProcessProfile postProcessprofile2;


    private void Start()
    {
        interactable = GetComponent<Interactable>();
        poser = GetComponentInChildren<SteamVR_Skeleton_Poser>();
        animator = GetComponent<Animator>();
        shotFx = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (interactable.attachedToHand!=null)
        {
            var hand = interactable.attachedToHand.handType;
            if (fireAction[hand].stateDown)
            {
                Shot();
            }
        }
    }

    private void Shot()
    {
        shotSound?.Play();
        animator.SetTrigger("Shot");
        shotFx.Play();
        if (Physics.Raycast(shotOrigin.position, shotAim.position - shotOrigin.position, out RaycastHit hit, 100f))
        {
            
            var bulletHit = Instantiate(hitEffect, hit.point, Quaternion.identity);
            StartCoroutine(DestroyHit(bulletHit));
            Debug.Log(hit.transform.name);
            if (hit.transform.name == "HeadCollider")
            {
                StartCoroutine(nameof(DeathBloom));
            }

            if (hit.transform.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth health) || hit.collider.CompareTag("Boss"))
            {
                if (hit.collider.CompareTag("Head"))
                {
                    health.TakeDamage(damage * 2);
                    print("Headshot");
                }
                else health.TakeDamage(damage);
            }
        }
    }

    IEnumerator DestroyHit(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
    }

   
    IEnumerator DeathBloom()
    {
        //postProcessVolume.enabled = true;
        //postProcessVolume.profile = postProcessprofile2;
        yield return new WaitForSeconds(5);
        //postProcessVolume.enabled = false;

    }
}
