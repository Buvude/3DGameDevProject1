using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedScript : MonoBehaviour
{
    public ThirdPersonMovement player;
    private LayerMask ground;
    private LayerMask wall;
    // Start is called before the first frame update
    void Start()
    {
        ground = LayerMask.NameToLayer("Ground");
        wall = LayerMask.NameToLayer("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ground)
        {
            //player.grounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ground)
        {
            //player.grounded = false;
        }
    }
}
