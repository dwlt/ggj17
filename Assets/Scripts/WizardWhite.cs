using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WizardWhite : MonoBehaviour {
	public Cauldron cauldron;
	public ScrollScript scroll;
    //string literals of ingredient types. Filepath for the corresponding texture 
    //is Assets/Resources/Textures/<string
    // Use this for initialization
    void Start () {
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
	

	public void controlScroll() {

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
