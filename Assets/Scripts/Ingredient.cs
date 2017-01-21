using System;
using UnityEngine;
using VRTK;

public class Ingredient : VRTK_InteractableObject
{
	private VRTK_ControllerActions controllerActions;

	public enum IngredientType
	{
		Gardened, Hunted, Mined
	}

	[Header("Custom Options", order = 1)]

	[Tooltip("The type of ingredient")]
	public IngredientType ingredientType = IngredientType.Gardened;

    public override bool IsValidInteractableController(GameObject actualController, AllowedController controllerCheck)
    {
        controllerActions = actualController.GetComponent<VRTK_ControllerActions>();
        Utensil utensil = actualController.GetComponent<Utensil>();
        bool allowable = false;

        if (utensil)
        {
            if ((ingredientType == IngredientType.Mined && Utensil.UtensilType.Pickaxe == utensil.utensilType) ||
                (ingredientType == IngredientType.Hunted && Utensil.UtensilType.Knife == utensil.utensilType) ||
                (ingredientType == IngredientType.Gardened && Utensil.UtensilType.Shears == utensil.utensilType))
            {
                Debug.Log("pick up correctly");
                if (controllerActions)
                {
                    controllerActions.TriggerHapticPulse(0.5f);
                }
                allowable = true;
            }
            else
            {
                Debug.Log("pick up badly");
                if (controllerActions)
                {
                    controllerActions.TriggerHapticPulse(0.75f, 0.3f, 0.01f);
                }
            }
        }
        return allowable;


    }


   
}

