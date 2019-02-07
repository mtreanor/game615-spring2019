using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballScript : MonoBehaviour
{
	public float speed = 5f;
	GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
		//Get a reference to the player game object.
		//We weren't able to manual assign the value to playerObj in the Unity editor
		//because the snowball didn't exist before the game was running.
		//NOTE: There are other ways to accomplish this same thing, and this example
		//is meant to just show how GameObject.Find() works.
		playerObj = GameObject.Find("Player");

		//Create a random number between 0 and 1.5
		float randomScale = Random.value * 1.5f;
		//Overwrite the transform's scale to be randomScale for x, y, and z
		transform.localScale = new Vector3(randomScale, randomScale, randomScale);

		//Give the snowball a random color, but modifying the game object's
		//material's color value.
		Renderer renderer = gameObject.GetComponent<Renderer>();
		//This creates a variable that holds a color object with random rgb values
		Color randomColor = new Color(Random.value, Random.value, Random.value);
		//Assign the value (this is the line that actually changes the color)
		renderer.material.color = randomColor;
    }

    // Update is called once per frame
    void Update()
    {
		//Move forward
		//We multiple by Time.deltaTime to ensure continuous movement
		//We can control the speed by modifying the speed variable
		transform.position = transform.position + transform.forward * speed * Time.deltaTime;

		//When the player presses Z "Attack" (i.e. start moving toward the player)
		//LookAt rotates the transform so that the forward vector is facing the player
		if (Input.GetKeyDown(KeyCode.Z)) {
			//The code below demonstrates how to compute a vector from the snowball to 
			//the player.
			//Vector3 vectorToTarget = playerObj.transform.position - transform.position;
			//vectorToTarget = vectorToTarget.normalized;

			//There are situations where doing something like the above is useful, but in this
			//case, it is easier to just have the snowball 'LookAt' the player
			transform.LookAt(playerObj.transform);
		}

		//When the player presses C, "Go Nuts"
		//In other words, rotate a random amount in the x, y, and z directions. 
		//Once this happens, the forward movement of the snowball will send it off in 
		//a random direction.
		if (Input.GetKeyDown(KeyCode.C)) {
			transform.Rotate(Random.value * 360, Random.value * 360, Random.value * 360);
		}
	}
}
