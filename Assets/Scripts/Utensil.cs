using System;
using UnityEngine;
using VRTK;

public class Utensil : VRTK_ControllerEvents
{
	public enum UtensilType 
	{
		Shears, Knife, Pickaxe
	}

    public Ingredient attachedIngredient;

	[Tooltip("The type of utensil")]
	public UtensilType utensilType = UtensilType.Shears;


  

}

