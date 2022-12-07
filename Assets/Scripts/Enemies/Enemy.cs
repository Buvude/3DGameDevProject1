using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [Header("Combat Stats")]
    public float curHealth, maxHealth;
    public float range;
    public float damage;
    public float speed;
    public float aggroRange;


    [Header("GAMER STATS")]
    public float SpawnCost;// cost for the game manager to spawn one of these in
    public float pointsForKilling;
    public bool isPOG = true;

    //controls ability to pause the game
    public bool paused = false;

    [HideInInspector]
    public List<ParticleCollisionEvent> collisionEvents;

    public GameObject damagePopUp;
    private GameObject currentDamPop;

    public State currentState;
    public enum State
    {
        wandering,
        chasing,
        attacking,
        dead
    }
     private void Start()
    {
        setup();
        //events that listen for game manager
       
    }
    protected virtual void Pause() { 
        paused = true;
        onPauseFunc();
    }
    protected virtual void UnPause() { 
        paused = false;
        UnPauseFunc();
    }

    virtual public void onPauseFunc()
    {

    }
    virtual public void UnPauseFunc()
    {

    }



    virtual public void setup()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        GameManager.Instance.OnPause += Pause;
        GameManager.Instance.OnUnPause += UnPause;
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents=0;
        try
        {
            numCollisionEvents = other.GetComponent<ParticleSystem>().GetCollisionEvents(this.gameObject, collisionEvents);
        }
        catch (System.Exception)
        {
            numCollisionEvents = 0;
            throw;
        }
       
        
        // num collision events is shotgun pellets taken to the face
        
        if (numCollisionEvents > 0)
        {
            takeDamage(numCollisionEvents);
            numCollisionEvents = 0;
        }

    }

    public void takeDamage(float dam)
    {
        curHealth -= dam;
        die();
        UpdatePopUp((int)dam);
    }

    public void UpdatePopUp(int incomingDam)
    {
        if (currentDamPop == null)
        {
            currentDamPop = Instantiate(damagePopUp, transform.position, Quaternion.identity);
            currentDamPop.GetComponent<DamagePopUp>().Setup(incomingDam);
        }
        else
        {
            currentDamPop.GetComponent<DamagePopUp>().addDamage(incomingDam);
        }
    }

    virtual public void attack()
    {

    }

    //removes corpses after a set time
    IEnumerator GarbageMan()
    {
        yield return new WaitForSecondsRealtime(5);
        Destroy(this.gameObject);
    }

    virtual public void die()
    {
        if (curHealth <= 0)
        {
            print(curHealth);
            currentState = State.dead;
            //update the game manager and send this creature to the shadow realm invoke die after some time method
            StartCoroutine(GarbageMan());
            //spawn some particles maybe
        }
    }


}
