using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// this component should live on whatever the surface of the couldron is, not the cauldron shell
public class Cauldron : MonoBehaviour {
	
	public List<string> recipe; //Contains string representations of everything the cauldron needs
                                //Intention is for the working set to be the first 3 elements
    public List<string> ingredientTypes; 
    public GameObject theGame;
    private WizardWhite wizardWhite;

	public int minRecipeSize = 3;
	public int maxRecipeSize = 7;

	public AudioClip gameOverFanfare;
	public AudioClip gameWonFanfare;

	// Use this for initialization
	void Start () {
        
		gameOverFanfare.LoadAudioData();
		gameWonFanfare.LoadAudioData();

        this.ingredientTypes = new List<string>
        {
            //Mineables
            "Sapphire",
            "Gold",
            "Emerald",
            "Quartz",
            "Diamond",
            //Huntables
            "Ear",
            "Eye",
            "Feather",
            "Horn",
            "Bone",
            //Gatherables
            "Fruit",
            "Flower",
            "Venus",
            "Herb",
            "Root",
        };
        newRecipe();
        wizardWhite = theGame.GetComponent<WizardWhite>();
        wizardWhite.controlScroll();
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
        //The Active set is the first 3 elements of the recipe list
		if (this.recipe.Take(3).Contains (ingredientName)) {
			Debug.Log ("Recipe contains: " + ingredientName);
			recipe.Remove (ingredientName); //Always removes the first instance in list
            wizardWhite.controlScroll();
			successfulIngredient ();
			if (actualIngredient.cauldronCorrect) {
				AudioSource.PlayClipAtPoint(actualIngredient.cauldronCorrect, ingredient.transform.position);
			}

			if (recipe.Count == 0) {
                GameWon();
			}
		} else {
			unsuccessfulIngredient ();
			if (gameOverFanfare) {
				AudioSource.PlayClipAtPoint(gameOverFanfare, ingredient.transform.position);
			}
			Debug.Log ("Recipe did not contain: " + ingredientName);
		}
	}

    //Success State when cleared the recipe book.
    public void GameWon()
    {
        if (this.gameWonFanfare) {
            AudioSource.PlayClipAtPoint(gameOverFanfare, transform.position);
        }
        //Rest of Game Logic
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
        Debug.Log("Recipe size: " + recipeSize.ToString() + "Number of Ingredients" + ingredientTypes.Count.ToString());
		recipe = new List<string> (); //Reset Recipe
		for (int i = 0; i < recipeSize; i++) {
			int nextIngredient = Random.Range (0, ingredientTypes.Count);
			recipe.Add (ingredientTypes.ElementAt(nextIngredient));

		}
	}

	void printRecipe(){
		Debug.Log ("Recipe:");

		for (int i = 0; i < recipe.Count; i++) {
			Debug.Log (i + ": " + recipe [i]);
		}

	}
}
