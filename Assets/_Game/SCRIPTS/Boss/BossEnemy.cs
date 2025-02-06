using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;
using Zenject;

public class BossEnemy :MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] Transform testTargetTransform;
    private Transform currentTargetTransform;
    private PlayerHealth playerHealth;

    private Player _player;
    private Animator animator;
    private NavMeshAgent agent;
    public bool isAlive = true;
    public bool isMovable = false;


    public float remDist;
    public EnemyBehState currentState;

    [SerializeField] private EnemyBehState initialState = EnemyBehState.Wandering;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;

    

    private Transform currentDestination;
    private EnemySight stateHandler;

    [SerializeField] private GameObject enemyModelPrefab;
    [SerializeField] private GameObject enemyRagdollPrefab;
    private EnemyAttack[] enemyAttacksArray;
    private GameObject currentModel;
    private EnemyRgdll currentRgdll;
    private Rigidbody[] rigidBodiesRagdoll;


    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }


    private void Awake()
    {
        currentModel = Instantiate<GameObject>(enemyModelPrefab, transform);
        rigidBodiesRagdoll = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidBodiesRagdoll)
        {
            rb.isKinematic = true;
        }
        playerTransform = _player.transform;
        playerHealth = _player.GetComponent<PlayerHealth>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        currentDestination = endTransform;
        stateHandler = GetComponent<EnemySight>();
        currentState = initialState;
        enemyAttacksArray = GetComponentsInChildren<EnemyAttack>();
        //TurnAttackComponentsOff();
        //currentTargetTransform = defaulTargetTransform;
        currentTargetTransform = testTargetTransform != null ? testTargetTransform : playerTransform;

    }

    private void Start()
    {
        currentRgdll = GetComponentInChildren<EnemyRgdll>();
        Debug.Log($"Ragdoll found = {currentRgdll != null}");

        Debug.Log($"enemyAttacksArray Length {enemyAttacksArray.Length}");
    }


    public void SetState(EnemyBehState stateToSet)
    {
        if (currentState != stateToSet)
        {
            currentState = stateToSet;
        }
    }

    private void Update()
    {
        //if (isAlive)
        //{
        //    switch (currentState)
        //    {
        //        case EnemyBehState.Idle:
        //            {
        //                TurnAttackComponentsOff();
        //                ClearAnimationState();
        //                animator.SetTrigger("Idle");
        //                break;
        //            }
        //        case EnemyBehState.Wandering:
        //            {
        //                TurnAttackComponentsOff();
        //                agent.isStopped = false;
        //                animator.SetBool("Walk", true);
        //                agent.destination = currentDestination.position;
        //                stateHandler.StartCheck();

        //                remDist = agent.remainingDistance;

        //                if (agent.remainingDistance != 0 & agent.remainingDistance <= 0.8f & agent.remainingDistance != float.PositiveInfinity & agent.remainingDistance != float.NegativeInfinity)
        //                {
        //                    currentDestination = currentDestination == endTransform ? startTransform : endTransform;
        //                    agent.destination = currentDestination.position;
        //                }
        //                break;
        //            }
        //        case EnemyBehState.Steering:
        //            {
        //                animator.SetTrigger("Walk");
        //                agent.isStopped = false;
        //                TurnAttackComponentsOff();
        //                agent.destination = currentTargetTransform.position;
        //                //if (agent.remainingDistance <= agent.stoppingDistance  & playerHealth.isAlive)
        //                //{
        //                //    currentState = EnemyBehState.Attack;
        //                //}
        //                break;
        //            }
        //        case EnemyBehState.Attack:
        //            {
        //                agent.isStopped = true;
        //                transform.rotation = Quaternion.LookRotation(new Vector3(currentTargetTransform.position.x, transform.position.y, currentTargetTransform.position.z) - transform.position, Vector3.up);
        //                TurnAttackComponentsOn();
        //                animator.SetTrigger("Attack");
        //                if (agent.remainingDistance > agent.stoppingDistance + 0.02f)
        //                {
        //                    if (playerHealth.isAlive)
        //                    {
        //                        currentState = EnemyBehState.Steering;
        //                    }
        //                    else currentState = EnemyBehState.Wandering;
        //                }
        //                break;
        //            }

        //        case EnemyBehState.Death:
        //            {
        //                TurnAttackComponentsOff();
        //                ClearAnimationState();
        //                Death();
        //                break;
        //            }
        //        default: break;
        //    }
        //}
        //else
        //{
        //    agent.enabled = false;
        //}
    }

    //private void ClearAnimationState()
    //{
    //    animator.SetBool("Walk", false);
    //}

    //private void TurnAttackComponentsOn()
    //{
    //    for (int i = 0; i < enemyAttacksArray.Length; i++)
    //    {
    //        enemyAttacksArray[i].enabled = true;
    //    }

    //    Debug.Log($"TurnOn attack on length {enemyAttacksArray.Length}");
    //}

    //private void TurnAttackComponentsOff()
    //{
    //    for (int i = 0; i < enemyAttacksArray.Length; i++)
    //    {
    //        enemyAttacksArray[i].enabled = false;
    //    }
    //}

    //private void Death()
    //{
    //    foreach (var rb in rigidBodiesRagdoll)
    //    {
    //        rb.isKinematic = false;
    //    }
    //    animator.enabled = false;
    //    currentRgdll.StopConvulsing();
    //    isAlive = false;
    //}
}
