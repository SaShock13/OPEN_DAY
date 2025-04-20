using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;
using Zenject;

public class EnemySight : MonoBehaviour
{
    [SerializeField] private float sightDistance = 10f;
    [SerializeField] private float attackDistance = 2.5f;

    [SerializeField] private float checkSightInterval = 0.5f;
    [SerializeField] private Transform testTarget;
    private Transform currentTarget;

    private Player _player;

    [HideInInspector] public bool isNeedToCheck = false;
    public bool isPlayerInSteeringDistance = false;
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

            if (distanceToTarget <= attackDistance )
            {
                isPlayerInAttackDistance = true;
            }
            else isPlayerInAttackDistance = false;

            if (distanceToTarget > attackDistance & distanceToTarget < sightDistance)
            {
                isPlayerInSteeringDistance = true;
            }
            else isPlayerInSteeringDistance = false;
        }
    }
}
