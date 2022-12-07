using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerAnims anim;

    [Header("Player Stats")]
    public int MaxHealth = 100,CurHealth;
    public float armor;
    public float theStat;

  

    [Header("Movement Stats")]
    public float playerWalkingSpeed;
    public float playerRunningSpeed;
    public float playerJumpForce;

    private void Start()
    {
        CurHealth = MaxHealth;
        anim = FindObjectOfType<PlayerAnims>();
    }
    private void Update()
    {
        //Debug.Log(anim);
    }

    public void takeDamage(int dam)
    {
        CurHealth -= dam;
        //Debug.Log("Hello");
        anim.hurtTimer = anim.animDuration;
        anim.tookDmg = true;
        checkHealth();
    }

    public void checkHealth()
    {
        if (CurHealth <= 0)
        {
            //trigger a game over state
            print("gameover");
        }
    }

   
}
