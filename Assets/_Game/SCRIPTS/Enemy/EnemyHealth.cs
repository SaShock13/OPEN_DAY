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
    private BossSMMono bossStateMachine;
    private EnemySM enemySM;
    private ParticleSystem _fireFx;

    private AudioSource _sound;

    public event Action OnPlayerDeathEvent;
    [SerializeField] private UnityEvent deathEvent;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;





    private void Start()
    {
        _fireFx = GetComponentInChildren<ParticleSystem>();
        bossStateMachine = GetComponent<BossSMMono>();
        enemySM = GetComponent<EnemySM>();
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
        if ((enemy != null && enemy.IsAlive) || ((bossEnemy != null) && bossEnemy.isAlive) )
        {
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
                }
                else
                {
                    animator.SetTrigger("Hit");
                    if (enemySM == null) bossStateMachine.SetState<BossWanderingState>();
                    else enemySM.SetState<EnemyWanderingState>();
                }
            }

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
        deathEvent.Invoke();
        OnPlayerDeathEvent?.Invoke();
        _sound.clip = deathSound;
        _sound.Play();
        if (bossStateMachine!=null)
        {
            bossStateMachine.SetState<BossDeathState>(); 
        }

        //enemy.SetState(EnemyBehState.Death);
        rb.isKinematic = true;
        bodyCollider.enabled = false;
    }




    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Death();
        }
    }

}
