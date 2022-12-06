using UnityEngine;
using UnityEngine.AI;
public class EyeBall : Enemy
{
    public float flyingOffset;
    [HideInInspector] 
    public float wiggleRoom = .3f;//wiggle room for the offset

    private Transform playerLocation;
    private NavMeshAgent agent;
    public float wanderRadius;
    public float wanderTime;
    
    private float timer;

    private float attackTimer;
    public float attackCooldown = 2.0f;
    private bool canAttack;

    public GameObject attackProjectile;
    public Vector3 RelativePosition;

    private void Start()
    {
        setup();// do all the setup 
    }
    public override void setup()
    {
        base.setup();
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponentInParent<NavMeshAgent>();
        canAttack = true;
        currentState = State.wandering;
        timer = wanderTime;
        attackTimer = attackCooldown;
        curHealth = maxHealth;
        Animator aaaaaa = GetComponentInChildren<Animator>();
        aaaaaa.speed = Random.Range(.9f, 1.1f);
    }

    private void Update()
    {
        GameManager.Instance.Pause();
        if (paused)
        {
        }
        else
        {
            Transform t = transform.parent;
            RelativePosition = new Vector3(t.position.x, transform.position.y, t.position.z);

            //simple state machine do different actions based on what state were in
            switch (currentState)
            {
                case State.wandering:
                    wander();
                    break;
                case State.chasing:
                    JellyAI();//ai for the jelly
                    break;
                case State.attacking:
                    attack();
                    break;
                default:
                    break;
            }
        }
           
        
        

        
    }
    public override void onPauseFunc()
    {
        base.onPauseFunc();
        Animator aaaaaa = GetComponentInChildren<Animator>();
        aaaaaa.speed = 0;
        agent.SetDestination(transform.parent.position);
        GetComponent<Hover>().pause();
    }
    public override void UnPauseFunc()
    {
        base.UnPauseFunc();
        Animator aaaaaa = GetComponentInChildren<Animator>();
        aaaaaa.speed = Random.Range(.9f, 1.1f);
        GetComponent<Hover>().unPause();
    }


    //behaviour specific to the Jellyfish
    public void JellyAI()
    {
        attackTimer += Time.deltaTime;
        Vector3 playerPosIgnoreY = new Vector3(playerLocation.position.x, 0, playerLocation.position.z);
        float distanceToPlayer = Vector3.Distance(transform.parent.position, playerPosIgnoreY);
        if (distanceToPlayer <= range)
        {
            currentState = State.attacking;
            agent.destination = RelativePosition;
        }
        else
        {
            agent.destination = playerLocation.position;
        }
        //drop an attack on them



    }

    public override void attack()
    {
        float distanceToPlayer = Vector3.Distance(transform.parent.position, playerLocation.position);
        if (distanceToPlayer <= range)
        {
            attackTimer += Time.deltaTime;
            if (canAttack && attackTimer >= attackCooldown)
            {
                print("d");
               
                Instantiate(attackProjectile, RelativePosition + Vector3.down/2, Quaternion.identity);
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

        timer += Time.deltaTime;
        if (timer >= wanderTime)
        {
            Vector3 newPos = RandomNavSphere(RelativePosition, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        // see if the player is withing aggro range if so set to chase;
        float distanceFromPlayer = Vector3.Distance(RelativePosition, playerLocation.position);
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