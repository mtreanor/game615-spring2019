using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
	public GameObject snowballPrefab;

	float speed = 5f;
	float rotateSpeed = 200;
	//public GameObject target;
	bool shouldRotate = false;

	// Start is called before the first frame update
	void Start()
	{
		//This line is just to demonstrate how we can print out the values of variables to
		//the console. This is useful for debugging because it allows us to see the values
		//of variables while the game is running. This example isn't particularly useful, 
		//but it does show how to use Debug.Log().
		Debug.Log(transform.forward);
	}

	// Update is called once per frame
	void Update()
	{
		if (shouldRotate) {
			transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
		}

		//As long as the player is holding x, create snowball prefabs at the current position
		//of the player, and facing in the same direction as the player.
		if (Input.GetKey(KeyCode.X)) {
			GameObject snowball = Instantiate(snowballPrefab, transform.position, transform.rotation);
			//This will tell Unity to destroy the snowball in 20 seconds.
			Destroy(snowball, 20f);
		}
	}

	//This function is called by the 'spin' button when it is clicked. Look at the button and
	//review how this is set.
	public void ChangeShouldRotate() {
		//Set the shouldRotate boolean variable to be the opposite of what it was
		shouldRotate = !shouldRotate;
	}
}
