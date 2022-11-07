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
    private CurrentState state;

    [Header("GAMER STATS")]
    public float SpawnCost;// cost for the game manager to spawn one of these in
    public float pointsForKilling;
    public bool isPOG = true;

    enum CurrentState
    {
        Idle,//idle animations might not exist for some creatures
        Walk,//walk/fly/move
        Attack//trigger for attacking
    }

    private void Start()
    {
        //enemies start idling when they arrive
        state = CurrentState.Idle;
    }


    public void takeDamage(float dam)
    {
        maxHealth -= dam;
        die();
    }

    public void attack()
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
