using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    /// <summary>
    /// Melee damage is located in the MeleeCol script
    /// </summary>
    Rigidbody rb;
    public float startAngle;
    public float endAngle;
    float attackTimer;
    float attackTime = .3f;
    float attackCoolD = 1f;
    public float attackCooldTimer;
    bool attacking;
    bool slowDownVelIfTrue = false;
    GameObject sword;

    private void Awake()
    {
        sword = GameObject.FindGameObjectWithTag("melee");
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (attacking)
        {
            sword.SetActive(true);
        }
        else sword.SetActive(false);

        if (Input.GetKeyDown(KeyCode.E) && !attacking && attackCooldTimer < 0)
        {
            attacking = true;
            slowDownVelIfTrue = true;
            attackTimer = attackTime;
            attackCooldTimer = attackCoolD;
        }
        if (attacking)
        {
            Attack(endAngle);
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
            {
                attacking = false;
            }
        }
        else Attack(startAngle);
        if (!attacking)
        {
            attackCooldTimer -= Time.deltaTime;
        }
        if (slowDownVelIfTrue)
        {
            rb.velocity = new Vector3(rb.velocity.x / 3, rb.velocity.y, rb.velocity.z / 3);
            slowDownVelIfTrue = false;
        }

    }


    void Attack(float angle)
    {
        
        //This method rotates the weapon
        Quaternion newQuaternion = new Quaternion();
        newQuaternion.eulerAngles = new Vector3(transform.localRotation.x, angle, transform.localRotation.z);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, newQuaternion, Time.deltaTime * 15f);
        //transform.rotation = newQuaternion;

    }
}
