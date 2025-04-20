using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Valve.VR.InteractionSystem;
using Zenject;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float fullHealingTime = 100;
    [SerializeField] private float maxHealth = 1000;
    [SerializeField] private AudioClip heartedSound;
    [SerializeField] private AudioClip deathSound;

    private Transform afterDeathRespawnTransform;

    public bool isAlive = true;

    public UnityEvent<float,float> onHealthChanged;
    public UnityEvent onDeath;
    public UnityEvent onRebirth;


    public float health ;
    private bool isHealing = false;
    private float healingSpeed ;
    private Volume _volume;
    private Bloom _bloom;
    private Player _player;
    private CharacterController _characterController;
    private AudioSource _audioSource;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        afterDeathRespawnTransform = GameObject.Find("PLayerRespawn").transform;
        _volume = FindObjectOfType<Volume>();
        health = maxHealth;
        healingSpeed = maxHealth/ fullHealingTime;
        _volume?.profile.TryGet(out _bloom);
        _characterController = GetComponent<CharacterController>();
        _audioSource = GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        onHealthChanged?.Invoke(health, maxHealth);
        
    }


    private void Update()
    {
        TestingDamage();
        if(health<maxHealth&health>0)
        {
            AutoHealing();
        }
    }

    private void AutoHealing()
    {
        health += 1*Time.deltaTime*healingSpeed;
        onHealthChanged?.Invoke(health,maxHealth);
    }

    private void TestingDamage()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(100,this.gameObject);
        }
    }

    public void TakeDamage(float damage, GameObject damager)
    {
        if (isAlive)
        {
            if (damage > 0)
            {
                health -= damage;
                onHealthChanged?.Invoke(health,maxHealth);
                StartCoroutine(Damage());
                if (health <= 0)
                {
                    Death();
                }
            }
        }
    }

    private void Death()
    {
        isAlive = false;
        _audioSource.clip = deathSound;
        _audioSource.Play();
        _characterController.enabled = false;
        StartCoroutine(Respawn());
        onDeath?.Invoke();
    }

    private IEnumerator Damage()
    {
        _bloom.active = true;
        _audioSource.clip = heartedSound;
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play(); 
        }
        yield return new WaitForSeconds(0.5f);
        _bloom.active = false;
    }

    IEnumerator Respawn()
    {
        _player.transform.position = afterDeathRespawnTransform.position;
        yield return new WaitForSeconds(2);        
        health = maxHealth;
        onRebirth?.Invoke();
        onHealthChanged?.Invoke(health,maxHealth);
        _characterController.enabled = true;
        isAlive = true;
    }
}
