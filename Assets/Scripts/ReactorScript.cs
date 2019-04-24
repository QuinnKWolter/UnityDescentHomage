using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReactorScript : MonoBehaviour {

	private IEnumerator coroutine;

	bool started = false;
	bool alive = true;
	Transform goal;
	public int health = 100;
	public GameObject explosion, exitDoor;
	public GameObject[] goals;
	public Sprite openExitSprite;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		StartCoroutine(TravelDelay());
		exitDoor = GameObject.Find("ClosedExit");
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
			// Debug.Log("Reactor Destroyed");
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			gameObject.GetComponent<BoxCollider>().enabled = false;
			exitDoor.GetComponent<BoxCollider>().enabled = false;
			exitDoor.GetComponent<SpriteRenderer>().sprite = openExitSprite;
			GetComponent<AudioSource>().Play();
		}
	}

	IEnumerator Explosions(Vector2 point, float delay){
		Instantiate(explosion, (transform.position + (Vector3)point), transform.rotation);
		yield return new WaitForSeconds(delay);
		point = Random.insideUnitCircle;
	}

	IEnumerator TravelDelay(){
		yield return new WaitForSeconds(7.5f);
		int index = Random.Range(0,3);
		goal = goals[index].transform;
		agent.destination = goal.position;
		if (alive){
			StartCoroutine(TravelDelay());
		}
	}
}
