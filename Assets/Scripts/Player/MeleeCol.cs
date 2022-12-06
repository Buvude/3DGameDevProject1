using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCol : MonoBehaviour
{
    // Start is called before the first frame update
    float meleeDamage = 12;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) 
        {
            Debug.Log("Hit enemy");
            //Debug.Log(other.gameObject);
            Enemy target = other.gameObject.GetComponent<Enemy>();
            target.takeDamage(meleeDamage);
        }
    }
}
