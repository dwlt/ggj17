using System;
using UnityEngine;
using VRTK;

public class Ingredient : VRTK_InteractableObject
{
	private VRTK_ControllerActions controllerActions;
    public Utensil grabbingUtensil; 

	public enum IngredientType
	{
		Gardened, Hunted, Mined
	}

    [Header("Ingredient Options", order = 1)]

    [Tooltip("The type of ingredient contained within this object")]
    public IngredientType ingredientType;
    public string ingredientName;
	[Tooltip("FX for when dropped in the cauldron and is in the recipe")]
	public AudioClip cauldronCorrect;
	[Tooltip("FX for this hitting the floor")]
	public AudioClip floorHit;
	[Tooltip("FX for successful first grab of ingredient")]
	public AudioClip successfulGrab;
	private bool firstGrab; // assumes a new object each 
	[Tooltip("FX for invalid attempt to grab")]
	public AudioClip unsuccessfulGrab;

	// Use this for initialization
	void Start () {
		//cauldronCorrect.LoadAudioData();
		floorHit.LoadAudioData();
		successfulGrab.LoadAudioData();
		unsuccessfulGrab.LoadAudioData();
	}

	void OnCollisionEnter(Collision c) {
		if ("Floor".Equals(c.gameObject.name) && floorHit) {
			AudioSource.PlayClipAtPoint(floorHit, transform.position);
		}
	}

    public void togglePhysicsWhenGrabbed(bool physics)
    {
        this.transform.GetComponent<Collider>().enabled = physics;
    }

    public override bool IsValidInteractableController(GameObject actualController, AllowedController controllerCheck)
    {
        controllerActions = actualController.GetComponent<VRTK_ControllerActions>();
        Utensil utensil = actualController.GetComponent<Utensil>();

        if (utensil)
        {
            if ((ingredientType == IngredientType.Mined && Utensil.UtensilType.Pickaxe == utensil.utensilType) ||
                (ingredientType == IngredientType.Hunted && Utensil.UtensilType.Knife == utensil.utensilType) ||
                (ingredientType == IngredientType.Gardened && Utensil.UtensilType.Shears == utensil.utensilType))
                {
              
                if (controllerActions)
                {
                    controllerActions.TriggerHapticPulse(0.5f);
                }
				// only play sound if the sound file isn't null
				if (successfulGrab != null)
				{
					AudioSource.PlayClipAtPoint(successfulGrab, transform.position);
				}
                return true;
            }
            //Using the wrong tool triggers a different pulse
            else if (controllerActions)
            {
	            controllerActions.TriggerHapticPulse(0.75f, 0.3f, 0.01f);
				// only play sound if the sound file isn't null
				if (unsuccessfulGrab != null)
				{
					AudioSource.PlayClipAtPoint(unsuccessfulGrab, transform.position);
				}
            }
        }
        //Reaches this section only if the ingredient check doesn't match
        return false;
    }
   
}

