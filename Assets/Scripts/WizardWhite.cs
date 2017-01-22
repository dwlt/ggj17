using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardWhite : MonoBehaviour {
	public Cauldron cauldron;
	public ScrollScript scroll;

	public ArrayList ingredientMined = new ArrayList();
	public ArrayList ingredientGardened = new ArrayList();
	public ArrayList ingredientHunted = new ArrayList();



	// Use this for initialization
	void Start () {
		initialiseIngredientTypes ();
		cauldron.init ();

		cauldron.newRecipe ();
		controlScroll ();


	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void successfulIngredient() {
		controlScroll ();
	}

	public void unsuccessfulIngredient() {
		Debug.Log ("game level unsuccessful");
	}

	void initialiseIngredientTypes() {
		ingredientMined.Add ("Ruby");
		ingredientMined.Add ("Gold");
		ingredientMined.Add ("Quartz");
		ingredientMined.Add ("Sapphire");
		ingredientMined.Add ("Diamond");

		ingredientHunted.Add ("Bone");
		ingredientHunted.Add ("Ear");
		ingredientHunted.Add ("Eye");
		ingredientHunted.Add ("Horn");
		//ingredientHunted.Add ("Diamond");

		ingredientGardened.Add ("Flower");
		ingredientGardened.Add ("Fruit");
		ingredientGardened.Add ("Herb");
		ingredientGardened.Add ("Root");
		ingredientGardened.Add ("Venus");
	}

	void controlScroll() {
		
		//scroll.setPanels ("MagicWhole");

		if (cauldron.recipe.Count > 2) {
			scroll.setPanels (cauldron.recipe [0].ToString (), cauldron.recipe [1].ToString (), cauldron.recipe [2].ToString ());
			scroll.panelView ();
		} else if (cauldron.recipe.Count > 1) {
			scroll.setPanels (cauldron.recipe [0].ToString (), cauldron.recipe [1].ToString (), "MagicBottom");
			scroll.panelView ();
		} else if (cauldron.recipe.Count > 0) {
			scroll.setPanels (cauldron.recipe [0].ToString (), "MagicMid", "MagicBottom");
			scroll.panelView ();
		} else {
			scroll.setPanels ("MagicWhole");
			scroll.wholeView();
		}

	}
}
