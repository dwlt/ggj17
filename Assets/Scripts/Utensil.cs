using System;
using UnityEngine;

public class Utensil : MonoBehaviour
{
	public enum UtensilType 
	{
		Shears, Knife, Pickaxe
	}

	[Tooltip("The type of utensil")]
	public UtensilType utensilType = UtensilType.Shears;
}

