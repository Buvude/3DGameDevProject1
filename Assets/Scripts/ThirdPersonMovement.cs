using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{ //there's a weird bug right now where setting the speed and run speed in inspector fucks things up because of some dumb shit idk. for now just change speed and run speed on lines 46 and 51. i hate everything
    public Transform cam; // there's another bug where camera is stuttery. will be fixed. probably can be fixed by putting movement in fixed update.
    public ConstantForce force; // everything is buggy rn but just don't worry about it i'll fix it tomorrow
    private float runSpeed;
    private float speed;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private Rigidbody rb;
    [SerializeField] private float jumpForce;
    float turnSmoothVelocity;
    public bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float jump = Input.GetAxisRaw("Jump");
        bool sprint = Input.GetKey(KeyCode.LeftShift);


        Debug.Log(rb);
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized; // sets direction as hor and vert and normalizes

        if (direction.magnitude >= 0.1f) // if there is an input, move the character
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // gets the target angle with math n shit 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); //makes rotation way smoother
            transform.rotation = Quaternion.Euler(0f, angle, 0f); // sets rotation

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // idk movedir makes sure it moves in the rotation of the camera's local rotation. basically turns the rotation into a direction

            if (grounded && !sprint)
            {
                speed = 7f;
                rb.velocity = moveDir.normalized * speed; // moves the player. can move while grounded, air movement not implemented.
            }
            if (grounded && sprint)
            {
                runSpeed = 12f;
                rb.velocity = moveDir.normalized * runSpeed;
            }

        }
        else if (grounded) // if no input, and we are grounded, this means we are sliding to a stop. this is just a scuffed way of making it deccelerate faster. WIP. not perfect by any means. i know how to do this better but im tired it's 2 am im lazy af ill fix it later
        {
            if (Mathf.Abs(rb.velocity.x) < .6f && Mathf.Abs(rb.velocity.z) < .6f) // if the abs value of the axis's are less than 1, halt the player to a stop.
            {
                if (rb.velocity.x != 0 && rb.velocity.z != 0) // edge case for if the player is currently stopped, since we want to player to actualy move.
                {
                    rb.velocity = new Vector3(0, 0, 0);
                }
            }
        }
        if (rb.velocity.y < 0)
        {
            force.force = new Vector3(rb.velocity.x, rb.velocity.y * 2, rb.velocity.z); // if player is falling, fall faster
        }
        else force.force = Vector3.zero;

        if (grounded && jump != 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // jump bitch
            grounded = false;
        }
        if (sprint)
        {
            speed = speed * 2;
        }
    }
    private void FixedUpdate()
    {
        
    }
    void CalculateMovement()
    {

        
    }

}
