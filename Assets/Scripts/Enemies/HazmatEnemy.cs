using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HazmatEnemy : Enemy
{
    private Transform playerLocation;
    private NavMeshAgent agent;
    public float wanderRadius;
    public float wanderTimer;
    private Transform target;
    private float timer;


    [Tooltip("Position we want to hit")]
    public Vector3 targetPos;

    [Tooltip("Horizontal speed, in units/sec")]
    public float projectileSpeed = 10;

    [Tooltip("How high the arc should be, in units")]
    public float arcHeight = 1;

    private State currentState;

    enum State
    {
        wandering,
        chasing,
        attacking
    }


    private void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        currentState = State.wandering;
        timer = wanderTimer;
    }


    private void Update()
    {
        //simple state machine do different actions based on what state were in
        switch (currentState)
        {
            case State.wandering:
                wander();
                break;
            case State.chasing:
                hazmatAI();
                break;
            case State.attacking:
                attack();
                break;
            default:
                break;
        }

    }

    private void hazmatAI()
    {
        //cheese code that doesnt squad power gang gang gang ya ya ya
        float ranx = Random.Range(-1.0f, 1.0f);
        float ranz = Random.Range(-1.0f, 1.0f);
        // agent.nextPosition = agent.nextPosition + new Vector3(ranx*.1f, 0, ranz*.1f);

        //if within range set state to attack destination to poing and do attack
        float distanceToPlayer = Vector3.Distance(transform.position, playerLocation.position);
        if (distanceToPlayer <= range)
        {
            currentState = State.attacking;
            agent.destination = transform.position;
        }
        else
        {
            agent.destination = playerLocation.position;
        }


       
    }

    override public void attack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerLocation.position);
        if (distanceToPlayer <= range)
        {
            //do attack grenade toss type of attack
            
        }
        else
        {
            //revert back to chasing the player
            currentState = State.chasing;
        }
    }

    private void wander()
    {

        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        // see if the player is withing aggro range if so set to chase;
        float distanceFromPlayer =Vector3.Distance(transform.position, playerLocation.position);
        if (distanceFromPlayer <= aggroRange)
            currentState = State.chasing;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

}
