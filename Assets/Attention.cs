using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attention : MonoBehaviour {
	
	public Transform player;
    public Transform head;
	Animator anim;

    string state = "pace";
	public GameObject[] waypoints;
	int currentWaypoint = 0;
	public float rotationSpeed = 0.6f;
	public float walkSpeed = 1.5f;
	public float runSpeed = 1.5f;
	float WaypointPrecision = 5.0f;

	public int walkDistance = 15;
	public int runDistance = 10;
	public int attackDistance = 5;
    public int visionAngle = 30;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
        Vector3 direction = player.position - this.transform.position;
		direction.y = 0;
        float angle = Vector3.Angle(direction, this.transform.forward);
		if(state == "pace" && waypoints.Length > 0)
		{
			anim.SetBool("isIdle", false);
			anim.SetBool("isWalking", true);
			if(Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position) < WaypointPrecision)
			{
				currentWaypoint++;
				if(currentWaypoint >= waypoints.Length){
					currentWaypoint = 0;
				}

			}

			direction = waypoints [currentWaypoint].transform.position - transform.position;
			this.transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), rotationSpeed * Time.deltaTime);
			this.transform.Translate (0, 0, Time.deltaTime * walkSpeed);
		}
		if (Vector3.Distance (player.position, this.transform.position) < walkDistance && (angle < visionAngle || state == "pursuing")) {
			
			state = "pursuing";			
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), rotationSpeed * Time.deltaTime);

			if (direction.magnitude > runDistance) {
				this.transform.Translate (0, 0, walkSpeed * Time.deltaTime);
				anim.SetBool ("isAttacking", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isWalking", true);

            } else if (direction.magnitude > attackDistance) {
				anim.SetBool ("isAttacking", false);
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isRunning", true);
				this.transform.Translate (0, 0, runSpeed * Time.deltaTime);
			} else {
				anim.SetBool ("isRunning", false);
				anim.SetBool ("isAttacking", true);
			}
		} else {
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isWalking", true);
			state = "pace";

		}
	}
}
