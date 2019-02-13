using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	float moveSpeed = 5f;
	float rotateSpeed = 40f;

	public Rigidbody rigidbody;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Debug.DrawRay(transform.position, transform.forward * 10);

		float hAxis = Input.GetAxis("Horizontal");
		float vAxis = Input.GetAxis("Vertical");

		transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0);
		transform.Translate(transform.forward * vAxis * moveSpeed * Time.deltaTime, Space.World);
	}
}
