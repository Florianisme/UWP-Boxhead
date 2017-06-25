using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private GameObject player; // We'll add closest player calculation when adding local multiplayer, just use one for now for every instance
	public int speed;
	public int turnSpeed;
	private int health = 40;

	public int attackSpeed;
	private float fireDelay;
	private float nextFire;

	private Rigidbody rig;

	void Start () {
		player = GameObject.Find("Character One"); //target the player
		rig = GetComponent<Rigidbody> ();
		fireDelay = 60.0f / attackSpeed;
	}


	void Update () {
		if (Vector3.Distance (player.transform.position, transform.position) <= 0.9f && Time.time >= nextFire) {
			player.GetComponent<PlayerController> ().dealDamage ();
			nextFire = Time.time + fireDelay;
		} 
		else { // Move
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (player.transform.position - transform.position), turnSpeed * Time.deltaTime);
			transform.position += transform.forward * speed * Time.deltaTime;


			//Vector3 movement = new Vector3 (1, 0, 1) * speed * Time.deltaTime;

			//rig.MovePosition (transform.position + movement);
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
