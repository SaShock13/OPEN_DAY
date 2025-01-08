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

    private float health ;
    public bool isAlive = true;
    public UnityEvent<float,float> onHealthChanged;
    private Volume _volume;
    private Bloom _bloom;
    private bool isHealing = false;
    //[SerializeField] GameObject damageEffect;
    [SerializeField] private float maxHealth = 1000;
    private float healingSpeed ;
    [SerializeField] float fullHealingTime = 100;
    [SerializeField] private Transform respawnTransform;
    private Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _volume = FindObjectOfType<Volume>();
        health = maxHealth;
        healingSpeed = maxHealth/ fullHealingTime;
        _volume.profile.TryGet(out _bloom);
        //player = GetComponent<Player>();
        
    }
    private void OnEnable()
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

    /// <summary>
    /// Для тестирования урона!!!
    /// </summary>
    private void TestingDamage()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(100);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isAlive)
        {
            if (damage > 0)
            {
                health -= damage;
                onHealthChanged?.Invoke(health,maxHealth);
                Debug.Log($"Health : {health}");
                print(health);
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
        print("PLayer is dead!!");
        isAlive = false;
        _player.transform.position = respawnTransform.position;
        StartCoroutine(Respawn());
    }

    private IEnumerator Damage()
    {
        _bloom.active = true;
       // damageEffect.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        //damageEffect.SetActive(false);
        _bloom.active = false;

    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        
        health = maxHealth;
        isAlive = true;
    }
}
