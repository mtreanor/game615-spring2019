using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchScript : MonoBehaviour {
	//This variable will store how much force we will launch the shape forward with
	//NOTE: Because I am 'declaring' the variable here (outside of any function 
	//		definition), we will be able to access it from any where in this file.
	float launchForce = 0;

	//These variables are used to store the position and rotation of the shape right when
	//the game starts. We will reset the transform to these values if the gameObject
	//collides with a big invisible trigger at the bottom of the scene.
	//See the OnTriggerEnter function below.
	Vector3 startPosition;
	Quaternion startRotation;

	//We will set this to true in the OnTriggerEnter function if the gameObject
	//goes through the ring.
	bool playerWon = false;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		//If the player releases the space bar function, launch the shape!
		//Description below:
		if (Input.GetKeyUp(KeyCode.Space)) {
			//1. Get a reference to the Rigidbody on this GameObject
			//	 Store it in a variable called rb. rb contains all of the 
			//   variables and functions that are part of the Rigidbody component
			//   that we added in the Unity inspector (right side of the screen)
			Rigidbody rb = gameObject.GetComponent<Rigidbody>();
			//2. Enable gravity (NOTE: We previously unchecked this in the inspector).
			//	 By setting rb's useGravity variable to true, we are "rechecking" the
			//	 box via code.
			rb.useGravity = true;
			//3. Shoot the cube in the forward direction scaled by te force value
			//	 Don't worry too much about the math for now.
			rb.AddForce(transform.forward * launchForce);
		}

		//If the player is pressing space, increase the launchForce variable by 3.
		//Otherwise, decrease it by 3.
		if (Input.GetKey(KeyCode.Space)) {
			launchForce = launchForce + 3f;
		} else {
			//Only decrease the launch force if it is greater than 0
			//This prevents the variable from going negative.
			if (launchForce > 0) {
				launchForce = launchForce - 3f;
			}
		}
	}

	//This function is called by Unity when gameObjects with the following configuration
	//collide with oneanother:
	//
	//Recipe for making Unity call the OnTriggerEnter function:
	//	1. At least one of the objects needs to have a Rigidbody component
	//	2. At least one of the objects needs to have a collider marked as a trigger
	//	3. Any Component with an "OnTriggerEnter" function will have their function called
	void OnTriggerEnter(Collider other)
	{
		//Check to see if the trigger we collided with is the "InvisibleTriggerCube"
		//that is in the middle of the Ring gameObject.
		if (other.gameObject.name == "InvisibleTriggerCube") {
			playerWon = true;//In OnGUI we will print you win when this is set to true
		}

		if (other.gameObject.name == "InvisibleRespawnCube") {
			//If we collide with the big invisible cube at the bottom of the scene
			//reposition the shape, turn off gravity again, and stop its
			//momentum (otherwise it will keep moving at the velocity it was flying at)
			Rigidbody rb = gameObject.GetComponent<Rigidbody>();
			rb.useGravity = false;
			rb.velocity = Vector3.zero;//This sets x, y, and z velocity to zero
			rb.angularVelocity = Vector3.zero;

			//Reposition the shape to the startPosition we stored in the Start function
			gameObject.transform.position = startPosition;
			//Reorient the shape to the startRotation we stored in the Start function
			gameObject.transform.rotation = startRotation;
		}
	}

	//This function is called once per frame and it draws User Interface elements.
	//Don't worry too much about this right now, but it is useful to get our force
	//printing to the screen.
	private void OnGUI()
	{
		GUI.Label(new Rect(50, 50, 200, 200), launchForce.ToString());

		if (playerWon) {
			GUI.Label(new Rect(250, 250, 200, 200), "You win!");
		}
	}
}
