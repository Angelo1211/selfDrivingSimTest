using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour {

    [Range(0.5f, 60f)]
    public float scanFrequency;

    [Range(0.08f, 20f)]
    public float horizontalAngularRes;
    [Range(0.4f, 26.9f)]
    public float verticalAngularRes;

    public LaserLine laserLinePrefab;

    float[] verticalFOVRange = {2f,-24.9f};
    const float totalVertFOV = 26.9f;
    float[] horizontalFOVRange = { 0f, 360f };
    float totalHorFOV = 360f;
    [SerializeField]
    float scansPerSecond = 0;
    int scansPerSlice = 0;
    int numSlice = 0;
    Vector3 tiltAngle;

	// Use this for initialization
	void Start () {
        createLidarScan();
	}
	
	// Update is called once per frame
	void Update () {
        scansPerSecond = calculateScansPerSecond();

	}

    float calculateScansPerSecond() {
        return scanFrequency * (totalHorFOV/horizontalAngularRes) * (totalVertFOV/verticalAngularRes);
    }

    void spawnLaserBeam() {
        LaserLine spawn = Instantiate<LaserLine>(laserLinePrefab);
        spawn.transform.localPosition = transform.position;
        spawn.transform.localRotation = Quaternion.Euler(tiltAngle);
        spawn.transform.parent = gameObject.transform; 
    }

    void createLidarScan() {
        scansPerSlice = (int)Mathf.Floor(totalVertFOV / verticalAngularRes);
        numSlice = (int)Mathf.Floor(totalHorFOV / horizontalAngularRes);
        for (int i = 0; i < numSlice; i++) {
            for (int j = 0; j < scansPerSlice; j++) {

                tiltAngle = new Vector3(verticalFOVRange[0]-j*verticalAngularRes, i*horizontalAngularRes, 0);
                spawnLaserBeam();

            }
        }
        
    }
}
