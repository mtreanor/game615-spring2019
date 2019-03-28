using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	public GameObject prefabObj;

	public Image fadePanelImg;

	public GameObject cylinderTalkPanel;
	public TMP_Text cylinderText;

	public GameObject cubeTalkPanel;
	public TMP_Text cubeText;
	GameObject activeCube;
	// Start is called before the first frame update
	void Start()
    {
		//Start the fadeIn coroutine
		//Once the fade has finished, we will start the scene
		StartCoroutine(fadeIn());
	}

    // Update is called once per frame
    void Update()
    {
		//If the cube panel is active, and there is a cube on the screen, update the position
		//of the talk panel to be right above where the active cube is
        if (cubeTalkPanel.activeSelf && activeCube != null) {
			Vector3 cubeTalkPanelPos = Camera.main.WorldToScreenPoint(activeCube.transform.position + Vector3.up * 2f);
			cubeTalkPanel.transform.position = cubeTalkPanelPos;
		}
	}

	IEnumerator fadeIn()
	{
		//In this function, we will slowly lower the 'alpha' (transparency) value of the giant
		//image's color to achieve a 'fade in' effect (fading from black to clear).
		while (fadePanelImg.color.a > 0) {
			float newAlpha = fadePanelImg.color.a - 0.5f * Time.deltaTime;
			fadePanelImg.color = new Color(0, 0, 0, newAlpha);

			//This line will end the function for this round of Unity's update cycle (at time=n)
			yield return null;
			//This is where the Unity update cycle will enter at time=n+1
		}

		//Once we're done fading, we can set the fade panel to inactive. We do this to avoid
		//the panel 'catching' clicks. Sometimes an invisible panel can make it so our click
		//events aren't recognized as expected.
		fadePanelImg.gameObject.SetActive(false);

		//Start the drop the cube scene
		StartCoroutine(cylinderAndCubeScene());
	}

	IEnumerator cylinderAndCubeScene()
	{
		//Typically, we would never want to use a "while (true)" loop, but because we are
		//using it in a coroutine, the loop will "pause" at the "yield return new WaitForSeconds"
		//line of code, and start again after the specified amount of seconds has gone by.
		while (true) {
			//Create a cube
			activeCube = Instantiate(prefabObj, transform.position, Quaternion.identity);

			yield return new WaitForSeconds(0.5f);

			cylinderText.text = "Hey";
			cylinderTalkPanel.SetActive(true);

			yield return new WaitForSeconds(1);

			cubeText.text = "Sup";
			cubeTalkPanel.SetActive(true);

			yield return new WaitForSeconds(1);

			cylinderTalkPanel.SetActive(false);

			yield return new WaitForSeconds(1);

			cubeTalkPanel.SetActive(false);

			yield return new WaitForSeconds(2);

			Rigidbody cubeRB = activeCube.GetComponent<Rigidbody>();
			cubeRB.useGravity = true;

			yield return new WaitForSeconds(0.25f);

			cubeText.text = "Agghhhhhh!!!";
			cubeTalkPanel.SetActive(true);

			yield return new WaitForSeconds(3);

			Destroy(activeCube);
			cubeTalkPanel.SetActive(false);
		}
	}
}
