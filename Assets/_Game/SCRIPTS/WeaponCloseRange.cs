using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCloseRange : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    [SerializeField] private int damage = 30;
    public bool isAttacking = false;
    private float attackPause = 1;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Some collision of weapon {this}");
        if (!isAttacking)
        {
            if (collision.gameObject.TryGetComponent<EnemyHealth>(out enemyHealth))
            {
                isAttacking = true;
                StartCoroutine(AttackPause());
                Debug.Log($"EnemyHealth collision of weapon {this}");
                enemyHealth.TakeDamage(damage);
            }
            else
            {
                enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                    isAttacking = true;
                    StartCoroutine(AttackPause());
                }
            } 
        }
    }

    IEnumerator AttackPause()
    {
        yield return new WaitForSeconds(attackPause);
        isAttacking = false;
    }
}
