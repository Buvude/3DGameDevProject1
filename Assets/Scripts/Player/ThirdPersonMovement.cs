using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{ //there's a weird bug right now where setting the speed and run speed in inspector fucks things up because of some dumb shit idk. for now just change speed and run speed on lines 53 and 59. i hate everything
    public Transform cam;
    public ConstantForce force; // everything is buggy rn but just don't worry about it i'll fix it tomorrow
    public Grappling grappleScript;
    private float runSpeed;
    private float speed;
    [SerializeField] private float turnSmoothTime = 0.01f;
    private Rigidbody rb;
    [SerializeField] private float jumpForce, jumpCutMultiplier;

    float turnSmoothVelocity;

    float horizontal, vertical, jumpHeld;
    bool jump;
    bool sprint;
    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool isGrounded;

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
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
        GetInput();
        CheckJump();
        //Debug.Log(isGrounded);
    }
    private void FixedUpdate()
    {
        CalculateMovement();
    }
    void CalculateMovement()
    {
        //if (grappleScript.activeGrapple) return;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // sets direction as hor and vert and normalizes
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // gets the target angle with math n shit 
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle - Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, ref turnSmoothVelocity, turnSmoothTime); //easiest way to get strafing working is to just reverse the atan in the rotation damping itself
        transform.rotation = Quaternion.Euler(0f, angle, 0f); // sets rotation

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // idk movedir makes sure it moves in the rotation of the camera's local rotation. basically turns the rotation into a direction
        if (direction.magnitude >= 0.1f) // if there is an input, do this shit
        {
            if (!sprint)
            {
                speed = 7f;
                rb.velocity = new Vector3(moveDir.normalized.x * speed, rb.velocity.y, moveDir.normalized.z * speed); // moves the player. can move while grounded, air movement not implemented.
                //rb.velocity = new Vector3(horizontal, rb.velocity.y, vertical);
            }
            if (sprint)
            {
                runSpeed = 12f;
                rb.velocity = new Vector3(moveDir.normalized.x * runSpeed, rb.velocity.y, moveDir.normalized.z * runSpeed);
            }

        }
        else if (isGrounded) // if no input, and we are grounded, this means we are sliding to a stop. this is just a scuffed way of making it deccelerate faster. WIP. not perfect by any means. i know how to do this better but im tired it's 2 am im lazy af ill fix it later
        {
            if (Mathf.Abs(rb.velocity.x) < 1f && Mathf.Abs(rb.velocity.z) < 1f) // if the abs value of the axis's are less than 1, halt the player to a stop.
            {
                if (rb.velocity.x != 0 || rb.velocity.z != 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x / 2, rb.velocity.y, rb.velocity.z / 2);
                }
            }
        }
        if (rb.velocity.y < 0)
        {
            force.force = new Vector3(rb.velocity.x, rb.velocity.y * 15, rb.velocity.z); // if player is falling, fall faster
        }
        //else if (rb.velocity.y > 0 && !isGrounded) force.force = new Vector3(rb.velocity.x, rb.velocity.y * -2, rb.velocity.z);
        else force.force = Vector3.zero;

        if (rb.velocity.y > 0 && jumpHeld == 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Min(rb.velocity.y, jumpForce / jumpCutMultiplier), rb.velocity.z); //if you let go of jump, cut the jump early
        }
        if (sprint)
        {
            speed = speed * 2;
        }
    }

    void CheckJump()
    {
        if (isGrounded && jump)
        {
            Debug.Log("Loop entered");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // jump bitch
            isGrounded = false;
        }
    }

    void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        jump = Input.GetButtonDown("Jump");
        jumpHeld = Input.GetAxisRaw("Jump");
        sprint = Input.GetKey(KeyCode.LeftShift);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundRadius);
    }
}
