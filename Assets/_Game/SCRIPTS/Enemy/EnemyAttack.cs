using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 50;
    private bool isAttacking = false;
    private float attackDelay = 1f;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage, this.gameObject);
            }
        }         
    }

    IEnumerator AttackDelay()
    {
        Debug.Log($"Attack Delay {attackDelay}");
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false ;
    }

    private void OnDisable()
    {
        isAttacking = false ;
    }
}
