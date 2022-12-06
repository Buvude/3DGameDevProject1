using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    /// <summary>
    /// Melee damage is located in the MeleeCol script
    /// </summary>
    public float startAngle;
    public float endAngle;
    float attackTimer;
    float attackTime = .3f;
    float attackCoolD = 1f;
    float attackCooldTimer;
    bool attacking;
    GameObject sword;

    private void Awake()
    {
        sword = GameObject.FindGameObjectWithTag("melee");
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
