﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
/*Subclass this to implement the specific behaviour for what should happen when the correct tool hits it*/
public class EnvironmentInteractable : VRTK_InteractableObject {
    private VRTK_ControllerActions controllerActions;
    

    public enum IngredientType
    {
        Gardened, Hunted, Mined
    }

    [Header("Custom Options", order = 1)]

    [Tooltip("The type of ingredient contained within this object")]
    public IngredientType ingredientType;
    public string ingredientName;
    public GameObject resource;

    //abstract method to be implemented for specific behaviour
    public virtual void interact()
    {

    }

    //triggered when controller overlaps with object - use to do Utensil/EnvironmentInteractable harvesting
    public override void ToggleHighlight(bool toggle)
    {
        BeingTooled(toggle);
    }

    // tooled is true if there's the right utensil inside me!
    public void BeingTooled(bool tooled)
    {
        Debug.Log("Tooled! by " + this.controllerActions.gameObject.name + " = " + tooled);
        //Get the thing that's tooling me this.controllerActions.gameObject.name 
        GameObject utensil = this.controllerActions.gameObject;
        //spawn my resource - try not to spawn too many - OOPS

        if (tooled && (!utensil.GetComponentInChildren<Utensil>().attachedIngredient || utensil.GetComponentInChildren<Utensil>().attachedIngredient.ingredientName != this.resource.GetComponent<Ingredient>().ingredientName))
        {
            if (utensil.GetComponentInChildren<Utensil>().attachedIngredient)
            {
                //drop old attached item
                utensil.GetComponent<VRTK_InteractGrab>().grabEnabledState = 2;
                utensil.GetComponent<VRTK_InteractGrab>().AttemptReleaseObject();
            }
            GameObject harvestedResource = Instantiate(this.resource, transform.position, transform.rotation);
            //attach it to the utensil
            Rigidbody attachPoint = controllerActions.GetComponent<VRTK_InteractGrab>().controllerAttachPoint;
            Debug.Log("Grabbing object"  + harvestedResource);
            controllerActions.GetComponent<VRTK_InteractGrab>().grabbedObject = harvestedResource;
            controllerActions.GetComponent<VRTK_InteractGrab>().grabEnabledState++;
            harvestedResource.GetComponent<Ingredient>().grabAttachMechanicScript.StartGrab(utensil, harvestedResource, attachPoint);
            harvestedResource.GetComponent<Ingredient>().grabbingObjects.Add(utensil);
            utensil.GetComponentInChildren<Utensil>().attachedIngredient = harvestedResource.GetComponent<Ingredient>();
            harvestedResource.GetComponent<Ingredient>().grabbingUtensil = utensil.GetComponentInChildren<Utensil>();
            harvestedResource.GetComponent<Ingredient>().togglePhysicsWhenGrabbed(false);
            // make the resource exclaim its joy at being included in a recipe
            harvestedResource.GetComponent<Ingredient>().playGrabbedSound();
        }
    }

    public override bool IsValidInteractableController(GameObject actualController, AllowedController controllerCheck)
    {
        controllerActions = actualController.GetComponent<VRTK_ControllerActions>();
        Utensil utensil = actualController.GetComponentInChildren<Utensil>();
        // this triggers when utensil hits a resource object
        if (utensil)
        {// is this the correct tool for this thing we are trying to grab?
            if ((ingredientType == IngredientType.Mined && Utensil.UtensilType.Pickaxe == utensil.utensilType) ||
                (ingredientType == IngredientType.Hunted && Utensil.UtensilType.Knife == utensil.utensilType) ||
                (ingredientType == IngredientType.Gardened && Utensil.UtensilType.Shears == utensil.utensilType))
            {

                if (controllerActions)
                {
                    controllerActions.TriggerHapticPulse(0.5f);
                }
                return true;
            }
            //Using the wrong tool triggers a different pulse
            else if (controllerActions)
            {
                controllerActions.TriggerHapticPulse(0.75f, 0.3f, 0.01f);
            }
        }
        // only play sound if the sound file isn't null
        /*if (unsuccessfulGrab != null && !goodGrab)
        {
            AudioSource.PlayClipAtPoint(unsuccessfulGrab, transform.position);
        }*/
        utensil.playUnsuccessfulGrabSound();
    
        //Reaches this section only if the ingredient check doesn't match
        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
}
