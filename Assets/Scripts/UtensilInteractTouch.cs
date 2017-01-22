using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class UtensilInteractTouch : VRTK_InteractTouch {

    public Vector3 positionChange;
    public Vector3 rotationChange;
    public Vector3 scaleChange;

	protected override void CreateTouchCollider()
    {
        base.CreateTouchCollider();
        //controllerCollisionDetector.transform.position =  new Vector3(positionChange.x, positionChange.y, positionChange.z);
        DestroyImmediate(controllerCollisionDetector.transform.Find("Body").gameObject);
        DestroyImmediate(controllerCollisionDetector.transform.Find("SideA").gameObject);
        DestroyImmediate(controllerCollisionDetector.transform.Find("SideB").gameObject);
        Transform headCollider = controllerCollisionDetector.transform.Find("Head");
        Vector3 newPos = new Vector3(positionChange.x, positionChange.y, positionChange.z);
        headCollider.transform.position = Vector3.zero;
        headCollider.transform.localPosition = newPos;

        //Vector3 newRot = new Vector3(rotationChange.x, rotationChange.y, rotationChange.z);
        Quaternion newRot = Quaternion.identity;
        newRot.eulerAngles = new Vector3(rotationChange.x, rotationChange.y, rotationChange.z);
        headCollider.transform.rotation = newRot;
        //headCollider.transform.localRotation = newRot;

        Vector3 newSca = new Vector3(scaleChange.x, scaleChange.y, scaleChange.z);
        //headCollider.transform.scale = Vector3.zero;
        headCollider.transform.localScale = newSca;

    }
}
