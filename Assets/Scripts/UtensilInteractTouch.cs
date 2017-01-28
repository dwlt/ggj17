using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class UtensilInteractTouch : VRTK_InteractTouch {

	protected override void CreateTouchCollider()
    {
        base.CreateTouchCollider();
        updateCollider();

    }

    public void updateCollider()
    {
        Utensil myUtensil = this.gameObject.GetComponentInChildren<Utensil>();
        Vector3 positionChange = myUtensil.positionChange;
        Vector3 rotationChange = myUtensil.rotationChange;
        Vector3 scaleChange = myUtensil.scaleChange;


        //controllerCollisionDetector.transform.position =  new Vector3(positionChange.x, positionChange.y, positionChange.z);

        if(controllerCollisionDetector.transform.Find("Body"))
            DestroyImmediate(controllerCollisionDetector.transform.Find("Body").gameObject);
        if(controllerCollisionDetector.transform.Find("SideA"))
            DestroyImmediate(controllerCollisionDetector.transform.Find("SideA").gameObject);
        if(controllerCollisionDetector.transform.Find("SideB"))
            DestroyImmediate(controllerCollisionDetector.transform.Find("SideB").gameObject);
        Transform headCollider = controllerCollisionDetector.transform.Find("Head");
        Vector3 newPos = new Vector3(positionChange.x, positionChange.y, positionChange.z);
        headCollider.transform.position = Vector3.zero;
        headCollider.transform.localPosition = newPos;

        //Vector3 newRot = new Vector3(rotationChange.x, rotationChange.y, rotationChange.z);
        Quaternion newRot = Quaternion.identity;
        newRot.eulerAngles = new Vector3(rotationChange.x, rotationChange.y, rotationChange.z);
        headCollider.transform.rotation = newRot;
        headCollider.transform.localRotation = newRot;

        Vector3 newSca = new Vector3(scaleChange.x, scaleChange.y, scaleChange.z);
        //headCollider.transform.scale = Vector3.zero;
        headCollider.transform.localScale = newSca;
    }
}
