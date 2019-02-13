using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainScript : MonoBehaviour
{
	public Text scoreText; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("DropDestroyer")) {
			Destroy(gameObject);
		}
	}
}
