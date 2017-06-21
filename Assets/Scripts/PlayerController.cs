﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 8;
	public GameObject pickupWeapon;
	public Transform pickupSpawnLocation;

    private Rigidbody rig;

	// Use this for initialization
	void Start () 
    {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float hAxis = 0;
		float vAxis = 0;

		if (Input.GetKey(KeyCode.W)) {
			vAxis = 1;
			transform.rotation = Quaternion.Euler(0, -90, 0);
		}
		if (Input.GetKey(KeyCode.S)) {
			vAxis = -1;
			transform.rotation = Quaternion.Euler(0, 90, 0);
		}
		if (Input.GetKey(KeyCode.A)) {
			hAxis = -1;
			transform.rotation = Quaternion.Euler(0, 180, 0);
		}
		if (Input.GetKey (KeyCode.D)) {
			hAxis = 1;
			transform.rotation = Quaternion.Euler(0, 0, 0);
		} 

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;

        rig.MovePosition(transform.position + movement);
	}

	void onPickupActivated() {
		Instantiate (pickupWeapon, 
			new Vector3(transform.position.x + 0.113f, transform.position.y + 0.513f, transform.position.z -0.335f), 
			Quaternion.Euler(0, 180, 0), gameObject.transform);
	}
}