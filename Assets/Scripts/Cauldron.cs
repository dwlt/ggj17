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
    [Tooltip("Both recipe sizes must be > 3")]
    public int minRecipeSize = 4;
	public int maxRecipeSize = 7;

	public AudioClip gameOverFanfare;
	public AudioClip gameWonFanfare;

	// Use this for initialization
	void Start () {
        minRecipeSize -= 3; //3 guaranteed picks one from each category.
        maxRecipeSize -= 3;
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
            "Eye",//"Ear",
            "Eye",
            "Eye",//"Feather",
           "Eye",// "Horn",
           "Eye",// "Bone",
            //Gatherables
            "Fruit",
            "Flower",
            "Fruit",//"Venus",
            "Herb",
            "Flower",//"Root",
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
        recipe = new List<string>(); //Reset Recipe
        //Seed with one from each category
        int[] seeded = { Random.Range(0, 4), Random.Range(5, 9), Random.Range(10, 14) };
        for (int i = 0; i < seeded.Length; i++)
        {
            recipe.Add(ingredientTypes[seeded[i]]);
            Debug.Log( "Adding: " + seeded[i].ToString() + ingredientTypes[seeded[i]]);
        }

        int recipeSize = Random.Range (minRecipeSize, maxRecipeSize);
		for (int i = 0; i < recipeSize; i++) {
			int nextIngredient = Random.Range (0, ingredientTypes.Count);
			recipe.Add (ingredientTypes.ElementAt(nextIngredient));
		}
        printRecipe();
	}

	void printRecipe(){
		Debug.Log ("Recipe:");

		for (int i = 0; i < recipe.Count; i++) {
			Debug.Log (i + ": " + recipe [i]);
		}

	}
}
