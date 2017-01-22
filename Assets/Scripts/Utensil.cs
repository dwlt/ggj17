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
    public Ingredient attachedIngredient;
    private float prevSoundPlayed = 0;

    [Tooltip("The type of utensil")]
	public UtensilType utensilType = UtensilType.Shears;


    void start()
    {
        unsuccessfulGrab.LoadAudioData();
    }

    public void playUnsuccessfulGrabSound()
    {
        // only play sound if the sound file isn't null
        if (unsuccessfulGrab != null)
        {
            Debug.Log("prev: " + prevSoundPlayed);
            prevSoundPlayed -= Time.deltaTime;
            if (prevSoundPlayed <= 0)
            {
                AudioSource.PlayClipAtPoint(unsuccessfulGrab, transform.position);
                prevSoundPlayed = (unsuccessfulGrab.length);
            }

        }
    }

}

