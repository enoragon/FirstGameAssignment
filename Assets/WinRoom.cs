using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinRoom : MonoBehaviour {
	public Transform spawnFireWorks;
    public Transform FireworksPrefab;
	// Use this for initialization

	//TODO change this to be player
	void OnTriggerEnter (Collider other) {
        Instantiate(FireworksPrefab, spawnFireWorks.position, spawnFireWorks.rotation);
	}
	

}
