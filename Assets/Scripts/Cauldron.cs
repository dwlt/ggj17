﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this component should live on whatever the surface of the couldron is, not the cauldron shell
public class Cauldron : MonoBehaviour {
	
	public ArrayList recipe; 
	private ArrayList ingredientTypes;
	public GameObject theGame;

	public int minRecipeSize = 3;
	public int maxRecipeSize = 7;

	public AudioClip gameOverFanfare;
	public AudioClip gameWonFanfare;

	// Use this for initialization
	void Start () {
		gameOverFanfare.LoadAudioData();
		gameWonFanfare.LoadAudioData();
	}

	public void init() {
		ingredientTypes = theGame.GetComponent<WizardWhite> ().ingredientTypes;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "ingredient") {
			Destroy (other.gameObject);
			checkRecipe (other.gameObject);

		} else {
			//Debug.Log ("Not Ingredient: " + other.gameObject.ToString ());
		}
	}

	void checkRecipe(GameObject ingredient) {
		Ingredient actualIngredient = ingredient.GetComponent<Ingredient>();

		string ingredientName = actualIngredient.ingredientName;

		if (recipe.Contains (ingredientName)) {
			Debug.Log ("Recipe contains: " + ingredientName);
			recipe.Remove (ingredientName);
			successfulIngredient ();
			if (actualIngredient.cauldronCorrect) {
				AudioSource.PlayClipAtPoint(actualIngredient.cauldronCorrect, ingredient.transform.position);
			}

			if (recipe.Count == 0 && gameWonFanfare) {
				AudioSource.PlayClipAtPoint(gameWonFanfare, transform.position);
			}
		} else {
			unsuccessfulIngredient ();
			if (gameOverFanfare) {
				AudioSource.PlayClipAtPoint(gameOverFanfare, ingredient.transform.position);
			}
			Debug.Log ("Recipe did not contain: " + ingredientName);
		}
	}

	void successfulIngredient() {
		Debug.Log ("whizz bang cool stuff");
		theGame.GetComponent<WizardWhite> ().successfulIngredient ();
	}

	void unsuccessfulIngredient() {
		Debug.Log ("crash whoop bad stuff");
		theGame.GetComponent<WizardWhite> ().unsuccessfulIngredient ();
	}


	public void newRecipe() {
		int recipeSize = Random.Range (minRecipeSize, maxRecipeSize);
		recipe = new ArrayList ();
		for (int ingNum = 0; ingNum < recipeSize; ingNum++) {
			int nextIngredient = Random.Range (0, ingredientTypes.Count);
			recipe.Add (ingredientTypes [nextIngredient]);
		}
	}



	void printRecipe(){
		Debug.Log ("Recipe:");

		for (int i = 0; i < recipe.Count; i++) {
			Debug.Log (i + ": " + recipe [i]);
		}

	}
}
