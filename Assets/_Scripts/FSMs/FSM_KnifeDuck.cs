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
    private Player_Manager player;
    private Rigidbody rigidbody3D;
    private Vector3 playerPos = new Vector3();

    public float speed = 10.0f;
    public float distanceToAttack = 5f;
    public float distanceToChase = 7f;
    public float jumpForce = 10f;
    public float jumpSpeed = 30f;
    public bool jumping = false;

    void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody3D = GetComponent<Rigidbody>();
    }

    void Start()
    {
        player = Game_Manager.GetGameController().GetPlayer();
    }

    void Update()
    {
        playerPos = player.transform.position;

        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.CHASE);
                break;
            case State.CHASE:
                //Check transitions
                if (DistanceToPlayer() <= distanceToAttack)
                    ChangeState(State.ATTACK);
                else if (jumping)
                    ChangeState(State.JUMP);
                //does pursue
                break;
            case State.ATTACK:
                //Check transitions
                if (DistanceToPlayer() > distanceToChase)
                    ChangeState(State.CHASE);
                //Attack

                break;
            case State.JUMP:
                //Check transitions
                if (!jumping)
                    ChangeState(State.CHASE);
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
                navMeshAgent.isStopped = true;
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
                navMeshAgent.isStopped = false;
                navMeshAgent.destination = playerPos;
                break;
            case State.ATTACK:
                print("attacking");
                break;
            case State.JUMP:
                navMeshAgent.isStopped = true;
                rigidbody3D.AddForce(new Vector3(0, 1, 0) * jumpForce);
                rigidbody3D.velocity = transform.forward * jumpSpeed;
                break;
            case State.DEAD:
                enabled = false;
                break;
        }

        currentState = newState;
    }

    float DistanceToPlayer()
    {
        return (playerPos - gameObject.transform.position).magnitude;
    }
}
