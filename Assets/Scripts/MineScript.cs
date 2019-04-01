using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour {

	public int health = 1;
	public GameObject explosion;
	public AudioClip ExplosionSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Detonate(){
		Instantiate(explosion, transform.position, transform.rotation);
		AudioSource.PlayClipAtPoint(ExplosionSound, transform.position);
		Destroy(gameObject);
	}
}
