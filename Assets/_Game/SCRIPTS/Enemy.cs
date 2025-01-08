using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;
using Zenject;

public enum EnemyBehState
{
    Idle,
    Steering,
    Attack,
    Wandering,
    Death
}

public class Enemy : MonoBehaviour
{
    private Transform playerTransform;
    private PlayerHealth playerHealth;

    [Inject]private Player player;
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
    private EnemyStateHandler stateHandler;

    [SerializeField] private GameObject enemyModelPrefab;
    [SerializeField] private GameObject enemyRagdollPrefab;
    private EnemyAttack[] enemyAttacksArray;
    private GameObject currentModel;
    private EnemyRgdll currentRgdll;
    private Rigidbody[] rigidBodiesRagdoll;


    private void Awake()
    {
        currentModel = Instantiate<GameObject>(enemyModelPrefab, transform);
        rigidBodiesRagdoll = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidBodiesRagdoll)
        {
            rb.isKinematic = true;
        }
        playerTransform = player.transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        currentDestination = endTransform;
        stateHandler = GetComponent<EnemyStateHandler>();
        currentState = initialState;
        enemyAttacksArray = GetComponentsInChildren<EnemyAttack>();
        TurnAttackComponentsOff();

    }

    private void Start()
    {
        currentRgdll = GetComponentInChildren<EnemyRgdll>();
        Debug.Log($"Ragdoll found = {currentRgdll != null}");

        Debug.Log($"enemyAttacksArray Length {enemyAttacksArray.Length}");
    }


    public void SetState(EnemyBehState stateToSet)
    {
        if (currentState!=stateToSet)
        {
            currentState = stateToSet; 
        }
    }

    private void Update()
    {
        if (isAlive)
        {
            switch (currentState)
            {
                case EnemyBehState.Idle:
                    {
                        TurnAttackComponentsOff();
                        ClearAnimationState();
                        animator.SetTrigger("Idle");
                        break;
                    }
                case EnemyBehState.Wandering:
                    {
                        TurnAttackComponentsOff();
                        agent.isStopped = false;
                        animator.SetBool("Walk", true);
                        agent.destination = currentDestination.position;
                        stateHandler.StartCheck();

                        remDist = agent.remainingDistance;

                        if (agent.remainingDistance != 0 & agent.remainingDistance <= 0.8f & agent.remainingDistance != float.PositiveInfinity & agent.remainingDistance != float.NegativeInfinity)
                        {
                            currentDestination = currentDestination == endTransform ? startTransform : endTransform;
                            agent.destination = currentDestination.position;
                        }
                        break;
                    }
                case EnemyBehState.Steering:
                    {
                        agent.isStopped = false;
                        TurnAttackComponentsOff();
                        animator.SetTrigger("Walk");
                        agent.destination = playerTransform.position;
                        if (agent.remainingDistance <= agent.stoppingDistance + 0.02f & playerHealth.isAlive)
                        {
                            currentState = EnemyBehState.Attack;
                        }
                        break;
                    }
                case EnemyBehState.Attack:
                    {
                        agent.isStopped = true; ;
                        transform.rotation = Quaternion.LookRotation(playerTransform.position - transform.position, Vector3.up);
                        TurnAttackComponentsOn();
                        animator.SetTrigger("Attack");
                        if (agent.remainingDistance > agent.stoppingDistance + 0.02f)
                        {
                            if (playerHealth.isAlive)
                            {
                                currentState = EnemyBehState.Steering; 
                            }
                            else currentState = EnemyBehState.Wandering;
                        }
                        break;
                    }

                case EnemyBehState.Death:
                    {
                        TurnAttackComponentsOff();
                        ClearAnimationState();
                        Death();
                        break;
                    }
                default: break;
            }
        }
        else
        {
            agent.enabled = false;
        }
    }

    private void ClearAnimationState()
    {
        animator.SetBool("Walk", false);
    }

    private void TurnAttackComponentsOn()
    {
        for (int i = 0; i < enemyAttacksArray.Length; i++)
        {
            enemyAttacksArray[i].enabled = true;
        }

        Debug.Log($"TurnOn attack on length {enemyAttacksArray.Length}");
    }

    private void TurnAttackComponentsOff()
    {
        for (int i = 0; i < enemyAttacksArray.Length; i++)
        {
            enemyAttacksArray[i].enabled = false;
        }
    }

    private void Death()
    {
        foreach (var rb in rigidBodiesRagdoll)
        {
            rb.isKinematic = false;
        }
        animator.enabled = false;
        currentRgdll.StopConvulsing();
        isAlive = false;
    }
}
