using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLine : MonoBehaviour {

    private float laserMaxLength = 60f;
    private Vector3 endPosition;

    //Returns a raycasthit point if the ray "laser" encounters a physics collider, (0,0,0) if it doesn't.
     public Vector3 getRay() {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, laserMaxLength)){
            endPosition = raycastHit.point;
        }
        else {
            endPosition = Vector3.zero;
        }
        return endPosition;
    }
}