using UnityEngine;

public class CameraFollowHelper : MonoBehaviour
{

    public float yOffset;
    public Transform FollowTarget;

    //makes the look at position which hovers over the player follow along the x/z but limit y(vertical) following by a offset

    void Update()
    {
        Vector3 newPosition = new Vector3(FollowTarget.position.x, transform.position.y, FollowTarget.position.z);
        transform.position = newPosition;
        // transform.position.x = FollowTarget.position.x;

    }
}
