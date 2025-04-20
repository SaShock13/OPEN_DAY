using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;
using Zenject;

public enum EnemyBehState
{
    Iddle,
    Steering,
    Attack,
    Wandering,
    Death
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool isBlind = false;
    [SerializeField] private bool isMovable = false;
    [SerializeField] private Transform testTargetTransform;
    [SerializeField] private EnemyBehState initialState = EnemyBehState.Wandering;
    [SerializeField] private GameObject enemyModelPrefab;
    [SerializeField] private GameObject enemyRagdollPrefab;

    public EnemyBehState currentState;
    public Transform startTransform;
    public Transform endTransform;

    private Transform playerTransform;
    private Transform currentTargetTransform;
    private PlayerHealth playerHealth;
    private Collider capsuleCollider;
    private EnemySight enemySight;
    private Player _player;
    private Animator animator;
    private NavMeshAgent agent;
    private float remDist;
    private Transform currentDestination;
    private EnemySight stateHandler;
    private EnemyAttack[] enemyAttacksArray;
    private GameObject currentModel;
    private EnemyRgdll currentRgdll;
    private Rigidbody[] rigidBodiesRagdoll;
    private EnemySM enemySM;
    private bool isAlive = true;

    public Transform CurrentTargetTransform { get => currentTargetTransform; private set => currentTargetTransform = value; }
    public PlayerHealth PlayerHealth { get => playerHealth; private set => playerHealth = value; }
    public EnemySight EnemySight { get => enemySight; private set => enemySight = value; }
    public bool IsBlind { get => isBlind; private set => isBlind = value; }
    public Animator Animator { get => animator; private set => animator = value; }
    public NavMeshAgent Agent { get => agent; private set => agent = value; }
    public bool IsAlive { get => isAlive; private set => isAlive = value; }
    public Transform CurrentDestination { get => currentDestination; set => currentDestination = value; }
    public EnemySight StateHandler { get => stateHandler; private set => stateHandler = value; }
    public float RemDist { get => remDist; set => remDist = value; }

    public event Action EndAttackEvent;

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
        EnemySight = GetComponent<EnemySight>();
        capsuleCollider = GetComponent<Collider>();
        playerTransform = _player.transform;
        PlayerHealth = _player.GetComponent<PlayerHealth>();
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
        CurrentDestination = endTransform;
        stateHandler = GetComponent<EnemySight>();
        currentState = initialState;
        enemyAttacksArray = GetComponentsInChildren<EnemyAttack>();
        TurnAttackComponentsOff();
        //currentTargetTransform = defaulTargetTransform ;
        CurrentTargetTransform = testTargetTransform != null ? testTargetTransform : playerTransform;

    }

    private void Start()
    {
        enemySM = GetComponent<EnemySM>();
        currentRgdll = GetComponentInChildren<EnemyRgdll>();
    }


    public void SetState(EnemyBehState stateToSet)
    {
        if (currentState!=stateToSet)
        {
            currentState = stateToSet; 
        }
    }

    public void SetSteering()
    {
        //SetState(EnemyBehState.Steering);
        enemySM.SetState<EnemySteeringState>();
    }
    public void SetIddle()
    {
        //SetState(EnemyBehState.Iddle);
        enemySM.SetState<EnemyIddleState>();
    }
    public void SetWandering()
    {
        //SetState(EnemyBehState.Wandering);
        enemySM.SetState<EnemyWanderingState>();
    }
    public void SetAttack()
    {
        //SetState(EnemyBehState.Attack);
        enemySM.SetState<EnemyAttackState>();
    }
    public void SetDeath()
    {
        //SetState(EnemyBehState.Death);
        enemySM.SetState<EnemyDeathState>();
    }

    public void ClearAnimationState()
    {
        Animator.SetBool("Walk", false);
    }

    public void TurnAttackComponentsOn()
    {
        for (int i = 0; i < enemyAttacksArray.Length; i++)
        {
            enemyAttacksArray[i].enabled = true;
        }

        Debug.Log($"TurnOn attack on length {enemyAttacksArray.Length}");
    }

    public void TurnAttackComponentsOff()
    {
        for (int i = 0; i < enemyAttacksArray.Length; i++)
        {
            enemyAttacksArray[i].enabled = false;
        }
    }

    public void Death()
    {
        agent.isStopped = true;
        foreach (var rb in rigidBodiesRagdoll)
        {
            rb.angularDrag = 2;
            rb.drag = 2;
            rb.isKinematic = false;
        }
        capsuleCollider.GetComponent<Rigidbody>().isKinematic = true;
        capsuleCollider.enabled = false;
        Animator.enabled = false;
        currentRgdll.StopConvulsing();
        IsAlive = false;
    }

    //public void StopMoving()
    //{
    //    Agent.isStopped = true;
    //}
    //public void StartMoving()
    //{
    //    Agent.isStopped = false;
    //}


}
