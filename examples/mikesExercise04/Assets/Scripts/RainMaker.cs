using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMaker : MonoBehaviour
{
	float dropRate;
	float dropTimer;

	public GameObject dropPrefab;

	// Start is called before the first frame update
	void Start()
	{
		dropRate = 0.4f;
		dropTimer = dropRate;
	}

	// Update is called once per frame
	void Update()
	{
		//This is the "timer pattern":
		//		1. decrement a timer float by the amount of time that has passed
		//		2. if the timer is less than zero, we know that the "rate" amount of time has passed
		//		3. in the if statement, do whatever we are trying to do
		//		4. reset the timer to the rate
		dropTimer -= Time.deltaTime;
		if (dropTimer < 0) {
			//Make a drop using the function defined below
			//We do this to keep our code cleaner. It is 'cleaner' because the make drop functionality
			//is encapsulated in an appropriately names function, rather than being in the middle
			//of the Update function.
			makeDrop();

			//Reset the timer (step 4 above)
			dropTimer = dropRate;
		}
	}
	void makeDrop()
	{
		//Get random x and z positions
		float x = transform.position.x + Random.Range(-transform.localScale.x / 2 * 10, transform.localScale.x / 2 * 10);
		float z = transform.position.z + Random.Range(-transform.localScale.z / 2 * 10, transform.localScale.z / 2 * 10);
		Vector3 pos = new Vector3(x, transform.position.y + 5, z);

		//Create a "drop" at that position, store the object in the local variable 'drop'
		//NOTE: The Instantiate function adds a prefab to the game, and 'returns' a reference
		//		  to the object.
		GameObject drop = Instantiate(dropPrefab, pos, Quaternion.identity);
		//Give the drop a random color
		Material dropMat = drop.GetComponent<Renderer>().material;
		dropMat.color = new Color(Random.value, Random.value, Random.value);

		//Rotate the drop a random amount and then shoot it forward. We are doing this in order to 
		//	give the drops some movement so they aren't just going to fall straight down.
		drop.transform.Rotate(0, Random.Range(0, 360), 0, Space.World);
		Rigidbody dropRB = drop.GetComponent<Rigidbody>();
		dropRB.AddForce(drop.gameObject.transform.forward * Random.Range(50, 200));
	}

}
