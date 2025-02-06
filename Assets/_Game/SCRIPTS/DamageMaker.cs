using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Ñêðèïò óíèâåðñàëüíûé, äëÿ ëþáîãî ïðåäìåòà, ñïîñîáíîãî íàíåñòè óðîí âðàãàì. Ñ÷èòûâàåò óñêîðåíèå êîëëàéäåðà, 
/// è ïðîïîðöèàíàëüíî íåìó ïðêëàäûâàåò óðîí ê âðàãó!
/// </summary>
public class DamageMaker : MonoBehaviour
{
    private Rigidbody rigidBody;
    [SerializeField] float damageMuliplyer = 2;
    private bool isInHand = false;
    private bool stillCanDamage = false;
    [Range(0f, 2f)]
    public float minDamageVelocity = 0.2f;
    private LayerMask layer;
    private float damageLongerAmount = 1.5f; // время после броска, которое предмет может дамажить

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (isInHand || (stillCanDamage && rigidBody.velocity.magnitude > minDamageVelocity ))
        {
            if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {

                float damage = rigidBody.velocity.magnitude * damageMuliplyer;
                print($"Ускорение damageMaker : {rigidBody.velocity.magnitude}");
                print($"Урон  damageMaker : {damage}");
                enemyHealth.TakeDamage((int)damage);
            } 
        }
    }

    public void setInHand()
    {
        isInHand = true;
        
        gameObject.layer = LayerMask.NameToLayer("Weapons");
    }
    public void setNotInHand()
    {
        isInHand = false;
        stillCanDamage = true;
        StartCoroutine(DamageLongerCoroutine());
        gameObject.layer = LayerMask.NameToLayer("Environment");
    }

    IEnumerator DamageLongerCoroutine() // продление дамага предметом после броска
    { 
        yield return new WaitForSeconds(damageLongerAmount);
        stillCanDamage = false;
    }

}
