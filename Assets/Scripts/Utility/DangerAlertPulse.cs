using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerAlertPulse : MonoBehaviour
{
    public float minScale;
    public float maxScale;
    public float pulseSpeed;
    private bool ScaleDirection = true;
    private Transform transform;

    private void OnEnable()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (ScaleDirection)
        {
            if (transform.localScale.x > minScale)
            {
                transform.localScale = new Vector3(transform.localScale.x - pulseSpeed, 1, transform.localScale.z - pulseSpeed);
            }
            else
            {
                ScaleDirection = !ScaleDirection;
            }
        }
        else
        {
            if (transform.localScale.x < maxScale)
            {
                transform.localScale = new Vector3(transform.localScale.x + pulseSpeed, 1, transform.localScale.z + pulseSpeed);
            }
            else
            {
                ScaleDirection = !ScaleDirection;
            }
        }
        



    }
}
