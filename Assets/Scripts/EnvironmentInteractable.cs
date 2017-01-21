using System.Collections;
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
    public int testInt;

    //abstract method to be implemented for specific behaviour
    public virtual void interact()
    {

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
                return true;
            }
            //Using the wrong tool triggers a different pulse
            else if (controllerActions)
            {
                controllerActions.TriggerHapticPulse(0.75f, 0.3f, 0.01f);
            }
        }
        //Reaches this section only if the ingredient check doesn't match
        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
}
