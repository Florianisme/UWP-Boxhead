using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		other.BroadcastMessage ("onPickupActivated");
		Destroy (gameObject);
	}
}
