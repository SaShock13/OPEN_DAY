using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;
using Zenject;

public class BossSMMono : MonoBehaviour
{
    private BossStateMachine _stateMachine;
    private BossEnemy boss;
    private Player _player;
    private NavMeshAgent _agent;
    private EnemySight _enemySight;
    private ParticleSystem _fireFx;
    [SerializeField] private Transform target;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform finishPoint;


    [SerializeField] private string currentState;

    [Inject]
    public void Construct(Player player)
    {
       _player = player;
    }



    private void Start()
    {
        //_fireFx.GetComponentInChildren<ParticleSystem>();
        _enemySight = GetComponent<EnemySight>();
        _agent = GetComponent<NavMeshAgent>();
        boss = GetComponent<BossEnemy>();
        _stateMachine = new BossStateMachine();

        _stateMachine.AddState(new BossIddleState(_stateMachine,boss));
        _stateMachine.AddState(new BossWanderingState(_stateMachine, boss, startPoint, finishPoint,_agent));
        _stateMachine.AddState(new BossSteeringState(_stateMachine, boss, _player.transform, _agent));
        _stateMachine.AddState(new BossAttackState(_stateMachine, boss, _player, _agent));
        _stateMachine.AddState(new BossDeathState(_stateMachine, boss, _agent,_enemySight));

        _stateMachine.SetState<BossWanderingState>();
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

    public void OnTargetInSight()
    {
        SetState<BossSteeringState>();
    }

    public void OnTargetInAttackDistance()
    {
        SetState<BossAttackState>();
    }

    public void OnTargetOutOfSight()
    {
        SetState<BossWanderingState>();
    }

    public void OnTargetOutOfAttackDistance()
    {
        SetState<BossSteeringState>();
    }


}
