using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    GameObject currentEnemyGrapped;
    private Rigidbody rb;
    private Transform orientation;
    public Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public LayerMask whatIsEnemy;
    public Transform gunTip, camera;
    public float maxGrappleDistance;
    private SpringJoint joint;
    [Header("Grappling Joint Values\n")]
    public float grappleJointStrength;
    public float grappleJointDamper;
    public float grappleJointMassScale;
    private float grappleMaxDist = .8f;
    private float grappleMinDist = .25f;
    [Header("Grappling Movement Values\n")]
    public float horizontalThrustForce;
    public float forwardThrustForce;
    public float extendCableSpeed;
    public bool isGrappling;
    public bool enemyGrappled;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        orientation = GetComponent<Transform>();
    }

    private void Update()
    {
        //Debug.Log(enemyGrappled);
        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Q)) // added q for laptop support frfr
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.position, camera.forward, out hit, maxGrappleDistance, whatIsGrappleable)) // check if the object is grappleable
            {
                GetComponent<AudioSource>().Play();
                StartGrapple();
                isGrappling = true;
            }
            if (Physics.Raycast(camera.position, camera.forward, out hit, maxGrappleDistance, whatIsEnemy )) // check if the object is grappleable
            {
                Debug.Log("Hello");
                StartGrapple();
                isGrappling = true;
            }
        }
        else if (Input.GetMouseButtonUp(2) || Input.GetKeyUp(KeyCode.Q))
        {
            EndGrapple();
            isGrappling = false;
        }
        if (isGrappling)
        {
            CalculateGrappleMovement();
        }
    }


    private void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.position, camera.forward, out hit, maxGrappleDistance, whatIsGrappleable)) // check if the object is grappleable
        {
            grapplePoint = hit.point;
            joint = gameObject.AddComponent<SpringJoint>(); // create a spring joint which will act as the grapple
            joint.autoConfigureConnectedAnchor = false; // we don't want unity to auto configure a joint to attach to
            joint.connectedAnchor = grapplePoint; // we set ^ ourselves here

            float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint); //gets the initial distance
            joint.maxDistance = distanceFromPoint * grappleMaxDist; // sets max dist from point
            joint.minDistance = distanceFromPoint * grappleMinDist;

            joint.spring = grappleJointStrength;
            joint.damper = grappleJointDamper;
            joint.massScale = grappleJointMassScale;

        } else if (Physics.Raycast(camera.position, camera.forward, out hit, maxGrappleDistance, whatIsEnemy)) // check if the object is an enemy
        {
            enemyGrappled = true;
            currentEnemyGrapped = hit.transform.gameObject;
            grapplePoint = currentEnemyGrapped.transform.position;
            joint = gameObject.AddComponent<SpringJoint>(); // create a spring joint which will act as the grapple
            joint.autoConfigureConnectedAnchor = false; // we don't want unity to auto configure a joint to attach to
            joint.connectedAnchor = grapplePoint; // we set ^ ourselves here

            float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint); //gets the initial distance
            joint.maxDistance = distanceFromPoint * grappleMaxDist; // sets max dist from point
            joint.minDistance = distanceFromPoint * grappleMinDist;

            joint.spring = grappleJointStrength;
            joint.damper = grappleJointDamper;
            joint.massScale = grappleJointMassScale;

        }
    }


    private void EndGrapple()
    {
        enemyGrappled = false;
        isGrappling = false;
        Destroy(joint);
    }
    private void CalculateGrappleMovement()
    {
        if (Input.GetKey(KeyCode.D)) // right
        {
            rb.AddForce(orientation.right * horizontalThrustForce * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) // left
        {
            rb.AddForce(-orientation.right * horizontalThrustForce * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W)) // forward
        {
            rb.AddForce(orientation.forward * forwardThrustForce * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) // backward
        {
            rb.AddForce(-orientation.forward * forwardThrustForce * Time.deltaTime);
        } // air movement

        Vector3 directionToPoint = grapplePoint - transform.position;
        rb.AddForce(directionToPoint.normalized * forwardThrustForce * Time.deltaTime);
        if (enemyGrappled && currentEnemyGrapped != null)
        {
            grapplePoint = currentEnemyGrapped.transform.position;
        }
        if (currentEnemyGrapped == null && enemyGrappled)
        {
            EndGrapple();
            isGrappling = false;
            enemyGrappled = false;
        }
        
        float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint); // constantly update the distance from point to pull us further into the point
        if (joint != null)
        {
            joint.maxDistance = distanceFromPoint * grappleMaxDist;
            joint.minDistance = distanceFromPoint * grappleMinDist;
        }





        //extend cable with E (idk if we'll keep this in final but we could)
        /*        if (Input.GetKey(KeyCode.E))
                {
                    float extendedDistanceFromPoint = Vector3.Distance(transform.position, grapplePoint) + extendCableSpeed;

                    joint.maxDistance = extendedDistanceFromPoint * grappleMaxDist;
                    joint.minDistance = extendedDistanceFromPoint * grappleMinDist;
                }*/

    }
    public bool IsGrappling()
    {
        return joint != null;
    }
    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
