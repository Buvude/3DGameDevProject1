using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcingProjectile : MonoBehaviour
{
	

		[Tooltip("Position we want to hit")]
		public Vector3 targetPos;

		[Tooltip("Horizontal speed, in units/sec")]
		public float speed = 10;

		[Tooltip("How high the arc should be, in units")]
		public float arcHeight = 1;

	public GameObject warningMarker;
		GameObject spawnedWarning;

		Vector3 startPos;

		void Start()
		{
			// Cache our start position, which is really the only thing we need
			// (in addition to our current position, and the target).
			startPos = transform.position;
			targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
		spawnedWarning = Instantiate(warningMarker, targetPos, Quaternion.identity);
		}

		void Update()
		{

		// Compute the next position, with arc added in
		float x0 = startPos.x;
		float x1 = targetPos.x;

		float z0 = startPos.z;
		float z1 = targetPos.z;

		float Xdist = x1 - x0;
		float Zdist = z1 - z0;

		float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime );

		float nextZ = Mathf.MoveTowards(transform.position.z, z1, speed * Time.deltaTime );

		

		float baseY = Mathf.Lerp(startPos.y, targetPos.y, nextX - x0 /Xdist);
		float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * Xdist * Xdist);
		
		Vector3 moveTo = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

		Vector3 nextPos = new Vector3(moveTo.x, baseY+arc, moveTo.z);
		
		// Rotate to face the next position, and then move there
		//transform.rotation = transform.LookAt(nextPos);

		transform.position = nextPos;

		// Do something when we reach the target 
		if (nextPos == targetPos|| nextPos.x ==0 ) Arrived();
		}

		void Arrived()
		{
			Destroy(spawnedWarning);
			Destroy(gameObject);
		}

    private void OnDrawGizmos()
    {
		Gizmos.DrawSphere(startPos, .5f);
		Gizmos.DrawSphere(targetPos, .5f);

	}


}

