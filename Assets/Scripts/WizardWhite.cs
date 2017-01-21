using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardWhite : MonoBehaviour {
	public Cauldron cauldron;
	public ScrollScript scroll;

	public ArrayList ingredientTypes = new ArrayList();


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

	void initialiseIngredientTypes() {
		ingredientTypes.Add ("Ruby");
		ingredientTypes.Add ("Gold");
		ingredientTypes.Add ("Quartz");
	}

	void controlScroll() {
		//scroll.setPanels ("grass");
		scroll.setPanels (cauldron.recipe[0].ToString(), cauldron.recipe[1].ToString(), cauldron.recipe[2].ToString());

		scroll.panelView ();
	}
}
