using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;
using Zenject;

public class BossSMMono : MonoBehaviour
{
    private BossStateMachine _stateMachine;
    private BossEnemy _boss;
    private Player _player;
    private NavMeshAgent _agent;
    private EnemySight _enemySight;
    private ParticleSystem _fireFx;
    private EnemyHealth _enemyHealth;
    private BossAttack _bossAttack;
    private Animator _bossAnimator;
    public Transform target;
    public Transform startPoint;
    public Transform finishPoint;
    [SerializeField] private string currentState;

    public BossEnemy Boss { get => _boss; private set => _boss = value; }
    public Player MyPlayer { get => _player; private set => _player = value; }
    public NavMeshAgent Agent { get => _agent; private set => _agent = value; }
    public EnemySight EnemySight { get => _enemySight; private set => _enemySight = value; }
    public ParticleSystem FireFx { get => _fireFx; private set => _fireFx = value; }
    public EnemyHealth EnemyHealth { get => _enemyHealth; private set => _enemyHealth = value; }
    public BossAttack BossAttack { get => _bossAttack; set => _bossAttack = value; }
    public Animator BossAnimator { get => _bossAnimator; set => _bossAnimator = value; }

    [Inject]
    public void Construct(Player player)
    {
       MyPlayer = player;
    }

    private void Start()
    {
        EnemyHealth = GetComponent<EnemyHealth>();
        EnemyHealth.OnPlayerDeathEvent += OnDeath;
        EnemySight = GetComponent<EnemySight>();
        Agent = GetComponent<NavMeshAgent>();
        Boss = GetComponent<BossEnemy>();
        BossAttack = GetComponentInChildren<BossAttack>();
        BossAnimator = GetComponentInChildren<Animator>();
        _stateMachine = new BossStateMachine();
        target = MyPlayer.transform;
        EnemySight.StartCheck();
        _stateMachine.AddState(new BossIddleState(_stateMachine,this));
        _stateMachine.AddState(new BossWanderingState(_stateMachine, this));
        _stateMachine.AddState(new BossSteeringState(_stateMachine, this));
        _stateMachine.AddState(new BossAttackState(_stateMachine, this));
        _stateMachine.AddState(new BossDeathState(_stateMachine, this));
        //_stateMachine.AddState(new BossIddleState(_stateMachine,_boss));
        //_stateMachine.AddState(new BossWanderingState(_stateMachine, _boss, startPoint, finishPoint,_agent,_enemySight));
        //_stateMachine.AddState(new BossSteeringState(_stateMachine, _boss, _player.transform, _agent, _enemySight));
        //_stateMachine.AddState(new BossAttackState(_stateMachine, _boss, _player, _agent, _enemySight, _bossAttack));
        //_stateMachine.AddState(new BossDeathState(_stateMachine, _boss, _agent,_enemySight));
        _stateMachine.SetState<BossWanderingState>();
    }

    private void OnDeath()
    {
        SetState<BossDeathState>();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void SetState<T>() where T : BossState
    {
        _stateMachine.SetState<T>();
        currentState = nameof(T); /// название состояния для дебага
    }

    internal void AttackAnimationFinished()
    {
        
    }

    //public void OnTargetInSight()
    //{
    //    SetState<BossSteeringState>();
    //}

    //public void OnTargetInAttackDistance()
    //{
    //    SetState<BossAttackState>();
    //}

    //public void OnTargetOutOfSight()
    //{
    //    SetState<BossWanderingState>();
    //}

    //public void OnTargetOutOfAttackDistance()
    //{
    //    SetState<BossSteeringState>();
    //}


}
