using System;
using UnityEngine;
using VRTK;

public class Utensil : MonoBehaviour
{
	public enum UtensilType 
	{
		Shears, Knife, Pickaxe
	}

    public Vector3 positionChange;
    public Vector3 rotationChange;
    public Vector3 scaleChange;
    public Vector3 defaultPosition;
    public Vector3 defaultScale;

    public Rigidbody attachPoint;
    public GameObject altar;


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

