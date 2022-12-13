using UnityEngine;
using UnityEngine.AI;


public class HazmatEnemy : Enemy
{
    public ScoreManager SM;
    public EventAnimManager eAM;
    private Transform playerLocation;
    private NavMeshAgent agent;
    public float wanderRadius;
    public float wanderTime;
    private Transform target;
    private float timer;

    private float attackTimer;
    public float attackCooldown = 2.0f;
    private bool canAttack;

    public GameObject attackProjectile;
    public Transform LobStartPos;
    private Animator PizzaManDan;
    //initilizations 
    void OnEnable()
    {
        SM = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreManager>();
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        canAttack = true;
        currentState = State.wandering;
        timer = wanderTime;
        attackTimer = attackCooldown;
        curHealth = maxHealth;
        //grab animation controller
        PizzaManDan = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        //if paused or dead do nothing
        if (paused|| currentState == State.dead)
        {
    
        }
        else
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
            PizzaManDan.Play("Walk");
            agent.destination = playerLocation.position;
        }



    }
    public override void onPauseFunc()
    {
        base.onPauseFunc();
        // Animator aaaaaa = GetComponentInChildren<Animator>();
        //aaaaaa.speed = 0;
        agent.SetDestination(transform.position);
       
    }
    public override void UnPauseFunc()
    {
        base.UnPauseFunc();
        // Animator aaaaaa = GetComponentInChildren<Animator>();
        //aaaaaa.speed = 1;
        
    }


    override public void attack()
    {
        //turn to face the target
        Vector3 playerPosIgnoreY = new Vector3(playerLocation.position.x, 0, playerLocation.position.z);
        transform.LookAt(playerPosIgnoreY);

        float distanceToPlayer = Vector3.Distance(transform.position, playerLocation.position);
        if (distanceToPlayer <= range)
        {
            attackTimer += Time.deltaTime;
            if (canAttack && attackTimer >= attackCooldown)
            {
                PizzaManDan.Play("Attack");
                Instantiate(attackProjectile, LobStartPos.position, Quaternion.identity);
                //canAttack = !canAttack;
                attackTimer = 0;
            }
        }
        else
        {
            //revert back to chasing the player
            currentState = State.chasing;
        }
    }



    private void wander()
    {
        PizzaManDan.Play("Walk");
        timer += Time.deltaTime;
        if (timer >= wanderTime)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        // see if the player is withing aggro range if so set to chase;
        float distanceFromPlayer = Vector3.Distance(transform.position, playerLocation.position);
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
    public override void die()
    {
        if (curHealth <= 0 && currentState != State.dead)
        {
            SM.Kill(ScoreManager.EnemyType.HazmatDude);
            PizzaManDan.Play("Death");
        }
        base.die();
    }
}
