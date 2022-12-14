using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour
{
    PlayerStats stats;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stats = other.GetComponent<PlayerStats>();
            stats.takeDamage(100);
        }
        
    }
}
