using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private Transform player; // We'll add closest player calculation when adding local multiplayer, just use one for now for every instance
	public int speed;
	private int health = 100;

	void Start () {
		player = GameObject.Find("Character One").transform; //target the player
	}


	void Update () {
		if (Vector3.Distance (player.position, transform.position) <= 0.9f) {
			// Attack!
		} 
		else { // Move
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (player.position - transform.position), speed * Time.deltaTime);
			transform.position += transform.forward * speed * Time.deltaTime;
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
