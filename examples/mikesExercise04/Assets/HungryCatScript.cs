using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungryCatScript : MonoBehaviour
{
	int score = 0;

	public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
		scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Drop")) {
			Destroy(other.gameObject);
			score++;
			scoreText.text = score.ToString();
		}
	}
}
