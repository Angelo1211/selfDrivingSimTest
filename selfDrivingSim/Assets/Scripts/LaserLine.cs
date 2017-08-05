using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLine : MonoBehaviour {

    public LineRenderer laserLineRenderer;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 60f;

    public Vector3 Origin;

    private Gradient gradient;

    void Start() {
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.SetWidth(laserWidth, laserWidth);
        Scanner parent = gameObject.GetComponentInParent<Scanner>();
        gradient = parent.coloring;
    }

    void Update() {
        ShootLaserFromTargetPosition(transform.position, transform.TransformDirection(Vector3.forward), laserMaxLength);
    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length) {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = Vector3.zero;//targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length)) {
            endPosition = raycastHit.point;
            targetPosition = endPosition*0.991f;
        }
        else {
            targetPosition = Vector3.zero;
        }

        

        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);

        laserLineRenderer.material.color = gradient.Evaluate(targetPosition.magnitude*3/laserMaxLength);

    }
}