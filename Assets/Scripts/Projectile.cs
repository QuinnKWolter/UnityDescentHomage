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
		if (explosionPrefab) {
			Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			Destroy (GetComponent<Transform> ().GetChild (0).gameObject);
			Destroy (gameObject.GetComponent<MeshRenderer>());
			
		}
		Destroy (gameObject.GetComponent<Rigidbody>());
		if (impactSound) {
			AudioSource.PlayClipAtPoint (impactSound, transform.position);
		}
		Destroy(gameObject);
	}

}
