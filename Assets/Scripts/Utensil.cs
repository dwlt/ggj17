using System;
using UnityEngine;
using VRTK;

public class Utensil : VRTK_ControllerEvents
{
	public enum UtensilType 
	{
		Shears, Knife, Pickaxe
	}

	[Tooltip("FX for invalid attempt to grab")]
	public AudioClip unsuccessfulGrab;

	private float prevSoundPlayed = 0;

    public Ingredient attachedIngredient;

	[Tooltip("The type of utensil")]
	public UtensilType utensilType = UtensilType.Shears;

	void start(){
		unsuccessfulGrab.LoadAudioData();
	}

	public void playUnsuccessfulGrabSound() {
		// only play sound if the sound file isn't null
		if (unsuccessfulGrab != null)
		{
			Debug.Log ("prev: " + prevSoundPlayed);
			prevSoundPlayed -= Time.deltaTime;
			if (prevSoundPlayed <= 0) {
				AudioSource.PlayClipAtPoint (unsuccessfulGrab, transform.position);
				prevSoundPlayed = (unsuccessfulGrab.length);
			} 

		}
	}
  
	void Update() {
		if (Input.GetKeyDown ("space")) {
			nextUtensil ();
		}
	}

	  // for single player testing 
	  public void nextUtensil(){ 
	    Debug.Log ("Next Utensil"); 
	 
	 
	    if (utensilType == UtensilType.Shears) { 
	      utensilType = UtensilType.Knife; 
	    } else if (utensilType == UtensilType.Knife) { 
	      utensilType = UtensilType.Pickaxe; 
	    } else if (utensilType == UtensilType.Pickaxe) { 
	      utensilType = UtensilType.Shears; 
	    } 
	  } 

}

