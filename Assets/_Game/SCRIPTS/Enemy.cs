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
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        currentDestination = endTransform;
        stateHandler = GetComponent<EnemyStateHandler>();
        currentState = initialState;

    }

    private void Start()
    {
        currentRgdll = GetComponentInChildren<EnemyRgdll>();
        Debug.Log($"Ragdoll found = {currentRgdll != null}");
    }


    public void SetState(EnemyBehState stateToSet)
    {
        if (currentState!=stateToSet)
        {
            currentState = stateToSet; 
        }
    }

    //public GameObject GetRagdoll()
    //{
    //    return currentRgdll;
    //}

    private void Update()
    {
        if (isAlive)
        {
            switch (currentState)
            {
                case EnemyBehState.Idle:
                    {
                        ClearAnimationState();
                        animator.SetTrigger("Idle");
                        break;
                    }
                case EnemyBehState.Wandering:
                    {
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
                        animator.SetTrigger("Walk");
                        agent.destination = playerTransform.position;
                        if (agent.remainingDistance <= agent.stoppingDistance + 0.02f)
                        {
                            currentState = EnemyBehState.Attack;
                        }
                        break;
                    }
                case EnemyBehState.Attack:
                    {
                        animator.SetTrigger("Attack");
                        if (agent.remainingDistance > agent.stoppingDistance + 0.02f)
                        {
                            currentState = EnemyBehState.Steering;
                        }
                        break;
                    }

                case EnemyBehState.Death:
                    {
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
            //agent.isStopped = true;
        }
    }

    private void ClearAnimationState()
    {
        animator.SetBool("Walk", false);
    }

    private void Death()
    {
        //currentModel.SetActive(false);
        //currentRgdll.transform.position = transform.position;
        //currentRgdll.transform.rotation = transform.rotation;
        foreach (var rb in rigidBodiesRagdoll)
        {
            rb.isKinematic = false;
        }
        animator.enabled = false;
        //currentRgdll.SetActive(true);
        currentRgdll.StopConvulsing();
        isAlive = false;
        //agent.enabled = false;
        //gameObject.SetActive(false);
    }
}
