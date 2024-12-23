using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Скрипт универсальный, для любого предмета, способного нанести урон врагам. Считывает ускорение коллайдера, 
/// и пропорцианально нему пркладывает урон к врагу!
/// </summary>
public class DamageMaker : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float damageMuliplyer = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
        {
            
            float damage = rb.velocity.magnitude * damageMuliplyer;
            print($"Ускорение RB составило : {rb.velocity.magnitude}");
            print($"Урон врагу составил : {damage}");
            enemyHealth.TakeDamage((int)damage);
        }
    }
}
