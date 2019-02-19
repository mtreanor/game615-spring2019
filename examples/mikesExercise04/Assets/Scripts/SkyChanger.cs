using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyChanger : MonoBehaviour
{
	public Color color1;
	public Color color2;

	float colorChangeVel = 0.2f;
	float cVal = 0;

	int colorChangeCount = 0;

	public Camera camera;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//Set the background color to be somewhere between color1 and color2
		camera.backgroundColor = Color.Lerp(color1, color2, cVal);

		//Change
		cVal += colorChangeVel * Time.deltaTime;
		if (cVal > 1 || cVal < 0) {
			colorChangeVel *= -1;

			colorChangeCount++;
			if (colorChangeCount % 2 == 0) {
				color2 = new Color(Random.value, Random.value, Random.value);
			} else {
				color1 = new Color(Random.value, Random.value, Random.value);
			}
		}
	}
}
