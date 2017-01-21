using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour {
    public GameObject planeTop;
    public GameObject planeMiddle;
    public GameObject planeBottom;
    public GameObject planeWhole;

	public string ingredientTop;
	public string ingredientMiddle;
	public string ingredientBottom;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void wholeView()
    {
        planeTop.GetComponent<Renderer>().enabled = false;
        planeMiddle.GetComponent<Renderer>().enabled = false;
        planeBottom.GetComponent<Renderer>().enabled = false;
        planeWhole.GetComponent<Renderer>().enabled = true;
    }
	public void panelView()
    {
        planeTop.GetComponent<Renderer>().enabled = true;
        planeMiddle.GetComponent<Renderer>().enabled = true;
        planeBottom.GetComponent<Renderer>().enabled = true;
        planeWhole.GetComponent<Renderer>().enabled = false;
    }
    //Set textures for the constituent parts of the scroll, top, middle, bottom third segments. Visible in panelView
	public  void setPanels(string top, string middle, string bottom)
    {
        planeTop.GetComponent<Renderer>().material.mainTexture = (Texture2D) Resources.Load("Textures/" + top, typeof(Texture2D));
		ingredientTop = top;
        planeMiddle.GetComponent<Renderer>().material.mainTexture = (Texture2D)Resources.Load("Textures/" + middle, typeof(Texture2D));
		ingredientMiddle = middle;   
		planeBottom.GetComponent<Renderer>().material.mainTexture = (Texture2D)Resources.Load("Textures/" + bottom, typeof(Texture2D));
		ingredientBottom = bottom;
	}
    //Use to change the whole page canvas. Visible in wholeView
	public void setPanels(string whole)
    {
        planeWhole.GetComponent<Renderer>().material.mainTexture = (Texture2D)Resources.Load("Textures/" + whole, typeof(Texture2D));
    }
}
