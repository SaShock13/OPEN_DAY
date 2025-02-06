using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField, Range(10,20000)] private int health = 20;
    [SerializeField] float damageModifier = 1;
    private int maximumBloodSpritesCount = 10;
    private Animator animator;
    private Enemy enemy;
    private BossEnemy bossEnemy;
    private Rigidbody rb;
    private Collider bodyCollider;
    [SerializeField] GameObject bloodSprite;
    private GameObject[] bloodSpritesPool;
    int bloodSpritesCounter = 0;
    private BossSMMono stateMachine;
    private ParticleSystem _fireFx;

    private AudioSource _sound;
    [SerializeField] private UnityEvent deathEvent;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;





    private void Start()
    {
        _fireFx = GetComponentInChildren<ParticleSystem>();
        stateMachine = GetComponent<BossSMMono>();
        Debug.Log("EnemyHealth Started");
        bloodSpritesPool = new GameObject[maximumBloodSpritesCount];
        for (int i = 0; i < maximumBloodSpritesCount; i++)
        {
            bloodSpritesPool[i] = Instantiate<GameObject>(bloodSprite); 
            bloodSpritesPool[i].SetActive(false);
        }
        animator = GetComponentInChildren<Animator>();
        enemy = GetComponent<Enemy>();
        bossEnemy = GetComponent<BossEnemy>(); 
        rb = GetComponent<Rigidbody>();
        bodyCollider = GetComponent<Collider>();
        _sound = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if ((enemy != null && enemy.isAlive) || ((bossEnemy != null) && bossEnemy.isAlive) )
        {

            Debug.Log($"TRUE (enemy != null && enemy.isAlive) || ((bossEnemy != null) && bossEnemy.isAlive) {this}");
            if (damage > 0)
            {
                MakeBlood();
                health -= (int)(damage * damageModifier);

                _sound.clip = hitSound;
                if (!_sound.isPlaying)
                {
                    _sound.Play();
                }

                print($"Health : {health}");
                if (health <= 0)
                {
                    Death();
                    print($"Enemy is Dead");
                }
                else animator.SetTrigger("Hit");
            }

        }
        else
        {

            Debug.Log($"NOT TRUE : ({enemy != null} ");
            Debug.Log($"NOT TRUE : ({enemy.isAlive})");
            Debug.Log($"NOT TRUE : {bossEnemy != null})");
            Debug.Log($"NOT TRUE : {bossEnemy.isAlive}");
        }
    }

    private void MakeBlood()
    {
        bloodSpritesPool[bloodSpritesCounter].transform.position =
                            new Vector3(
                                transform.position.x,
                                bloodSpritesPool[bloodSpritesCounter].transform.position.y,
                                transform.position.z);
        bloodSpritesPool[bloodSpritesCounter].SetActive(true);
        bloodSpritesCounter++;
        bloodSpritesCounter = bloodSpritesCounter >= maximumBloodSpritesCount ? 0 : bloodSpritesCounter;
    }

    public void Death()
    {
        //Õ≈ ”Ã»–¿≈“ »« —Œ—“ŒﬂÕ»ﬂ ¿“¿ »!!!
        deathEvent?.Invoke();
        _sound.clip = deathSound;
        _sound.Play();
        if (stateMachine!=null)
        {
            stateMachine.SetState<BossDeathState>(); 
        }

        enemy.SetState(EnemyBehState.Death);
        rb.isKinematic = true;
        bodyCollider.enabled = false;
    }




    /// <summary>
    /// ◊ËÒÚÓ ‰Îˇ ÚÂÒÚËÓ‚‡ÌËˇ
    /// </summary>
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Death();
        }
    }

}
