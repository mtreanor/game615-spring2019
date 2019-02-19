using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VacuumScript : MonoBehaviour
{
	int score = 0;
	float speed = 2f;
	bool move = false;

	float decisionTimer = 3f;
	float decisionRate = 3f;

	public Light onLight;

	public Image vacuumScoreMeter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		decisionTimer -= Time.deltaTime;
		if (decisionTimer < 0) {
			//turn off the light
			onLight.enabled = false;
			move = false;

			decisionRate = Random.Range(1, 5);
			decisionTimer = decisionRate;
			float random = Random.value;
			if (random < 0.5f) {
				onLight.enabled = true;
				move = true;
				transform.Rotate(0, Random.Range(0,360), 0, Space.World);
			}
		}


		if (move) {
			transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Drop")) {
			Destroy(other.gameObject);
			score++;
			vacuumScoreMeter.fillAmount = score / 100f;
		}

		if (other.gameObject.CompareTag("Border")) {
			transform.Rotate(0, 180, 0, Space.World);
		}
	}
}
