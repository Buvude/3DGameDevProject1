using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{


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
    }

    public void takeDamage(int dam)
    {
        CurHealth -= dam;
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
