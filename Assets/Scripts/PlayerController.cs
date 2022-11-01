using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    public float playerSpeed;
    public float maxSpeed = 25f;
    public float x, z, x1, z1;
    public float drag = 4;
    public Vector3 movementDirection;
    public Vector3 hsp;
    bool inputx;
    bool inputz;
    private void Update()
    {

        MyInput();
        //Debug.Log(movementDirection);
        CalculateMovement();
        SetVelocity();
        if (x1 != 0)
        {
            inputx = true;
        }
        else inputx = false;
        if (z1 != 0)
        {
            inputz = true;
        }
        else inputz = false;
    }

    void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (x1 < maxSpeed)
        {
            x = Input.GetAxis("Horizontal");
        }
        if (z1 < maxSpeed)
        {
            z = Input.GetAxis("Vertical");
        }

        if (x1 > -maxSpeed)
        {
            x1 = Input.GetAxis("Horizontal");
        }
        if (z1 > -maxSpeed)
        {
            z1 = Input.GetAxis("Vertical");
        }

        if (x != 0)
        {
            inputx = true;
        }
        else inputx = false;
        if (z != 0)
        {
            inputz = true;
        }
        else inputz = false;
    }

    void CalculateMovement()
    {
        //movementDirection += new Vector3(x * playerSpeed, 0, z * playerSpeed);
        hsp += new Vector3((x1 * playerSpeed), rb.velocity.y, (z1 * playerSpeed)); // move

        // This section of code ^^ recreates GMS2's lerp function. This is for decceleration.


        // ^ When your deceleration is below 1, instantly decelerate to 0. makes deceleration faster. idk how 2 spell deceleration. is it deccel or decel ??? :(


        rb.velocity = new Vector3(x1 * playerSpeed, rb.velocity.y, z1 * playerSpeed);


    }
    void SetVelocity()
    {

    }
}