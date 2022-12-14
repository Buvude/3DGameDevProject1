using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithGrap : MonoBehaviour
{

    public Grappling grappling;
    public Transform other;
    private Quaternion desiredRotation;
    private float rotationSpeed = 5f;

    void Update()
    {
        if (!grappling.IsGrappling())
        {
            desiredRotation = transform.parent.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            //transform.LookAt(grappling.GetGrapplePoint(), transform.up); // uncomment this line for the player to rotate with the grapple, dont know if we want this in.
            //transform.RotateAround(grappling.GetGrapplePoint(), transform.up, Time.deltaTime);
            
            //transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y - 45, transform.rotation.z);
            //
            //desiredRotation = Quaternion.LookRotation(grappling.GetGrapplePoint() - new Vector3(transform.position.x, transform.position.y * transform.position., transform.position.z));
        }

    }
}
