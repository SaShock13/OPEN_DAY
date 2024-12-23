using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCloseRange : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(30);
        }
    }
}
