using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballScript : MonoBehaviour
{
	GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
		//Get a reference to a "GameManager" script.
		//That script is designed to take care of things that aren't specific to any particular
		//object in the scene.
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Stadium")) {
			//Tell the game manager to increase the score
			gameManager.increaseScore();
		}
	}
}
