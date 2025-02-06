using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimAttack : MonoBehaviour
{
    [SerializeField] public GameObject simpleAttackTrigger;
    [SerializeField] float simpleAttackDamage;


    private void Start()
    {
        simpleAttackTrigger.SetActive(false);
        simpleAttackTrigger.GetComponent<EnemyAttack>().damage = simpleAttackDamage;
        
    }
    public void SimpleAttackStart()
    {
        simpleAttackTrigger.SetActive(true);
    }
    public void SimpleAttackStop()
    {
        simpleAttackTrigger.SetActive(false);
    }
}
