using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyChanger : MonoBehaviour
{
	public Color color1;
	public Color color2;

	float colorChangeVel = 1f;
	float cVal = 0;

	public Camera camera;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		camera.backgroundColor = Color.Lerp(color1, color2, cVal);

		cVal += colorChangeVel * Time.deltaTime;
		if (cVal > 1 || cVal < 0) {
			colorChangeVel *= -1;
		}
	}
}
