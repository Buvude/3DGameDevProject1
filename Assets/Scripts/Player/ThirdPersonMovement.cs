using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{ //there's a weird bug right now where setting the speed and run speed in inspector fucks things up because of some dumb shit idk. for now just change speed and run speed on lines 53 and 59. i hate everything
    public GameObject uIScreen;
    public EventAnimManager eAM;
    public Transform cam;
    public ConstantForce force; // everything is buggy rn but just don't worry about it i'll fix it tomorrow
    public Grappling grappleScript;
    private PlayerStats stats;
    [SerializeField] private float turnSmoothTime = 0.01f;
    private Rigidbody rb;
    [SerializeField] private float jumpCutMultiplier;
    //float boxScale = .5f;
    Vector3 groundBox = new Vector3(.5f, .125f, .5f);

    float turnSmoothVelocity;

    float horizontal, vertical, jumpHeld;
    bool jump;
    bool sprint;
    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] public bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        //eAM = GameObject.FindGameObjectWithTag("EventManeger").GetComponent<EventAnimManager>();
        stats = GetComponent<PlayerStats>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            eAM.Pause();
            uIScreen.gameObject.SetActive(true);
        }
        CheckGround();
        GetInput();
        CheckJump();
        //Debug.Log(isGrounded);
    }
    private void FixedUpdate()
    {
        CalculateMovement();
    }
    public void unpausePlayer()
    {
        uIScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None; 
        eAM.Unpause();
    }
    void CalculateMovement()
    {
        //if (grappleScript.activeGrapple) return;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // sets direction as hor and vert and normalizes
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // gets the target angle with math n shit 
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle - Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, ref turnSmoothVelocity, turnSmoothTime); //easiest way to get strafing working is to just reverse the atan in the rotation damping itself (i am lazy)
        transform.rotation = Quaternion.Euler(0f, angle, 0f); // sets rotation

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // idk movedir makes sure it moves in the rotation of the camera's local rotation. basically turns the rotation into a direction
        if (direction.magnitude >= 0.1f) // if there is an input, do this shit
        {
            if (!sprint && !grappleScript.isGrappling && isGrounded)
            {
                //speed = 7f;
                rb.velocity = new Vector3(moveDir.normalized.x * stats.playerWalkingSpeed, rb.velocity.y, moveDir.normalized.z * stats.playerWalkingSpeed); // moves the player. can move while grounded
                //rb.velocity = new Vector3(horizontal, rb.velocity.y, vertical);
            } else if (!isGrounded && !grappleScript.isGrappling)
            {
                CalculateAirMovement();
            }
            if (sprint && !grappleScript.isGrappling && isGrounded)
            {
                //runSpeed = 12f;
                rb.velocity = new Vector3(moveDir.normalized.x * stats.playerRunningSpeed, rb.velocity.y, moveDir.normalized.z * stats.playerRunningSpeed);
            } else if (!isGrounded && !grappleScript.isGrappling)
            {
                CalculateAirMovement(); 
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
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Min(rb.velocity.y, stats.playerJumpForce / jumpCutMultiplier), rb.velocity.z); //if you let go of jump, cut the jump early
        }
    }

    void CalculateAirMovement()
    {
        if (Input.GetKey(KeyCode.D)) // right
        {
            rb.AddForce(gameObject.transform.right * grappleScript.horizontalThrustForce * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) // left
        {
            rb.AddForce(-gameObject.transform.right * grappleScript.horizontalThrustForce * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W)) // forward
        {
            rb.AddForce(gameObject.transform.forward * grappleScript.forwardThrustForce * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) // backward
        {
            rb.AddForce(-gameObject.transform.forward * grappleScript.forwardThrustForce * Time.deltaTime);
        }
        if (sprint)
        {
            if (Mathf.Abs(rb.velocity.x) > stats.playerRunningSpeed)
            {
                rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * stats.playerRunningSpeed, rb.velocity.y, rb.velocity.z);
            }
            if (Mathf.Abs(rb.velocity.z) > stats.playerRunningSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Sign(rb.velocity.z) * stats.playerRunningSpeed);
            }
        } else if (!sprint)
        {
            if (Mathf.Abs(rb.velocity.x) > stats.playerWalkingSpeed)
            {
                rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * stats.playerWalkingSpeed, rb.velocity.y, rb.velocity.z);
            }
            if (Mathf.Abs(rb.velocity.z) > 12)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Sign(rb.velocity.z) * stats.playerWalkingSpeed);
            }
        }

    }

    void CheckJump()
    {
        if (isGrounded && jump)
        {
            rb.AddForce(Vector3.up * stats.playerJumpForce, ForceMode.Impulse); // jump bitch
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

    void CheckGround()
    { 
        isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(.5f, .125f, .5f), /*new Quaternion(0, 0, 0, 0)*/transform.rotation, whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(.5f, .125f, .5f) * 2);
    }


}
