using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attention : MonoBehaviour {
	
	public Transform player;
	static Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (player.position, this.transform.position) < 10) {
			Vector3 direction = player.position - this.transform.position;
			direction.y = 0;

			anim.SetBool ("isIdle", false);
			anim.SetBool ("isWalking", true);
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
			if (direction.magnitude > 5) {
				this.transform.Translate (0, 0, 0.05f);

//			} else if (direction.magnitude > 5) { 
//				anim.SetBool ("isWalking", false);
//				anim.SetBool ("isRunning", true);
//				this.transform.Translate (0, 0, 0.07f);
			} else {
				anim.SetBool ("isRunning", false);
				anim.SetBool ("isAttacking", true);
			}
		} else {
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", true);
		}
	}
}
