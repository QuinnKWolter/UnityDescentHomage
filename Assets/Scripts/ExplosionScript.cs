using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {

	//Change to reflect what's exploding eventually.
	public float radius = 0.0001f;
	public float power = 10.0f;

	// Use this for initialization
	void Start () {
		//Apply explosive force to all nearby rigidbodies
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
		GetComponent<AudioSource>().Play();
		StartCoroutine(DestroyExplosion());

		foreach (Collider hit in colliders){

			Debug.Log(hit.tag);

			if (!hit){
				continue;
			}

			if (hit.GetComponent<Rigidbody>()){
				hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0f);
			}

			if (hit.tag == "Door"){
				hit.transform.parent.GetComponent<DoorScript>().Open();
			}

			if (hit.tag == "Light"){
				hit.GetComponent<LightScript>().Break();
			}

		}
	}

	// Update is called once per frame
	void Update () {
		var v = transform.position - Camera.main.transform.position;
		//v.y = 0.0f;
		transform.rotation = Quaternion.LookRotation (v);
	}

	IEnumerator DestroyExplosion()
	{
			yield return new WaitForSeconds(2);
			Destroy(gameObject);
	}
}
