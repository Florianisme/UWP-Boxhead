using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private Transform player; // We'll add closest player calculation when adding local multiplayer, just use one for now for every instance
	public int speed;
	public int turnSpeed;
	private int health = 100;

	private Rigidbody rig;

	void Start () {
		player = GameObject.Find("Character One").transform; //target the player
		rig = GetComponent<Rigidbody> ();
	}


	void Update () {
		if (Vector3.Distance (player.position, transform.position) <= 0.9f) {
			// Attack!
		} 
		else { // Move
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (player.position - transform.position), turnSpeed * Time.deltaTime);
			//transform.position += transform.forward * speed * Time.deltaTime;


			Vector3 movement = new Vector3 (1, 0, 1) * speed * Time.deltaTime;

			rig.MovePosition (transform.position + movement);
		}
	}

	void registerHit() {
		Debug.Log ("Hit registered");
		if (health - 20 <= 0)
			killEnemy ();
		else
			health -= 20;
	}

	void killEnemy() {
		SpawnEnemySystem.enemyKilled (gameObject);
		Destroy (gameObject);
	}
}
