using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float flyingOffsetMe;
    private float wiggleRoomMe;
    private void Start()
    {
        EyeBall Dad = GetComponentInParent<EyeBall>();
        flyingOffsetMe = Dad.flyingOffset;
        wiggleRoomMe = Dad.wiggleRoom;
    }


    private void Update()
    {
        //slowly float towards the flying offset

        RaycastHit ground;
        Physics.Raycast(transform.position, Vector3.down, out ground, 6000);

        if (ground.collider != null)
        {
            //find the distance from the ground 
            Vector3 groundPoint = ground.point;
            float distance = Vector3.Distance(transform.position, groundPoint);
            if (distance < flyingOffsetMe - wiggleRoomMe)
            {
                // lerp towards height
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 10, transform.position.z), .01f);
            }

            else if (distance > flyingOffsetMe + wiggleRoomMe)
            {
                //liggity lerpy
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 10, transform.position.z), .01f);
            }

        }



        //do a look at the player

        //do something when in range slowly float towards the player? or just float around in a general area who knows I dont

        //do a small bobbing animation 

        //do a medium sized slow moving projectile at the player yeah That sounds epic some fun particle effects maybe

        //do hard drugs

        //wait no dont do that

        //YESSS DO THE SUBSTANCES

        //...
    }


}
