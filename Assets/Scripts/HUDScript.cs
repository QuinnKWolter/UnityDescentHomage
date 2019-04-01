using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

	public GameObject EnergyLeft;
	public GameObject EnergyRight;
	public Image BlueKeyLight;
	public Image YellowKeyLight;
	public Image RedKeyLight;
	public Image Crosshair;
	public Image Cockpit;
	public Image ScreenCover;
	public Text Messages;
	public Text Stats;

	float xR, xL;

	Queue<string> ActiveMessages = new Queue<string>();

	// Use this for initialization
	void Start () {
		xR = (float)EnergyRight.GetComponent<RectTransform>().sizeDelta.x;
		xL = (float)EnergyLeft.GetComponent<RectTransform>().sizeDelta.x;
		Debug.Log("sizeDelta Xs:");
		Debug.Log(EnergyRight.GetComponent<RectTransform>().sizeDelta.x);
		Debug.Log(EnergyLeft.GetComponent<RectTransform>().sizeDelta.x);
		Debug.Log("xR and Xl:");
		Debug.Log(xR);
		Debug.Log(xL);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetPanels(int energy){
		Debug.Log("Energy is: " + energy);
		Debug.Log("energy/200 is: " + ((float)energy/200.0f));
		float percentage = ((float)energy/200.0f);
		float tempR = (float)xR * (float)percentage;
		float tempL = xL * percentage;
		Debug.Log("TempR/TempL = " + tempR + "/" + tempL);

		float yR = EnergyRight.GetComponent<RectTransform>().sizeDelta.y;
		float yL = EnergyLeft.GetComponent<RectTransform>().sizeDelta.y;

		EnergyRight.GetComponent<RectTransform>().sizeDelta = new Vector2(tempR, yR);
		EnergyLeft.GetComponent<RectTransform>().sizeDelta = new Vector2(tempL, yL);
	}

	public void BlueKeyOn(){
		BlueKeyLight.GetComponent<Image>().enabled = true; 
	}
	public void BlueKeyOff(){
		BlueKeyLight.GetComponent<Image>().enabled = false;
	}
	public void YellowKeyOn(){
		YellowKeyLight.GetComponent<Image>().enabled = true;
	}
	public void YellowKeyOff(){
		YellowKeyLight.GetComponent<Image>().enabled = false;
	}
	public void RedKeyOn(){
		RedKeyLight.GetComponent<Image>().enabled = true;
	}
	public void RedKeyOff(){
		RedKeyLight.GetComponent<Image>().enabled = false;
	}
		
	public void SetStats(int Lives, int Shields, int Energy){
		Stats.text = "Lives: " + Lives.ToString () + "\nShields: " + Shields.ToString () + "\nEnergy: " + Energy.ToString ();
	}

	public void PushMessage(string Message){
		ActiveMessages.Enqueue (Message);
	}
}
