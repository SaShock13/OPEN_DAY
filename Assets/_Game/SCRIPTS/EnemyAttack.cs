using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] Collider handCollider;


    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
            if (playerHealth!=null)
            {                
                playerHealth.TakeDamage(50);
            }
        }
    }
}
