using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [Header ("Combat Stats")]
    public float curHealth,maxHealth;
    public float range;
    public float damage;
    public float speed;
    public float aggroRange;
    

    [Header("GAMER STATS")]
    public float SpawnCost;// cost for the game manager to spawn one of these in
    public float pointsForKilling;
    public bool isPOG = true;

  

    public void takeDamage(float dam)
    {
        maxHealth -= dam;
        die();
    }

    virtual public void attack()
    {

    }

    public void die()
    {
        if(curHealth <= 0)
        {
            //update the game manager and send this creature to the shadow realm
            GameObject.Destroy(this.gameObject);
            //spawn some particles maybe
        }
    }


}
