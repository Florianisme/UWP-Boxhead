using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemySystem : MonoBehaviour {

	//public PlayerHealth playerHealth;       // Reference to the player's heatlh.
	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public static LinkedList<GameObject> enemys;


	void Start ()
	{
		enemys = new LinkedList<GameObject>();
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}


	void Spawn ()
	{
 		if (enemys.Count < 5) {
			// Find a random index between zero and one less than the number of spawn points.
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
			enemys.AddLast (Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation));
		}
	}

	public static void enemyKilled(GameObject gObject) {
		enemys.Remove (enemys.Find (gObject));
	}
}