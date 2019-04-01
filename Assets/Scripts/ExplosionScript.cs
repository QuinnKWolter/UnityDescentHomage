using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {

	//Change to reflect what's exploding eventually.
	public float radius = 5.0f;
	public float power = 10.0f;

	// Use this for initialization
	void Start () {
		//Apply explosive force to all nearby rigidbodies
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);

		foreach (Collider hit in colliders){
			if (!hit){
				continue;
			}
			
			if (hit.GetComponent<Rigidbody>()){
				hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0f);
			}
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		var v = transform.position - Camera.main.transform.position;
		//v.y = 0.0f;
		transform.rotation = Quaternion.LookRotation (v);
	}
}
