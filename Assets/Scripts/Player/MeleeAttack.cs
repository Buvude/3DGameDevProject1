using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float startAngle;
    public float endAngle;
    float attackTimer;
    float attackTime = .3f;
    bool attacking;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !attacking)
        {
            attacking = true;
            attackTimer = attackTime;
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
