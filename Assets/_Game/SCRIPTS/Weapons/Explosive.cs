using System.Collections;
using System.Collections.Generic;
using Assets._Game.SCRIPTS;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private bool showGizmos = false;
    [SerializeField] private ParticleSystem explosionFX;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private float radius = 10f;
    [SerializeField] private float force = 1000f;
    [SerializeField] private float upForce = 10;
    [SerializeField] bool isThisBurning = false;
    const float explosDelayDefault = 3f;
    private AudioSource explosionSound;
    private Collider[] collidersToExplode;
    private MeshRenderer meshRenderer;
    private EnemyHealth enemyHealth;
    private NPCHealth npcHealth;
    private Burnable bossBurnable;
    public bool isExploding = false;


    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        explosionSound = gameObject.AddComponent<AudioSource>();
        explosionSound.clip = explosionClip;
    }

    private void OnDrawGizmosSelected()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, radius); 
        }
    }

    

    void Update()
    {
        if (isExploding)
        {
            collidersToExplode = Physics.OverlapSphere(transform.position, radius);
            if (explosionFX)
            {
                explosionFX.transform.parent = null;
                explosionFX.transform.up = Vector3.up;
                explosionFX.Play();
                Destroy(explosionFX.gameObject,1f);
            }
            if (explosionSound)
            {
                explosionSound.Play();
            }

            meshRenderer.enabled = false;
            if (collidersToExplode != null)
            {
                foreach (Collider col in collidersToExplode)
                {
                    if(col.attachedRigidbody!=null)
                    {
                        if (isThisBurning )
                        {
                            Flamable flamable;
                            if (col.TryGetComponent<Flamable>(out flamable)) //todo мгновеннное возгарание сделать
                            {
                                flamable.Heating(20000);
                            }
                            else
                            {
                                bossBurnable = col.GetComponentInChildren<Burnable>();
                                if (bossBurnable != null)
                                {
                                    bossBurnable.BurnTheBoss();
                                }
                            }

                            
                        } else
                        if (col.TryGetComponent<EnemyHealth>(out enemyHealth))
                        {
                            enemyHealth.TakeDamage(1000);
                        }
                        else 
                        if (col.TryGetComponent<NPCHealth>(out npcHealth))
                        {
                            npcHealth.TakeDamage();
                        }


                        StartCoroutine(ExplodeRagdoll(col));
                    }
                }
            }
            else Debug.Log("Nothing to explode");
            isExploding = false;
            Destroy(gameObject, 1f);
        }
    }

    public void ActivateGrenade(float explosionDelay = explosDelayDefault)
    {
        StartCoroutine(ActivateGrenadeCoroutine(explosionDelay));
    }

    IEnumerator ExplodeRagdoll(Collider col)
    {
        
        yield return null;
        yield return null;
        //enemyHealth.GetComponent<Enemy>().AddExplosionForce(force, transform.position, radius);
        col.attachedRigidbody.AddExplosionForce(force, transform.position, radius, upForce);
    }

    IEnumerator ActivateGrenadeCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        isExploding = true;
    }
}
