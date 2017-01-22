﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// this component should live on whatever the surface of the couldron is, not the cauldron shell
public class Cauldron : MonoBehaviour {
	
	public List<string> recipe; //Contains string representations of everything the cauldron needs
                                //Intention is for the working set to be the first 3 elements
    public List<string> ingredientTypes; 
    public GameObject theGame;
    public GameObject thePrize;
    public GameObject failPrize;
    private WizardWhite wizardWhite;
    public int recipeSize = 1;

    public float remainingTime = 120f;

    //Correct and incorrect placements.
    public int successful = 0;
    public int unsuccessful = 0;
    public int maxfails = 3;

	public AudioClip gameOverFanfare;
	public AudioClip gameWonFanfare;

	// Use this for initialization
	void Start () {
        recipeSize += 2;
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
            checkRecipe(other.gameObject);
            Destroy (other.gameObject);
           


        } else {
			//Debug.Log ("Not Ingredient: " + other.gameObject.ToString ());
		}
	}

    void checkRecipe(GameObject ingredient) {
        Ingredient actualIngredient = ingredient.GetComponent<Ingredient>();

        if (!actualIngredient.hasBeenChecked)
            {
            actualIngredient.hasBeenChecked = true;
            string ingredientName = actualIngredient.ingredientName;
            //The Active set is the first 3 elements of the recipe list
            if (this.recipe.Take(3).Contains(ingredientName)) {
                Debug.Log("Recipe contains: " + ingredientName);
                recipe.Remove(ingredientName); //Always removes the first instance in list
                wizardWhite.controlScroll();
                successfulIngredient();
                if (actualIngredient.cauldronCorrect) {
                    AudioSource.PlayClipAtPoint(actualIngredient.cauldronCorrect, ingredient.transform.position);
                }

                if (recipe.Count == 0) {
                    GameWon();
                }
            } else {
                unsuccessfulIngredient();
                if (gameOverFanfare) {
                    AudioSource.PlayClipAtPoint(gameOverFanfare, ingredient.transform.position);
                }
                Debug.Log("Recipe did not contain: " + ingredientName);
            }
        }
    
	}

    //Success State when cleared the recipe book.
    public void GameWon()
    {
        if (this.gameWonFanfare) {

            AudioSource.PlayClipAtPoint(gameWonFanfare, transform.position);
        }
        //Rest of Game Logic
        Vector3 prizePos = new Vector3(0, 1.1f, 0);
        Instantiate(this.thePrize, transform.position + prizePos, transform.rotation);
        StartCoroutine(wait_restart());
    }
    //Add 10 seconds to their time, 5 of which is spent looking at the result.
    IEnumerator wait_restart()
    {
        this.remainingTime += 10.0f;
        yield return new WaitForSeconds(5);
        this.Start();
    }
    IEnumerator wait_finished()
    {
        GameLost();
        yield return new WaitForSeconds(10);
        
    }
    //Fail state for some reason
    public void GameLost()
    {
        if (this.gameOverFanfare)
        {
            AudioSource.PlayClipAtPoint(gameOverFanfare, transform.position);
        }
        GameObject fp = Instantiate(this.failPrize, transform.position , transform.rotation);
        fp.GetComponent<ObjectConfetti>().launchObjects();
    }

	void successfulIngredient() {
        this.successful++;
		Debug.Log ("whizz bang cool stuff");
		theGame.GetComponent<WizardWhite> ().successfulIngredient ();
	}

	void unsuccessfulIngredient() {
        this.unsuccessful++;
        if (this.unsuccessful >= this.maxfails) {
            GameLost();
        }
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
