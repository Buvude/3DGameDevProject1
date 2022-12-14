using UnityEngine;
using System.Collections;

public class ArcingProjectile : MonoBehaviour
{
    public float explosionRadius;
    private PlayerStats stats;

    [Tooltip("Position we want to hit")]
    public Vector3 targetPos;

    [Tooltip("Horizontal speed, in units/sec")]
    public float speed = 10;

    [Tooltip("How high the arc should be, in units")]
    public float arcHeight = 1;

    public GameObject warningMarker;
    GameObject spawnedWarning;
    bool notArrived;

    Vector3 startPos;

    void Start()
    {
        notArrived = true;
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        startPos = transform.position;
        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        targetPos.x += Random.Range(-2, 2);
        targetPos.z += Random.Range(-2, 2);
        //targetPos.y = groundPoint();
        spawnedWarning = Instantiate(warningMarker, targetPos, Quaternion.identity);
    }
    //returns the point on the ground where the thrown proj will land 
    public float groundPoint()
    {
        Vector3 skypos = new Vector3(targetPos.x, targetPos.y, targetPos.z + 50);
        RaycastHit r;
        Physics.Raycast(skypos, Vector3.down,out r);
        return r.point.y;
    }
    void Update()
    {
        Debug.Log(notArrived);
        // Compute the next position, with arc added in
        float x0 = startPos.x;
        float x1 = targetPos.x;

        float z0 = startPos.z;
        float z1 = targetPos.z;

        float Xdist = x1 - x0;
        float Zdist = z1 - z0;

        float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);

        float nextZ = Mathf.MoveTowards(transform.position.z, z1, speed * Time.deltaTime);



        float baseY = Mathf.Lerp(startPos.y, targetPos.y, nextX - x0 / Xdist);
        float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * Xdist * Xdist);

        Vector3 moveTo = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        Vector3 nextPos = new Vector3(moveTo.x, baseY + arc, moveTo.z);

        // Rotate to face the next position, and then move there
        //transform.rotation = transform.LookAt(nextPos);

        transform.position = nextPos;

        Vector2 v1 = new Vector2(transform.position.x, transform.position.z);
        Vector2 v2 = new Vector2(targetPos.x, targetPos.z);
        float proximity =Vector2.Distance(v1,v2);

        // Do something when we reach the target 
        if (nextPos == targetPos || proximity < .01f && notArrived) Arrived();
    }

    void Arrived()
    {
        notArrived = false;
        //do an explosion check.. player doesnt have any health or what not yet though
        Collider[] caughtInExplosion;
        caughtInExplosion = Physics.OverlapSphere(targetPos, explosionRadius);
        foreach (Collider item in caughtInExplosion)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                stats.takeDamage(10);
                print("player hit add damage calulation here");

            }
        }

        Destroy(spawnedWarning);
        //Destroy(this.gameObject);
        
        StartCoroutine(delayDestory());
        this.enabled = false;
    }

    IEnumerator delayDestory()
    {

        //turn off visual
        GetComponent<MeshRenderer>().enabled = false;
        
        //play sound
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPos, 3);
    }

}

