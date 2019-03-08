using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitScript : MonoBehaviour {
	Renderer renderer;
	CharacterController cc;
	GameManager gameManager;

	//Unit information to be set per unit in the Unity Inspector
	public string unitName = "";
	public string bio = "";
	public Sprite portrait;

	//Colors, set in the Unity Inspector (in this case for the prefab)
	public Color unselectedColor;
	public Color selectedColor;
	public Color hoverColor;

	//These are used to keep track of the mouse state
	public bool selected = false;
	bool hover = false;
	bool justSelected = false;

	//Movement speed
	float speed = 5;

	//In Update, the unit will always move toward its targetDestination. When it
	//gets close to it, won't move toward it anymore.
	Vector3 targetDestination;

	//We set this in the inspector to make it so our click raycast only
	//pays attention to certain layers (for this example, we will only pay attention to 
	//the 'ground' layer when raycasting).
	public LayerMask layerMask;



	void Start () {
		//Get references to various components on the Unit game object.
		renderer = gameObject.GetComponentInChildren<Renderer> ();
		cc = gameObject.GetComponent<CharacterController>();

		//Get a reference to the "GameManager" script. This script will take care of all sorts
		//of things that aren't specific to any one Unit, but instead to the whole game.
		//For example, populating the user interface, making sure only one thing is selected, etc.
		GameObject gameManagerObj = GameObject.Find("GameManager");
		gameManager = gameManagerObj.GetComponent<GameManager>();

		//Initialize the target destition to our current position, so the unit won't move
		targetDestination = transform.position;

		//Make sure the visuals are set correctly at the start of the game.
		updateVisuals();
	}


	// Update is called once per frame
	void Update () {

		if (selected && !justSelected) {
			//If we are selected and the left mouse was clicked, find out what world position the player clicked on, and 
			//set that to our targetDestination
			if (Input.GetMouseButtonDown(0)) { //0 Left click, 1/2 are right/middle click
				//The condition below makes sure that the mouse isn't currently on top of a UI element (just take mty word for it)
				if (EventSystem.current.IsPointerOverGameObject() == false) {
					//Create a ray from the mouse position (in camera/ui space) to 3d world space
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					//After the Raycast, 'hit' will store information about what the raycast hit
					RaycastHit hit;
					//The line below actually performs the "raycast". This will 'shoot' a line from the
					//mouse position into the world, and it if hits something, return true
					if (Physics.Raycast(ray, out hit, layerMask)) {
						//We will only go in here if the raycast hit something
						//Set our target destination to where the Raycast hit.
						targetDestination = hit.point;
					} else {
						//If we get here, this means we didn't click on ANYHTING, so let's deselect
						//the unit by passing 'null' into the selectUnit function.
						gameManager.selectUnit(null);
					}
				}
			}
		}

		//---MOVEMENT---
		//Compute the distance form the unit to its targetDestination
		float distanceToTarget = Vector3.Distance(transform.position, targetDestination);

		//If the unit isn't too close to the target, start rotating and moving towards it
		if (distanceToTarget > 1) {
			//The next chunk of code will rotate frame by frame toward the target
			//--START ROTATE TOWARD
			Vector3 destAtOurHeight = new Vector3(targetDestination.x, transform.position.y, targetDestination.z);
			Vector3 targetDir = destAtOurHeight - transform.position;
			// The step size is equal to speed times frame time.
			float step = 3 * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 1);
			// Rotate a step closer so we are looking tward our target.
			transform.rotation = Quaternion.LookRotation(newDir);
			//--END ROTATE TOWARD

			//Move forward
			cc.Move(transform.forward * speed * Time.deltaTime);
		}

		justSelected = false;
	}
		

	//---BASIC MOUSE INTERACTION---
	//These functions are called by Unity
	void OnMouseEnter() {
		hover = true;
		updateVisuals ();
	}
	void OnMouseExit() {
		hover = false;
		updateVisuals ();
	}
	void OnMouseDown() {
		if (gameManager.selectedUnit != this && gameManager.currentAction != "") {
			if (gameManager.currentAction == "compliment") {
				Debug.Log("Complimenting " + gameObject.name);
			} else if (gameManager.currentAction == "insult") {
				Debug.Log("Insulting " + gameObject.name);
			}
		} else {
			selected = true;
			justSelected = true;
			gameManager.selectUnit(this);

			updateVisuals();
		}
	}


	//This function will set the color of the unit appropriately based on the hover and selected variables
	public void updateVisuals() {
		if (selected) {
			renderer.material.color = selectedColor;
		} else {
			if (hover) {
				renderer.material.color = hoverColor;
			} else {
				renderer.material.color = unselectedColor;
			}
		}
	}
}
