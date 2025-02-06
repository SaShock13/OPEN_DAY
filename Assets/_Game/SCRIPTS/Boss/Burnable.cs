using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Burnable : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float burnInSeconds = 10f;
    private EnemyHealth _enemyHealth;
    private BossSMMono stateMachine;
    

    private void Start()
    {
        _enemyHealth = GetComponentInParent<EnemyHealth>();
        stateMachine = GetComponentInParent<BossSMMono>();
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            BurnTheBoss();
        }
    }
    public void BurnTheBoss()
    {
        StartCoroutine(BurnCoroutine());
    }

    IEnumerator BurnCoroutine()
    {
        _particleSystem.Play();
        yield return new WaitForSeconds(burnInSeconds/2);
        //stateMachine.SetState<BossDeathState>();
        _enemyHealth.Death();
        yield return new WaitForSeconds(burnInSeconds/2);
        _particleSystem.Stop();
    }
    
    
}
