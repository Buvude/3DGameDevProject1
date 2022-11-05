using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedScript : MonoBehaviour
{
    public ThirdPersonMovement player;
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        ground = LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ground)
        {
            player.grounded = true;
        }
    }
}
