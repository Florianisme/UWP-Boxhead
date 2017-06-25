using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float speed = 8;
	public GameObject pickupWeapon;
	public GameObject bulletPath;
	public int rpm;
	private float fireDelay;
	private float nextFire;
	private string weapon = "";

	private Quaternion rotation;
	private int health = 100;
	public Slider healthSlider;

	private Rigidbody rig;

	// Use this for initialization
	void Start ()
	{
		rig = GetComponent<Rigidbody> ();
		fireDelay = 60.0f / rpm;
		rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		float hAxis = 0;
		float vAxis = 0;

		if (Input.GetKey (KeyCode.W)) {
			vAxis = 1;
			rotation = Quaternion.Euler (0, -90, 0);
		}
		if (Input.GetKey (KeyCode.S)) {
			vAxis = -1;
			rotation = Quaternion.Euler (0, 90, 0);
		}
		if (Input.GetKey (KeyCode.A)) {
			hAxis = -1;
			rotation = Quaternion.Euler (0, 180, 0);
		}
		if (Input.GetKey (KeyCode.D)) {
			hAxis = 1;
			rotation = Quaternion.Euler (0, 0, 0);
		} 

		if (Input.GetKey (KeyCode.Space) && Time.time >= nextFire && weapon.Length > 0) {
			fire ();
			nextFire = Time.time + fireDelay;
		}

		rig.MoveRotation (rotation);
		rig.MovePosition (transform.position + new Vector3 (hAxis, 0, vAxis) * speed * Time.deltaTime);
	}


	void fire ()
	{
		Destroy(Instantiate (bulletPath, 
			new Vector3 (rig.position.x, rig.position.y, rig.position.z), 
			Quaternion.Euler (rig.rotation.x, rig.rotation.y + Random.Range(-2.0f, 2.0f), rig.rotation.z)), 0.5f);

			RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.right), out hit)) {
			print ("Found an object - distance: " + hit.distance);
			hit.collider.BroadcastMessage ("registerHit");
		}
			
	}


	void onPickupActivated ()
	{
		weapon = "gun";
		Instantiate (pickupWeapon, 
			new Vector3 (transform.position.x + 0.113f, transform.position.y + 0.513f, transform.position.z - 0.335f), 
			Quaternion.Euler (0, 180, 0), gameObject.transform);
	}

	public void dealDamage() {
		Debug.Log ("Hit registered");
		if (health - 5 <= 0)
			killPlayer ();
		else
			health -= 5;
		healthSlider.value = health;
	}

	void killPlayer() {
		Destroy (gameObject);
		healthSlider.value = 0;
	}

}
