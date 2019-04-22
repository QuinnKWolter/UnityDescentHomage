using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHandler : MonoBehaviour {

	public Rigidbody rb;

	Vector3 posInput;
	Vector3 rotInput;

	bool powered = true;
	float speed = 150;

	public void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	public void MoveInput (Vector3 move, Vector3 rot, bool power)
	{
		posInput = move;
		rotInput = rot;
		powered = power;

		Move ();
	}

	void Move()
	{
		if (powered) {
			speed = 250;
			rb.drag = 10;
		} else {
			speed = 0;
			rb.drag = 0;
		}
		rb.AddRelativeForce(posInput * speed);
		rb.AddRelativeTorque (rotInput * 5);
	}


}
