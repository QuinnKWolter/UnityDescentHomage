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
	public Image ShieldDisplay;
	public Image ShipDisplay;
	public Image Cockpit;
	public Image ScreenCover;
	public Text Messages;
	public Text Stats;

	// Shields
	public Sprite[] ShieldStates;
	public Sprite[] ShieldInvuln;

	// Weapons
	public Sprite[] WeaponGraphics;
	public Image PrimaryGraphic;
	public Text PrimaryLabel;
	public Text PrimaryAmmo;
	public Image SecondaryGraphic;
	public Text SecondaryLabel;
	public Text SecondaryAmmo;

	float xR, xL;

	Queue<string> ActiveMessages = new Queue<string>();

	// Use this for initialization
	void Start () {
		xR = (float)EnergyRight.GetComponent<RectTransform>().sizeDelta.x;
		xL = (float)EnergyLeft.GetComponent<RectTransform>().sizeDelta.x;
	}

	// Update is called once per frame
	void Update () {

	}

	public void SetPanels(int energy){
		float percentage = ((float)energy/200.0f);
		float tempR = (float)xR * (float)percentage;
		float tempL = xL * percentage;

		float yR = EnergyRight.GetComponent<RectTransform>().sizeDelta.y;
		float yL = EnergyLeft.GetComponent<RectTransform>().sizeDelta.y;

		EnergyRight.GetComponent<RectTransform>().sizeDelta = new Vector2(tempR, yR);
		EnergyLeft.GetComponent<RectTransform>().sizeDelta = new Vector2(tempL, yL);
	}

	public void SetShield(int shield){
		if (shield <= 0){
			ShieldDisplay.GetComponent<Image>().sprite = ShieldStates[9];
		} else if (shield < 25){
			ShieldDisplay.GetComponent<Image>().sprite = ShieldStates[8];
		} else if (shield < 50){
			ShieldDisplay.GetComponent<Image>().sprite = ShieldStates[7];
		} else if (shield < 75){
			ShieldDisplay.GetComponent<Image>().sprite = ShieldStates[6];
		} else if (shield < 100){
			ShieldDisplay.GetComponent<Image>().sprite = ShieldStates[5];
		} else if (shield < 125){
			ShieldDisplay.GetComponent<Image>().sprite = ShieldStates[4];
		} else if (shield < 150){
			ShieldDisplay.GetComponent<Image>().sprite = ShieldStates[3];
		} else if (shield < 175){
			ShieldDisplay.GetComponent<Image>().sprite = ShieldStates[2];
		} else if (shield < 200){
			ShieldDisplay.GetComponent<Image>().sprite = ShieldStates[1];
		} else if (shield >= 200){
			ShieldDisplay.GetComponent<Image>().sprite = ShieldStates[0];
		}
	}

	public void SetPrimary(int graphic, string label, int ammo){
		PrimaryGraphic.GetComponent<Image>().sprite = WeaponGraphics[graphic];
		PrimaryLabel.GetComponent<Text>().text = label;
		PrimaryAmmo.GetComponent<Text>().text = ammo.ToString();
	}

	public string GetPrimary(){
		return PrimaryLabel.GetComponent<Text>().text;
	}

	public void SetSecondary(int graphic, string label, int ammo){
		SecondaryGraphic.GetComponent<Image>().sprite = WeaponGraphics[graphic];
		SecondaryLabel.GetComponent<Text>().text = label;
		SecondaryAmmo.GetComponent<Text>().text = ammo.ToString();
	}

	public string GetSecondary(){
		return SecondaryLabel.GetComponent<Text>().text;
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

	public IEnumerator FlashColor(Color start, Color end, float overTime)
	{
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			ScreenCover.color = Color.Lerp(start, end, (Time.time - startTime)/overTime);
			yield return null;
		}
		startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			ScreenCover.color = Color.Lerp(end, start, (Time.time - startTime)/overTime);
			yield return null;
		}
		ScreenCover.color = start;
	}

	public void ScaleUI(){
		float scaleCockpitX = Screen.width / Cockpit.GetComponent<RectTransform>().rect.width;
		float scaleCockpitY = Screen.height / Cockpit.GetComponent<RectTransform>().rect.height;
		Cockpit.GetComponent<RectTransform>().localScale = new Vector3(scaleCockpitX, scaleCockpitY, 1);
	}
}
