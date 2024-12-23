using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PlayerHealth : MonoBehaviour
{

    private float health ;
    private bool isAlive = true;
    public UnityEvent<float,float> onHealthChanged;
    private Volume _volume;
    private Bloom _bloom;
    private bool isHealing = false;
    //[SerializeField] GameObject damageEffect;
    [SerializeField] private float maxHealth = 1000;
    private float healingSpeed ;
    [SerializeField] float fullHealingTime = 100;

    private void Awake()
    {
        _volume = FindObjectOfType<Volume>();
        health = maxHealth;
        healingSpeed = maxHealth/ fullHealingTime;
        _volume.profile.TryGet(out _bloom);
        
    }
    private void OnEnable()
    {
        onHealthChanged?.Invoke(health, maxHealth);
    }

    
    private void Update()
    {
        TestingDamage();
        if(health<maxHealth)
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
    }

    private IEnumerator Damage()
    {
        _bloom.active = true;
       // damageEffect.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        //damageEffect.SetActive(false);
        _bloom.active = false;

    }
}
