using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

	public int DIFFICULTY_MULTIPLIER = 6;

	public GameObject Laser1;
	public GameObject Laser2;
	public GameObject Laser3;
	public GameObject Laser4;
	public GameObject Laser5;
	public GameObject Laser6;
	public GameObject Flare;
	public GameObject Headlight;

	public Camera MainCam;

	public Transform laserSpawn1;
	public Transform laserSpawn2;
	public Transform laserSpawn3;
	public Transform laserSpawn4;
	public Transform flareSpawn;
	public AudioClip PickupSound;
	public AudioClip HostageSound;
	public AudioClip InvulnSound;
	public AudioClip CloakSound;
	public AudioClip LifeSound;
	public AudioClip KeySound;

	public HUDScript HUD;

	bool hasBlueKey = false;
	bool hasYellowKey = false;
	bool hasRedKey = false;
	bool hasQuad = false;
	bool hasConverter = false;
	bool hasAllmap = false;
	bool hasHeadlight = false;
	bool hasAfterburner = false;
	bool hasAmmoRack = false;
	bool hasVulcan = false;
	bool hasGauss = false;
	bool hasSpreadfire = false;
	bool hasHelix = false;
	bool hasPlasma = false;
	bool hasPhoenix = false;
	bool hasFusion = false;
	bool hasOmega = false;
	bool hasConc = false;
	bool hasFlash = false;
	bool hasHoming = false;
	bool hasGuide = false;
	bool hasProxbomb = false;
	bool hasSmartbomb = false;
	bool hasMerc = false;
	bool hasSmart = false;
	bool hasMega = false;
	bool hasEarthshaker = false;

	public Canvas UICanvas;

	int LifeCount = 3;
	int LaserLevel = 1;
	int Energy = 100;
	int Shields = 100;
	int VulcanAmmo = 0;
	int MaxVulcanAmmo = 12500;
	int ConcAmmo = 4;
	int FlashAmmo = 0;
	int HomingAmmo = 0;
	int GuideAmmo = 0;
	int ProxbombAmmo = 0;
	int SmartbombAmmo = 0;
	int MercAmmo = 0;
	int SmartAmmo = 0;
	int MegaAmmo = 0;
	int EarthshakerAmmo = 0;
	int OmegaAmmo = 0;

	string EquippedPrimary = "laser";
	string EquippedSecondary = "conc";

	private IEnumerator coroutine;

	ShipHandler sHand;

	Vector3 moveInput;
	Vector3 rotInput;

	bool powered = true;

	// Use this for initialization
	void Start () {
		sHand = GetComponent<ShipHandler> ();
		HUD.SetStats(LifeCount, Shields, Energy);
		//HUD.SetPanels(Energy);
	}
	
	// Update is called once per frame
	void Update () {
		//Receive input

		if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			if (LaserLevel < 6) {
				LaserLevel += 1;
			}
		}
		if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
			if (LaserLevel > 1) {
				LaserLevel -= 1;
			}
		}

		if (Input.GetKeyDown (KeyCode.Keypad8)) {
			if (Shields < 200) {
				BoostShields(1);
			}
			HUD.SetStats(LifeCount, Shields, Energy);
		}
		if (Input.GetKeyDown (KeyCode.Keypad2)) {
			if (Shields > 0) {
				Damage(10);
			}
			HUD.SetStats(LifeCount, Shields, Energy);
		}

		if (Input.GetKeyDown (KeyCode.Keypad6)) {
			if (Energy < 200) {
				BoostEnergy(1);
			}
			HUD.SetStats(LifeCount, Shields, Energy);
		}
		if (Input.GetKeyDown (KeyCode.Keypad4)) {
			if (Energy > 0) {
				DrainEnergy(1);
			}
			HUD.SetStats(LifeCount, Shields, Energy);
		}

		if (Input.GetMouseButtonDown (0)) {
			PrimaryFire (EquippedPrimary, LaserLevel);
			Debug.Log ("LMB PRESSED");
		}
		if (Input.GetMouseButtonDown (1)) {
			SecondaryFire (EquippedSecondary);
			Debug.Log ("RMB PRESSED");
		}

		if (Input.GetKey (KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else {
			Cursor.lockState = CursorLockMode.Locked;
		}

		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		float l = Input.GetAxisRaw("Lateral");

		float rh = Input.GetAxisRaw("Mouse X");
		float rv = -Input.GetAxisRaw("Mouse Y");
		float rl = Input.GetAxisRaw("Mouse Z");

		moveInput = new Vector3 (h, l, v);
		rotInput = new Vector3 (rv, rh, rl);

		if (Input.GetKeyDown (KeyCode.P)) {
			powered = !powered;
		}

		if (Input.GetKeyDown (KeyCode.F)) {
			FireFlare();
		}

		if (Input.GetKeyDown (KeyCode.H)) {
			if (hasHeadlight) {
				if (Headlight.GetComponent<Light>().intensity > 0) {
					Headlight.GetComponent<Light>().intensity = 0;
				} else {
					Headlight.GetComponent<Light>().intensity = 1;
				} 
			} else {
				Headlight.GetComponent<Light>().intensity = 0;
			}
		}

		if (Input.GetKeyDown (KeyCode.T)) {
			//ConvertEnergy();
		}

	}

	void FixedUpdate() {
		//Send input
		sHand.MoveInput(moveInput, rotInput, powered);
	}

	void OnTriggerEnter(Collider collision){
		if (collision.gameObject.tag == "ShieldBoost"){
			BoostShields (3);
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(0f,0f,80f,0f);
			Color end = new Color(0f,0f,80f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "EnergyBoost"){
			BoostEnergy (3);
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(80f,80f,80f,0f);
			Color end = new Color(80f,80f,0f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "key_red"){
			HUD.RedKeyOn ();
			hasRedKey = true;
			AudioSource.PlayClipAtPoint(KeySound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(80f,0f,0f,0f);
			Color end = new Color(80f,0f,0f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "key_yellow"){
			HUD.YellowKeyOn ();
			hasYellowKey = true;
			AudioSource.PlayClipAtPoint(KeySound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(80f,80f,0f,0f);
			Color end = new Color(80f,80f,0f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "key_blue"){
			HUD.BlueKeyOn ();
			hasBlueKey = true;
			AudioSource.PlayClipAtPoint(KeySound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(0f,0f,80f,0f);
			Color end = new Color(0f,0f,80f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "hostage"){
			AudioSource.PlayClipAtPoint(HostageSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(0f,0f,80f,0f);
			Color end = new Color(0f,0f,80f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "p_cloak"){
			//visible = false;
			AudioSource.PlayClipAtPoint(CloakSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(0f,0f,80f,0f);
			Color end = new Color(0f,0f,80f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "p_invuln"){
			//killable = false;
			AudioSource.PlayClipAtPoint(InvulnSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(0f,0f,80f,0f);
			Color end = new Color(0f,0f,80f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "p_life"){
			LifeCount += 1;
			AudioSource.PlayClipAtPoint(LifeSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(0f,0f,80f,0f);
			Color end = new Color(0f,0f,80f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
			HUD.SetStats(LifeCount, Shields, Energy);
		}
		if (collision.gameObject.tag == "p_headlight"){
			hasHeadlight = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "p_converter"){
			hasConverter = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "p_ammorack"){
			hasAmmoRack = true;
			MaxVulcanAmmo = 25000;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "p_allmap"){
			hasAllmap = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "p_afterburner"){
			hasAfterburner = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		//SECONDARY PICKUPS
		if (collision.gameObject.tag == "m_conc1"){
			if (hasAmmoRack) {
				if (ConcAmmo + 1 > 40) {
					ConcAmmo = 40;
				} else {
					ConcAmmo += 1;
				}
			} else {
				if (ConcAmmo + 1 > 20) {
					ConcAmmo = 20;
				} else {
					ConcAmmo += 1;
				}
			}
			hasConc = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_conc4"){
			if (hasAmmoRack) {
				if (ConcAmmo + 4 > 40) {
					ConcAmmo = 40;
				} else {
					ConcAmmo += 4;
				}
			} else {
				if (ConcAmmo + 4 > 20) {
					ConcAmmo = 20;
				} else {
					ConcAmmo += 4;
				}
			}
			hasConc = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_flash1"){
			if (hasAmmoRack) {
				if (FlashAmmo + 1 > 40) {
					FlashAmmo = 40;
				} else {
					FlashAmmo += 1;
				}
			} else {
				if (FlashAmmo + 1 > 20) {
					FlashAmmo = 20;
				} else {
					FlashAmmo += 1;
				}
			}
			hasFlash = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_flash4"){
			if (hasAmmoRack) {
				if (FlashAmmo + 4 > 40) {
					FlashAmmo = 40;
				} else {
					FlashAmmo += 4;
				}
			} else {
				if (FlashAmmo + 4 > 20) {
					FlashAmmo = 20;
				} else {
					FlashAmmo += 4;
				}
			}
			hasFlash = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_homing1"){
			if (hasAmmoRack) {
				if (HomingAmmo + 1 > 40) {
					HomingAmmo = 40;
				} else {
					HomingAmmo += 1;
				}
			} else {
				if (HomingAmmo + 1 > 20) {
					HomingAmmo = 20;
				} else {
					HomingAmmo += 1;
				}
			}
			hasHoming = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_homing4"){
			if (hasAmmoRack) {
				if (HomingAmmo + 4 > 40) {
					HomingAmmo = 40;
				} else {
					HomingAmmo += 4;
				}
			} else {
				if (HomingAmmo + 4 > 20) {
					HomingAmmo = 20;
				} else {
					HomingAmmo += 4;
				}
			}
			hasHoming = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_guide1"){
			if (hasAmmoRack) {
				if (GuideAmmo + 1 > 40) {
					GuideAmmo = 40;
				} else {
					GuideAmmo += 1;
				}
			} else {
				if (GuideAmmo + 1 > 20) {
					GuideAmmo = 20;
				} else {
					GuideAmmo += 1;
				}
			}
			hasGuide = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_guide4"){
			if (hasAmmoRack) {
				if (GuideAmmo + 4 > 40) {
					GuideAmmo = 40;
				} else {
					GuideAmmo += 4;
				}
			} else {
				if (GuideAmmo + 4 > 20) {
					GuideAmmo = 20;
				} else {
					GuideAmmo += 4;
				}
			}
			hasGuide = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_proxbox"){
			if (hasAmmoRack) {
				if (ProxbombAmmo + 4 > 30) {
					ProxbombAmmo = 30;
				} else {
					ProxbombAmmo += 4;
				}
			} else {
				if (ProxbombAmmo + 4 > 15) {
					ProxbombAmmo = 15;
				} else {
					ProxbombAmmo += 4;
				}
			}
			hasProxbomb = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_proxbomb"){
			collision.gameObject.GetComponent<MineScript>().Detonate();
			Debug.Log("PROX BOMB");
		}
		if (collision.gameObject.tag == "m_smartbomb"){
			collision.gameObject.GetComponent<MineScript>().Detonate();
			Debug.Log("SMART BOMB");
		}
		if (collision.gameObject.tag == "m_smartbox"){
			if (hasAmmoRack) {
				if (SmartbombAmmo + 4 > 30) {
					SmartbombAmmo = 30;
				} else {
					SmartbombAmmo += 4;
				}
			} else {
				if (SmartbombAmmo + 4 > 15) {
					SmartbombAmmo = 15;
				} else {
					SmartbombAmmo += 4;
				}
			}
			hasSmartbomb = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_merc1"){
			if (hasAmmoRack) {
				if (MercAmmo + 1 > 40) {
					MercAmmo = 40;
				} else {
					MercAmmo += 1;
				}
			} else {
				if (MercAmmo + 1 > 20) {
					MercAmmo = 20;
				} else {
					MercAmmo += 1;
				}
			}
			hasMerc = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_merc4"){
			if (hasAmmoRack) {
				if (MercAmmo + 4 > 40) {
					MercAmmo = 40;
				} else {
					MercAmmo += 4;
				}
			} else {
				if (MercAmmo + 4 > 20) {
					MercAmmo = 20;
				} else {
					MercAmmo += 4;
				}
			}
			hasMerc = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_mega"){
			if (hasAmmoRack) {
				if (MegaAmmo + 1 > 10) {
					MegaAmmo = 10;
				} else {
					MegaAmmo += 1;
				}
			} else {
				if (MegaAmmo + 1 > 5) {
					MegaAmmo = 5;
				} else {
					MegaAmmo += 1;
				}
			}
			hasMega = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_smart"){
			if (hasAmmoRack) {
				if (SmartAmmo + 1 > 10) {
					SmartAmmo = 10;
				} else {
					SmartAmmo += 1;
				}
			} else {
				if (SmartAmmo + 1 > 5) {
					SmartAmmo = 5;
				} else {
					SmartAmmo += 1;
				}
			}
			hasSmart = true;
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}
		if (collision.gameObject.tag == "m_earthshaker"){
			if (hasAmmoRack) {
				if (EarthshakerAmmo + 1 > 10) {
					EarthshakerAmmo = 10;
				} else {
					EarthshakerAmmo += 1;
				}
			} else {
				if (EarthshakerAmmo + 1 > 5) {
					EarthshakerAmmo = 5;
				} else {
					EarthshakerAmmo += 1;
				}
			}
			hasEarthshaker = true;
			AudioSource.PlayClipAtPoint(PickupSound, transform.position);
			Destroy (collision.gameObject);
			Color start = new Color(255f,255f,255f,0f);
			Color end = new Color(255f,255f,255f,.8f);
			coroutine = FlashColor (start, end, .1f);
			StartCoroutine (coroutine);
		}

		//WEAPON PICKUPS
		if (collision.gameObject.tag == "w_laser") {
			if (Energy < 200 && LaserLevel < 4) {
				ImproveLaser ("laser");
				BoostEnergy (3);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && LaserLevel < 4) {
				ImproveLaser ("laser");
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy < 200 && LaserLevel >= 4) {
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				BoostEnergy (3);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && LaserLevel >= 4) {
				//Do nothing. Display message eventually.
			} else {
				Debug.Log ("ERROR: This shouldn't be possible.");
			}
		}
		if (collision.gameObject.tag == "w_super"){
			if (Energy < 200 && LaserLevel < 6) {
				ImproveLaser ("super");
				BoostEnergy (3);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && LaserLevel < 6) {
				ImproveLaser ("super");
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy < 200 && LaserLevel >= 6) {
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				BoostEnergy (3);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && LaserLevel >= 6) {
				//Do nothing. Display message eventually.
			} else {
				Debug.Log ("ERROR: This shouldn't be possible.");
			}
		}
		if (collision.gameObject.tag == "w_quad"){
			if (Energy < 200 && !hasQuad) {
				hasQuad = true;
				laserSpawn3.GetComponent<Renderer> ().enabled = true;
				laserSpawn4.GetComponent<Renderer> ().enabled = true;
				BoostEnergy (3);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && !hasQuad) {
				hasQuad = true;
				laserSpawn3.GetComponent<Renderer> ().enabled = true;
				laserSpawn4.GetComponent<Renderer> ().enabled = true;
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy < 200 && hasQuad) {
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				BoostEnergy (3);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && hasQuad) {
				//Do nothing. Display message eventually.
			} else {
				Debug.Log ("ERROR: This shouldn't be possible.");
			}
		}
		if (collision.gameObject.tag == "w_vulcan"){
			if (VulcanAmmo < MaxVulcanAmmo && !hasVulcan) {
				hasVulcan = true;
				BoostAmmo (500);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (VulcanAmmo >= MaxVulcanAmmo && !hasVulcan) {
				hasVulcan = true;
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (VulcanAmmo < MaxVulcanAmmo && hasVulcan) {
				BoostAmmo (500);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (VulcanAmmo >= MaxVulcanAmmo && hasVulcan) {
				//Do nothing. Display message eventually.
			} else {
				Debug.Log ("ERROR: This shouldn't be possible.");
			}
		}
		if (collision.gameObject.tag == "w_vulcanammo"){
			if (VulcanAmmo < MaxVulcanAmmo) {
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
				BoostAmmo (1000);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
			} else {
				//Reject
			}
		}
		if (collision.gameObject.tag == "w_gauss"){
			if (VulcanAmmo < MaxVulcanAmmo && !hasGauss) {
				hasGauss = true;
				BoostAmmo (500);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (VulcanAmmo >= MaxVulcanAmmo && !hasGauss) {
				hasGauss = true;
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (VulcanAmmo < MaxVulcanAmmo && hasGauss) {
				BoostAmmo (500);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (VulcanAmmo >= MaxVulcanAmmo && hasGauss) {
				//Do nothing. Display message eventually.
			} else {
				Debug.Log ("ERROR: This shouldn't be possible.");
			}
		}
		if (collision.gameObject.tag == "w_spreadfire"){
			if (Energy < 200 && !hasSpreadfire) {
				hasSpreadfire = true;
				BoostEnergy (3);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && !hasSpreadfire) {
				hasSpreadfire = true;
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy < 200 && hasSpreadfire) {
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				BoostEnergy (3);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && hasSpreadfire) {
				//Do nothing. Display message eventually.
			} else {
				Debug.Log ("ERROR: This shouldn't be possible.");
			}
		}
		if (collision.gameObject.tag == "w_helix"){
			if (Energy < 200 && !hasHelix) {
				hasHelix = true;
				BoostEnergy (3);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && !hasHelix) {
				hasHelix = true;
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy < 200 && hasHelix) {
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				BoostEnergy (3);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && hasHelix) {
				//Do nothing. Display message eventually.
			} else {
				Debug.Log ("ERROR: This shouldn't be possible.");
			}
		}
		if (collision.gameObject.tag == "w_plasma"){
			if (Energy < 200 && !hasPlasma) {
				hasPlasma = true;
				BoostEnergy (3);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && !hasPlasma) {
				hasPlasma = true;
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy < 200 && hasPlasma) {
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				BoostEnergy (3);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && hasPlasma) {
				//Do nothing. Display message eventually.
			} else {
				Debug.Log ("ERROR: This shouldn't be possible.");
			}
		}
		if (collision.gameObject.tag == "w_phoenix"){
			if (Energy < 200 && !hasPhoenix) {
				hasPhoenix = true;
				BoostEnergy (3);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && !hasPhoenix) {
				hasPhoenix = true;
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy < 200 && hasPhoenix) {
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				BoostEnergy (3);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && hasPhoenix) {
				//Do nothing. Display message eventually.
			} else {
				Debug.Log ("ERROR: This shouldn't be possible.");
			}
		}
		if (collision.gameObject.tag == "w_fusion"){
			if (Energy < 200 && !hasFusion) {
				hasFusion = true;
				BoostEnergy (3);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && !hasFusion) {
				hasFusion = true;
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy < 200 && hasFusion) {
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				BoostEnergy (3);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && hasFusion) {
				//Do nothing. Display message eventually.
			} else {
				Debug.Log ("ERROR: This shouldn't be possible.");
			}
		}
		if (collision.gameObject.tag == "w_omega"){
			if (Energy < 200 && !hasOmega) {
				hasOmega = true;
				BoostEnergy (3);
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && !hasOmega) {
				hasOmega = true;
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy < 200 && hasOmega) {
				coroutine = FlashColor (new Color (255f, 255f, 255f, 0f), new Color (255f, 255f, 255f, .8f), .1f);
				StartCoroutine (coroutine);
				BoostEnergy (3);
				AudioSource.PlayClipAtPoint (PickupSound, transform.position);
				Destroy (collision.gameObject);
			} else if (Energy >= 200 && hasOmega) {
				//Do nothing. Display message eventually.
			} else {
				Debug.Log ("ERROR: This shouldn't be possible.");
			}
		}
	}

	IEnumerator FlashColor(Color start, Color end, float overTime)
	{
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			UICanvas.GetComponent<Image>().color = Color.Lerp(start, end, (Time.time - startTime)/overTime);
			yield return null;
		}
		startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			UICanvas.GetComponent<Image>().color = Color.Lerp(end, start, (Time.time - startTime)/overTime);
			yield return null;
		}
		UICanvas.GetComponent<Image>().color = start;
	}

	public void PrimaryFire(string Weapon, int LaserLevel)
	{
		if (Weapon == "laser") {
			var laser1 = new GameObject();
			var laser2 = new GameObject();
			var laser3 = new GameObject();
			var laser4 = new GameObject();
			if (LaserLevel == 1) {
				laser1 = (GameObject)Instantiate (
					Laser1,
					laserSpawn1.position,
					laserSpawn1.rotation);
				laser2 = (GameObject)Instantiate (
					Laser1,
					laserSpawn2.position,
					laserSpawn2.rotation);
				if (hasQuad) {
					laser3 = (GameObject)Instantiate (
						Laser1,
						laserSpawn3.position,
						laserSpawn3.rotation);
					laser4 = (GameObject)Instantiate (
						Laser1,
						laserSpawn4.position,
						laserSpawn4.rotation);
				}
			}
			if (LaserLevel == 2) {
				laser1 = (GameObject)Instantiate (
					Laser2,
					laserSpawn1.position,
					laserSpawn1.rotation);
				laser2 = (GameObject)Instantiate (
					Laser2,
					laserSpawn2.position,
					laserSpawn2.rotation);
				if (hasQuad) {
					laser3 = (GameObject)Instantiate (
						Laser2,
						laserSpawn3.position,
						laserSpawn3.rotation);
					laser4 = (GameObject)Instantiate (
						Laser2,
						laserSpawn4.position,
						laserSpawn4.rotation);
				}
			}
			if (LaserLevel == 3) {
				laser1 = (GameObject)Instantiate (
					Laser3,
					laserSpawn1.position,
					laserSpawn1.rotation);
				laser2 = (GameObject)Instantiate (
					Laser3,
					laserSpawn2.position,
					laserSpawn2.rotation);
				if (hasQuad) {
					laser3 = (GameObject)Instantiate (
						Laser3,
						laserSpawn3.position,
						laserSpawn3.rotation);
					laser4 = (GameObject)Instantiate (
						Laser3,
						laserSpawn4.position,
						laserSpawn4.rotation);
				}
			}
			if (LaserLevel == 4) {
				laser1 = (GameObject)Instantiate (
					Laser4,
					laserSpawn1.position,
					laserSpawn1.rotation);
				laser2 = (GameObject)Instantiate (
					Laser4,
					laserSpawn2.position,
					laserSpawn2.rotation);
				if (hasQuad) {
					laser3 = (GameObject)Instantiate (
						Laser4,
						laserSpawn3.position,
						laserSpawn3.rotation);
					laser4 = (GameObject)Instantiate (
						Laser4,
						laserSpawn4.position,
						laserSpawn4.rotation);
				}
			}
			if (LaserLevel == 5) {
				laser1 = (GameObject)Instantiate (
					Laser5,
					laserSpawn1.position,
					laserSpawn1.rotation);
				laser2 = (GameObject)Instantiate (
					Laser5,
					laserSpawn2.position,
					laserSpawn2.rotation);
				if (hasQuad) {
					laser3 = (GameObject)Instantiate (
						Laser5,
						laserSpawn3.position,
						laserSpawn3.rotation);
					laser4 = (GameObject)Instantiate (
						Laser5,
						laserSpawn4.position,
						laserSpawn4.rotation);
				}
			}
			if (LaserLevel == 6) {
				laser1 = (GameObject)Instantiate (
					Laser6,
					laserSpawn1.position,
					laserSpawn1.rotation);
				laser2 = (GameObject)Instantiate (
					Laser6,
					laserSpawn2.position,
					laserSpawn2.rotation);
				if (hasQuad) {
					laser3 = (GameObject)Instantiate (
						Laser6,
						laserSpawn3.position,
						laserSpawn3.rotation);
					laser4 = (GameObject)Instantiate (
						Laser6,
						laserSpawn4.position,
						laserSpawn4.rotation);
				}
			}

			//Add velocity to the projectile
			laser1.GetComponent<Rigidbody> ().velocity = laser1.transform.up * 30;
			laser2.GetComponent<Rigidbody> ().velocity = laser2.transform.up * 30;

			//Destroy projectile after 2 seconds
			Destroy (laser1, 2.0f);
			Destroy (laser2, 2.0f);

			if (hasQuad) {
				laser3.GetComponent<Rigidbody> ().velocity = laser3.transform.up * 30;
				laser4.GetComponent<Rigidbody> ().velocity = laser4.transform.up * 30;
				Destroy (laser3, 2.0f);
				Destroy (laser4, 2.0f);
			}
		}
	}

	public void SecondaryFire(string Weapon)
	{
		Debug.Log ("Missile Away!");
	}

	public void FireFlare(){
		//Fire a flare.
		if (DrainEnergy (1)) {
			var flare = new GameObject ();
			flare = (GameObject)Instantiate (
				Flare,
				flareSpawn.position,
				flareSpawn.rotation);
			flare.GetComponent<Rigidbody> ().velocity = flare.transform.up * 30;
		} else {
			//Not enough energy for a flare.
		}
	}

	void ImproveLaser(string s)
	{
		if (s == "laser") {
			if (LaserLevel < 4) {
				LaserLevel += 1;
			}
		} else if (s == "super") {
			if (LaserLevel < 6) {
				LaserLevel += 1;
			}
		} else {
			Debug.Log ("That's not supposed to happen....");
		}
	}

	public void BoostShields(int amount){
		if (Shields == 200) {
			// Nothing
		} else {
			Shields += amount * DIFFICULTY_MULTIPLIER;
			if (Shields > 200) {
				Shields = 200;
			}
		}
		HUD.SetStats(LifeCount, Shields, Energy);
	}

	public void BoostEnergy(int amount){
		if (Energy == 200) {
			//Nothing
		} else {
			Energy += amount * DIFFICULTY_MULTIPLIER;
			if (Energy > 200) {
				Energy = 200;
			}
		}
		HUD.SetStats(LifeCount, Shields, Energy);
	}

	public void BoostAmmo(int amount){
		if (hasAmmoRack) {
			if (VulcanAmmo == 25000) {
				//Play a reject sound?
			} else {
				VulcanAmmo += amount * DIFFICULTY_MULTIPLIER;
				if (VulcanAmmo > 25000) {
					VulcanAmmo = 25000;
				}
			}
		} else {
			if (VulcanAmmo == 12500) {
				//Play a reject sound?
			} else {
				VulcanAmmo += amount * DIFFICULTY_MULTIPLIER;
				if (VulcanAmmo > 12500) {
					VulcanAmmo = 12500;
				}
			}
		}
		HUD.SetStats(LifeCount, Shields, Energy);
	}

	public bool DrainEnergy(int amount){
		//PLAY SOUND HERE
		if (Energy == 0) {
			HUD.SetStats(LifeCount, Shields, Energy);
			HUD.SetPanels(Energy);
			return false;
		} else if (Energy - (amount) < 0) {
			Energy = 0;
			HUD.SetStats(LifeCount, Shields, Energy);
			HUD.SetPanels(Energy);
			return true;
		} else {
			Energy -= amount;
			HUD.SetStats(LifeCount, Shields, Energy);
			HUD.SetPanels(Energy);
			return true;
		}

	}

	public void Damage(int amount){
		Shields -= amount;
		HUD.SetStats(LifeCount, Shields, Energy);
		if (Shields <= 0){
			PlayerDeath();
		}
	}

	void PlayerDeath(){
		Debug.Log("PLAYER DEATH");
		MainCam.transform.position = new Vector3(-3f, 1.5f, 3f);
		MainCam.transform.rotation = new Quaternion(20f, 130f, 0f, 1);
		powered = false;
		float torque = 10;
		float turn = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody>().AddTorque(transform.up * torque * turn);
	}
}
