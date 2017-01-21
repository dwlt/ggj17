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

	public override void Grabbed(GameObject currentGrabbingObject)
	{
		controllerActions = currentGrabbingObject.GetComponent<VRTK_ControllerActions>();
		Utensil utensil = currentGrabbingObject.GetComponent<Utensil>();

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
				base.Grabbed(currentGrabbingObject);
			}
			else
			{
				if (controllerActions) 
				{ 
					controllerActions.TriggerHapticPulse(0.75f, 0.3f, 0.01f);
				}
			}
		}
	}
}

