using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class UtensilChanger : VRTK_InteractableObject
{

    public GameObject myUtensil;
    public GameObject testToucher;
    public bool unused;
    // Use this for initialization
    void Start()
    {

    }

    //triggered when controller overlaps  - if true then you are entering - false when existing
    public override void ToggleHighlight(bool toggle)
    {
        Debug.Log("tried to highlight the cube");
        //Switch Utensil type when enter
        //if (toggle)
            //SwitchUtensil();
    }

    public override void StartTouching(GameObject currentTouchingObject)
    {
        Debug.Log("started touching the cube");
        base.StartTouching(currentTouchingObject);
        SwitchUtensil(currentTouchingObject);
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKey(KeyCode.C))
        {
            SwitchUtensil(testToucher);
        }
    }



    //Each changer works for one utensil type - only switch if currently unused
    public void SwitchUtensil(GameObject currentTouchingObject)
    {
        if (unused)
        {
            GameObject utensil = currentTouchingObject.GetComponentInChildren<Utensil>().gameObject;
            Debug.Log("touched by a " + utensil.name);
            if (utensil) //did we get touched by a controller?
            {

                //we want to move this gameObject and everything under it to be under the new controller, and store the other one somewhere safe - maybe under ourself?
                //probably better to do with prefabs

                //OldUtensil
                //move from controller to altar
                utensil.transform.parent = utensil.GetComponentInChildren<Utensil>().altar.transform;
                //set unused on altar
                utensil.GetComponentInChildren<Utensil>().altar.GetComponent<UtensilChanger>().unused = true;
                //activate altar
                utensil.GetComponentInChildren<Utensil>().altar.SetActive(true);
                //clear and remove all attached ingredients
                if (utensil.GetComponentInChildren<Utensil>().attachedIngredient)
                {
                    Destroy(utensil.GetComponentInChildren<Utensil>().attachedIngredient);
                }
                //set active false
                utensil.SetActive(false);



                //new Utensil
                //move utensil to be under the hand
                myUtensil.transform.parent = currentTouchingObject.transform;
                //set active true on new utensil
                myUtensil.SetActive(true);

                //fix colliders and positions
                //update controller attachment point
                currentTouchingObject.GetComponent<VRTK_InteractGrab>().controllerAttachPoint = myUtensil.GetComponent<Utensil>().attachPoint;
                //set positions
                Vector3 newPos = new Vector3(utensil.transform.position.x, utensil.transform.position.y, utensil.transform.position.z);
                
                myUtensil.transform.position = newPos;
                //Vector3 newLocalPos = new Vector3(utensil.transform.localPosition.x, utensil.transform.localPosition.y, utensil.transform.localPosition.z);
                Vector3 newLocalPos = new Vector3(myUtensil.GetComponent<Utensil>().defaultPosition.x, myUtensil.GetComponent<Utensil>().defaultPosition.y, myUtensil.GetComponent<Utensil>().defaultPosition.z);
                myUtensil.transform.localPosition = newLocalPos;
                Quaternion newRot = Quaternion.identity;
                newRot.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                myUtensil.GetComponent<Utensil>().transform.localRotation = newRot;
                currentTouchingObject.GetComponentInChildren<UtensilInteractTouch>().updateCollider();
                myUtensil.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                //set unused false on me
                unused = false;
                //hide the spinning altar
                this.gameObject.SetActive(false);
                //myUtensil.transform.parent = utensil.transform.parent;







            }
        }
    }

}