using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour {

	public Texture t1, t2, t3, t4, t5, t6, t7, t8, t9;
	private float timer = .05f;
	private int counter = 1;
	public Material material;

	// Use this for initialization
	void Start () {
		IEnumerator coroutine = DestroyTimer1 ();
		StartCoroutine (coroutine);
		Invoke ("Flicker", timer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void Flicker(){
		if (counter == 1) {
			gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", t1);
		}
		if (counter == 2) {
			gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", t2);
		}
		if (counter == 3) {
			gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", t3);
		}
		if (counter == 4) {
			gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", t4);
		}
		if (counter == 5) {
			gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", t5);
		}
		if (counter == 6) {
			gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", t6);
		}
		if (counter == 7) {
			gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", t7);
		}
		if (counter == 8) {
			gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", t8);
		}
		if (counter == 9) {
			gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", t9);
		}
		if (counter < 9) {
			counter++;
		} else {
			counter = 1;
		}
		Invoke ("Flicker", timer);
	}

	IEnumerator DestroyTimer1(){
		yield return new WaitForSeconds (15f);
		Destroy (gameObject);
	}
	IEnumerator DestroyTimer2(){
		yield return new WaitForSeconds (10f);
		Destroy (gameObject);
	}
}
