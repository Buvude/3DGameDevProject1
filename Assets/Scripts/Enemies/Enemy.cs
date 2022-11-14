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

    [HideInInspector]
    public List<ParticleCollisionEvent> collisionEvents;

    public GameObject damagePopUp;
    private GameObject currentDamPop;

    private void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents;
        try
        {
            numCollisionEvents = other.GetComponent<ParticleSystem>().GetCollisionEvents(this.gameObject, collisionEvents);
        }
        catch (System.Exception)
        {
            numCollisionEvents = 0;
            throw;
        }
       
        print("num hits " + numCollisionEvents);
        // num collision events is shotgun pellets taken to the face
        
        if (numCollisionEvents > 0)
        {
            takeDamage(numCollisionEvents);
            numCollisionEvents = 0;
        }

    }

    public void takeDamage(float dam)
    {
        maxHealth -= dam;
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

    public void die()
    {
        if (curHealth <= 0)
        {
            //update the game manager and send this creature to the shadow realm
            GameObject.Destroy(this.gameObject);
            //spawn some particles maybe
        }
    }


}
