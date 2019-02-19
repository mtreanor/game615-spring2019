using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungryCatScript : MonoBehaviour
{
	int score = 0;

	public Image scoreMeter;
	public float levelTime = 20f;

	public GameObject eatEffectPrefab;

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
		if (other.gameObject.CompareTag("Drop")) {
			GameObject effect = Instantiate(eatEffectPrefab, other.gameObject.transform.position, Quaternion.identity);
			Destroy(effect, 5);


			Destroy(other.gameObject);
			score++;

			scoreMeter.fillAmount = score / 100.0f;
		}
	}
}
