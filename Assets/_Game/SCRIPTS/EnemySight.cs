using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;
using Zenject;

public class EnemySight : MonoBehaviour
{
    [SerializeField] float sightDistance = 10f;
    [SerializeField] float attackDistance = 2.5f;

    [SerializeField] float checkSightInterval = 0.5f;
    [SerializeField] private Transform testTarget;
    [SerializeField] private Transform currentTarget;

    public UnityEvent  onPlayerInSight;
    public UnityEvent onPlayerInAttackDistance;
    public UnityEvent onPlayerOutOFSight;
    public UnityEvent onPlayerOutOfAttackDistance;
    //public UnityEvent onPlayerOutAttackDistance;
    //public UnityEvent onPlayerOutSight;
    private Player _player;

    [HideInInspector]public bool isNeedToCheck = false;
    public bool isPlayerInView = false;
    public bool isPlayerInAttackDistance = false;
    private Coroutine checkCoroutine;
    public float distanceToTarget;


    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }


    private void Start()
    {
        currentTarget = testTarget != null ? testTarget : _player.transform;
        //currentTarget = testTarget ;
        StartCheck();
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
            //distanceToTarget = (Vector3.ProjectOnPlane(currentTarget.position - transform.position, Vector3.up)).magnitude  ;
            distanceToTarget = (currentTarget.position - transform.position).magnitude;

            if (distanceToTarget <= attackDistance && !isPlayerInAttackDistance)
            {
                isPlayerInAttackDistance=true;
                onPlayerInAttackDistance.Invoke();
            }
            if (distanceToTarget > attackDistance && isPlayerInAttackDistance)
            {
                isPlayerInAttackDistance = false;
                onPlayerOutOfAttackDistance.Invoke();
            }

            if (distanceToTarget <= sightDistance && !isPlayerInView)
            {
                isPlayerInView = true;
                onPlayerInSight.Invoke();
            }

            if (distanceToTarget > sightDistance && isPlayerInView)
            {
                isPlayerInView = false;
                onPlayerOutOFSight.Invoke();
            }


        } 
    }
}
