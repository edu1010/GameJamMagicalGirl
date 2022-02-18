using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_KnifeDuck : MonoBehaviour
{
    public enum State { INITIAL, CHASE, ATTACK, JUMP, DEAD }
    public State currentState = State.INITIAL;

    private HealthComponent healthComponent;
    private NavMeshAgent navMeshAgent;

    public float speed = 10.0f;

    void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.CHASE);
                break;
            case State.CHASE:
                //pursue player
                break;
            case State.ATTACK:
                //do animation
                break;
            case State.JUMP:
                //jump to next platform
                break;
            case State.DEAD:
                //nothing
                break;
        }
    }

    private void ChangeState(State newState)
    {
        //OnExit
        switch (currentState)
        {
            case State.CHASE:
                break;
            case State.ATTACK:
                break;
            case State.JUMP:
                break;
            case State.DEAD:
                break;
        }

        //OnEnter
        switch (newState)
        {
            case State.CHASE:
                //navMeshAgent.destination = player;
                break;
            case State.ATTACK:
                break;
            case State.JUMP:
                break;
            case State.DEAD:
                enabled = false;
                break;
        }

        currentState = newState;
    }
}
