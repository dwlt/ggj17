using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularAnimator : MonoBehaviour 

{
    public Vector3 Velocity = new Vector2(1, 0);

    [Range(0, 5)]
    public float RotateSpeed = 2f;
    [Range(0, 5)]
    public float RotateRadiusX = 0.01f;
    [Range(0, 5)]
    public float RotateRadiusZ = 0.05f;

    public bool Clockwise = true;

    private Vector2 _centre;
    private float _angle;

    private void Start()
    {
        _centre = transform.position;
    }

    private void Update()
    {

        _angle += (Clockwise ? RotateSpeed : -RotateSpeed) * Time.deltaTime;

        var x = Mathf.Sin(_angle) * RotateRadiusX;
        var z = Mathf.Cos(_angle) * RotateRadiusZ;

        transform.position = _centre + new Vector2(x, z);
    }

    /*void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_centre, 0.1f);
        Gizmos.DrawLine(_centre, transform.position);
    }
    */
}
