using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorScript : MonoBehaviour {

	Actor actor;

	private IEnumerator coroutine;

	bool started = false;
	bool alive = true;
	public int health = 100;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		actor = GetComponent<Actor>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!alive && !started){
			started = true;
			Vector2 point = Random.insideUnitCircle;
			coroutine = Explosions (point, .1f);
			StartCoroutine (coroutine);
		}
	}

	public void Damage(int damage){
		health -= damage;
		Debug.Log(health + " Reactor health left");
		if (health < 1){
			Debug.Log("Reactor Destroyed");
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}
	
	IEnumerator Explosions(Vector2 point, float delay){
		Instantiate(explosion, (transform.position + (Vector3)point), transform.rotation);
		yield return new WaitForSeconds(delay);
		point = Random.insideUnitCircle;
	}
}
