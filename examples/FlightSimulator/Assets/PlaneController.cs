using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{

	public float forwardSpeed = 15f;
	public float rollSpeed = 35f;
	public float pitchSpeed = 20f;

	public float pitchSpeedRate = 5f;

	float cameraFollowBehindAmount = 15;
	float cameraFollowAboveAmount = 5;

	Rigidbody rigidbody;

	public GameObject footballPrefab;
	public float footballLaunchForce = 1000f;

	// Start is called before the first frame update
	void Start()
    {
		rigidbody = gameObject.GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
		//Get the up/down/left/right input values
		float hAxis = Input.GetAxis("Horizontal");
		float vAxis = Input.GetAxis("Vertical");

		//Rotate
		transform.Rotate(vAxis * rollSpeed * Time.deltaTime, hAxis * pitchSpeed/4 * Time.deltaTime, -hAxis * pitchSpeed * Time.deltaTime, Space.Self);

		//Turn on gravity if we are moving too slow
		if (forwardSpeed < 0.5f) {
			rigidbody.isKinematic = false;
			rigidbody.useGravity = true;
		} else {
			rigidbody.isKinematic = true;
			rigidbody.useGravity = false;
		}
		//Change the forwardSpeed based on how much we are looking upward
		forwardSpeed += -transform.forward.y * pitchSpeedRate * Time.deltaTime;
		forwardSpeed = Mathf.Clamp(forwardSpeed, 0, 30);

		//Move the plane and keep it above the terrain height
		transform.Translate(transform.forward * forwardSpeed * Time.deltaTime, Space.World);
		float terrainHeight = Terrain.activeTerrain.SampleHeight(transform.position);
		if (transform.position.y < terrainHeight) {
			transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.z);
		}


		//Shoot a football when the player presses space
		if (Input.GetKeyDown(KeyCode.Space)) {
			GameObject football = Instantiate(footballPrefab, transform.position + transform.forward * 5, transform.rotation);
			Rigidbody footballRB = football.GetComponent<Rigidbody>();
			footballRB.AddForce(transform.forward * footballLaunchForce);
		}


		//Position the camera
		Camera.main.transform.position = transform.position + -transform.forward * cameraFollowBehindAmount + Vector3.up * cameraFollowAboveAmount;
		Camera.main.transform.LookAt(transform);
	}
}
