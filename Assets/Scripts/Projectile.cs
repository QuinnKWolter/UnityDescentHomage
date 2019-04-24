using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public int damage;
	public GameObject explosionPrefab;
	public AudioClip fireSound;
	public AudioClip impactSound;

	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.GetComponent<ReactorScript>()){
			collision.gameObject.GetComponent<ReactorScript>().Damage(damage);
		}
		if (collision.gameObject.GetComponent<MineScript>()){
			collision.gameObject.GetComponent<MineScript>().Detonate();
		}
		if (collision.gameObject.tag == "Door"){
			collision.gameObject.transform.parent.GetComponent<DoorScript>().Open();
		}
		if (collision.gameObject.tag == "Player"){
			collision.gameObject.GetComponent<PlayerInput>().Damage(damage);
		}
		if (collision.gameObject.tag == "Enemy"){
			collision.gameObject.GetComponent<EnemyScript>().Damage(damage);
		}
		if (explosionPrefab) {
			Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			// Destroy (GetComponent<Transform> ().GetChild (0).gameObject);
			// Destroy (gameObject.GetComponent<MeshRenderer>());
		}
		if (gameObject.tag != "Flare"){
			Destroy (gameObject.GetComponent<Rigidbody>());
			Destroy(gameObject);
		}
	}

}
