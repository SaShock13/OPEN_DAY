using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class EnemyStateHandler : MonoBehaviour
{
    [SerializeField] float sightDistance = 5f;
    [SerializeField] float attackDistance = 0.5f;

    [SerializeField] float checkSightInterval = 0.5f;
    public UnityEvent<EnemyBehState>  onPlayerInSight;
    public UnityEvent<EnemyBehState> onPlayerInAttackDistance;
    //public UnityEvent onPlayerOutAttackDistance;
    //public UnityEvent onPlayerOutSight;
    private Player player;
    [HideInInspector]public bool isNeedToCheck = false;
    public bool isPlayerInView = false;
    private Coroutine checkCoroutine;
    public float distanceToPlayer;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void StartCheck()
    {
        if (checkCoroutine==null)
        {
            checkCoroutine = StartCoroutine(CheckSight()); 
        }
    }

    public void StopCheck()
    {
        if (checkCoroutine != null)
        {
            StopCoroutine(checkCoroutine);
        }
    }

    IEnumerator CheckSight()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkSightInterval);
            distanceToPlayer = (player.transform.position - transform.position).magnitude;
            if (distanceToPlayer <= attackDistance)
            {
                onPlayerInAttackDistance.Invoke(EnemyBehState.Attack);
            }
            else
            {
                //onPlayerOutAttackDistance.Invoke();
                if (distanceToPlayer <= sightDistance)
                {
                    isPlayerInView = true;

                    onPlayerInSight.Invoke(EnemyBehState.Steering);
                }
                else
                {
                    isPlayerInView = false;
                    ///onPlayerOutSight.Invoke();
                }
            }


        } 
    }
}
