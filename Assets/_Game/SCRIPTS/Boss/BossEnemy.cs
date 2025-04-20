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
        currentTargetTransform = testTargetTransform != null ? testTargetTransform : playerTransform;

    }

    private void Start()
    {
        currentRgdll = GetComponentInChildren<EnemyRgdll>();        
    }


    public void SetState(EnemyBehState stateToSet)
    {
        if (currentState != stateToSet)
        {
            currentState = stateToSet;
        }
    }

}
