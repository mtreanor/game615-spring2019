using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	float moveSpeed = 5f;
	float rotateSpeed = 40f;

	public bool moveTowardFork = false;
	public float launchForce = 0;
	float launchChargeRate = 20;

	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		//-- MOVEMENT CONTROLS --
		//hAxis will be a value between -1 and 1 based on whether left or right are pressed
		float hAxis = Input.GetAxis("Horizontal");
		//vAxis will be a value between -1 and 1 based on whether up or down are pressed
		float vAxis = Input.GetAxis("Vertical");

		//Rotate on the y-axis based on hAxis and rotateSpeed (scale amount by deltaTime)
		transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0);
		//Move forward based on vAxis and moveSpeed (scale amount by deltaTime)
		transform.Translate(transform.forward * vAxis * moveSpeed * Time.deltaTime, Space.World);



		//-- SPECIAL POWER CONTROLS --
		if (Input.GetKeyDown(KeyCode.Space)) {
			//Each drop will change its behavior based on this boolean (in RainScript)
			moveTowardFork = true;
		}
		if (Input.GetKey(KeyCode.Space)) {
			//"Charge" the launch force value by making it grow as space is held (scaled by deltaTime)
			launchForce += launchChargeRate * Time.deltaTime;
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			//When space is released, change the boolean that the drops are basing their behavior
			//	on to false. Also, 
			moveTowardFork = false;
		}
	}
}
