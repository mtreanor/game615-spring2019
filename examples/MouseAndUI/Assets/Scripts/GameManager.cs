using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject selectedUnitPanel;

	//These are references to a bunch of UI elements in the Canvas
	public Image portraitImage;
	public Text nameText;
	public Text bioText;
	public ToggleGroup actionToggleGroup;

	public GameObject dialogeObject;
	public Text dialogeText;

	public GameObject complimentEffect;
	public GameObject insultEffect;

	public UnitScript selectedUnit;

	public string currentAction = "";


	//This function is called by UnitScript each time a selected unit is receives a click.
	//It either updates the UI elements with the recently clicked on unit, or deselects all
	//units and resets the UI elements.
	public void selectUnit(UnitScript unit)
	{
		selectedUnit = unit;

		if (unit != null) {
			//Select the unit that was passed in and update the UI
			nameText.text = unit.unitName;
			bioText.text = unit.bio;
			portraitImage.sprite = unit.portrait;

			unit.selected = true;

			selectedUnitPanel.SetActive(true);
		} else {
			//This means we want to disable the user interface and reset it
			resetUI();
		}

		//Get an array full of all game objects with the "Unit" tag. After this line of code, all of the 
		//game objects with tag 'Unit' will be stored in the units array.
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

		//Update all units so that the only selected unit is the one that was passed into this function
		for (int i = 0; i < units.Length; i++) {
			UnitScript us = units[i].GetComponent<UnitScript>();
			if (selectedUnit != us) {
				us.selected = false;
			}
			us.updateVisuals();
		}

		//NOTE: The commented chunk below is equivalent to the 'for' loop above. Both loop through
		//a block of code until a condition becomes false. Compare them to try to understand how they work.
		/*
		int i = 0;
		while (i < units.Length) {
			UnitScript us = units[i].GetComponent<UnitScript>();
			if (selectedUnit != us) {
				us.selected = false;
			}
			us.UpdateVisuals();
			i += 1;
		}
		*/
	}

	void resetUI()
	{
		actionToggleGroup.SetAllTogglesOff();
		currentAction = "";
		selectedUnitPanel.SetActive(false);
		dialogeObject.SetActive(false);
	}

	//These two functions are called based on which one is pressed when the player clicks on a second unit
	public void ComplimentSelected()
	{
		currentAction = "compliment";
	}
	public void InsultSelected()
	{
		currentAction = "insult";
	}

	public void Compliment(UnitScript recipient)
	{
		dialogeText.text = "You're the best, " + recipient.unitName + "!";
		Vector3 dialogueBoxPos = Camera.main.WorldToScreenPoint(selectedUnit.transform.position + Vector3.up * 4f);
		dialogeObject.transform.position = dialogueBoxPos;
		dialogeObject.SetActive(true);
		Destroy(Instantiate(complimentEffect, recipient.transform.position + Vector3.up, Quaternion.identity), 4f);
	}
	public void Insult(UnitScript recipient)
	{
		dialogeText.text = recipient.unitName + "? Booooo! Boooo!!!!";
		Vector3 dialogueBoxPos = Camera.main.WorldToScreenPoint(selectedUnit.transform.position + Vector3.up * 4);
		dialogeObject.transform.position = dialogueBoxPos;
		dialogeObject.SetActive(true);
		Destroy(Instantiate(insultEffect, recipient.transform.position + Vector3.up, Quaternion.identity), 4f);
	}
}
