using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField, Range(10,2000)] private int health = 20;
    private Animator animator;
    private Enemy enemy;
    private Rigidbody rb;
    private Collider bodyCollider;
    [SerializeField] GameObject bloodSprite;
    private GameObject[] bloodSpritesPool = new GameObject[5];
    int bloodSpritesCounter = 0;


    private void Start()
    {
        Debug.Log("EnemyHealth Started");
        for (int i = 0; i < 5; i++)
        {
            bloodSpritesPool[i] = Instantiate<GameObject>(bloodSprite);///НА каждом по5 создает, но выкидывает по 2 сразу
            bloodSpritesPool[i].SetActive(false);
        }
        animator = GetComponentInChildren<Animator>();
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody>();
        bodyCollider = GetComponent<Collider>();
    }

    public void TakeDamage(int damage)
    {
        if (enemy.isAlive)
        {

            if (damage > 0)
            {

                bloodSpritesPool[bloodSpritesCounter].transform.position = 
                    new Vector3(
                        transform.position.x, 
                        bloodSpritesPool[bloodSpritesCounter].transform.position.y, 
                        transform.position.z);
                bloodSpritesPool[bloodSpritesCounter].SetActive(true);
                bloodSpritesCounter++;
                bloodSpritesCounter = bloodSpritesCounter >= 5 ? 0 : bloodSpritesCounter;
                health -= damage;
                print($"Health : {health}");
                if (health <= 0)
                {
                    Death();
                    print($"Enemy is Dead");
                }
                else animator.SetTrigger("Hit");
            }
            
        }
    }

    private void Death()
    {
        //НЕ УМИРАЕТ ИЗ СОСТОЯНИЯ АТАКИ!!!
        //animator.SetTrigger("Death");

        enemy.SetState(EnemyBehState.Death);
        //rb.isKinematic = true;
        //bodyCollider.enabled = false;
    }


    /// <summary>
    /// Чисто для тестирования
    /// </summary>
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Death();
        }
    }

}
