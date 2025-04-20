using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySM : MonoBehaviour
{
    private StateMachine _stateMachine;
    private Enemy _enemy;
    private EnemyHealth _enemyHealth;

    public State currentState; 


    private void Start()
    {
        _stateMachine = new StateMachine();
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyHealth.OnPlayerDeathEvent += OnDeath;
        _enemy = GetComponent<Enemy>();
        _stateMachine.AddState(new EnemyAttackState(_stateMachine, _enemy));
        _stateMachine.AddState(new EnemyDeathState(_stateMachine, _enemy));
        _stateMachine.AddState(new EnemyIddleState(_stateMachine, _enemy));
        _stateMachine.AddState(new EnemySteeringState(_stateMachine, _enemy));
        _stateMachine.AddState(new EnemyWanderingState(_stateMachine, _enemy));
        _stateMachine.SetState<EnemyWanderingState>();
    }

    private void OnDeath()
    {
        SetState<EnemyDeathState>();
    }

    private void Update()
    {
        _stateMachine.Update();
    }


    public void SetState<T>() where T : State
    {
        _stateMachine.SetState<T>();

        currentState = _stateMachine.CurrentState;
    }
}
