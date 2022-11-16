using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectile : MonoBehaviour
{
    public int SlimeBallDamage;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //assign damage to the player 
            collision.gameObject.GetComponent<PlayerStats>().takeDamage(SlimeBallDamage);
        }
        Destroy(this.gameObject);
    }
}
