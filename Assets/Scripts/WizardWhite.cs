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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void initialiseIngredientTypes() {
		ingredientTypes.Add ("Ruby");
		ingredientTypes.Add ("Gold");
		ingredientTypes.Add ("Quartz");
	}
}
