using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    Animator animator;
    ThirdPersonMovement player;
    bool coolDownActive;
    Rigidbody playerRb;
    MeleeAttack punchRef;
    public float animDuration = .958f;
    float punchTimer;
    public float hurtTimer;
    bool punching;
    public bool tookDmg;
    // Start is called before the first frame update
    void Start()
    {
        punchRef = FindObjectOfType<MeleeAttack>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>();
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(playerRb.velocity.x) < 0.4 && Mathf.Abs(playerRb.velocity.z) < 0.4)
        {
            animator.SetBool("isMoving", false);
        }
        else animator.SetBool("isMoving", true);
        if (Input.GetKeyDown(KeyCode.E) && !coolDownActive)
        {
            punchTimer = animDuration;
            punching = true;
            animator.SetBool("isPunching", true);
        }
        if (punching)
        {
            punchTimer -= Time.deltaTime;
        }
        if (punchTimer < 0)
        {
            punching = false;
            animator.SetBool("isPunching", false);
        }


        if (punchRef.attackCooldTimer < 0)
        {
            coolDownActive = false;
        }
        else coolDownActive = true;



        if (tookDmg)
        {
            animator.SetBool("gotHurt", true);
            hurtTimer -= Time.deltaTime;
        }
        if (hurtTimer < 0)
        {
            tookDmg = false;
            animator.SetBool("gotHurt", false);
            
        }


        if (player.isGrounded == true)
        {
            animator.SetBool("grounded", true);
        }
        else animator.SetBool("grounded", false);

    }
}
