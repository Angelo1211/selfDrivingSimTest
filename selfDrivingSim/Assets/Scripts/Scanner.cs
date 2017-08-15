using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour {

    [Range(0.1f, 50f)]
    public float rotationFrequency;    
    public float scanArea;
    public float scansPerSecond = 0;

    public LaserLine laserLinePrefab;
    public Gradient coloring;
    public ScannerData dataDict;

    float verticalAngularRes = 0.4f;
    float[] verticalFOVRange = {-15f,15f};
    float[] horizontalFOVRange = { 0f, 360f };
    int scansPerSlice = 64;
    Vector3 tiltAngle;
    Transform transform;       
    LaserLine[] laserArray;
    Vector3[] laserPositions;
    Vector3 rotation;
    

	// Use this for initialization
	void Awake() {
        transform = gameObject.GetComponent<Transform>();
        createLidarScan();
        scanArea = 360 * rotationFrequency * Time.fixedDeltaTime;
        dataDict = new ScannerData((int)(scansPerSlice*(360/scanArea)));
        scansPerSecond = calculateScansPerSecond();
        rotation = new Vector3(0, scanArea, 0);
    }
	
	void FixedUpdate () {
                
        transform.Rotate(rotation);
        int currentAngle = (int)transform.localRotation.eulerAngles.y;
        if(currentAngle % 2 == 1) {
            currentAngle++;
        }

        updateLaserImpactLocations();
        dataDict.addPointsAtAngle(currentAngle, laserPositions);
        
             
    }


    void updateLaserImpactLocations() {
        for (int i = 0; i < laserArray.Length; i++) {
            laserPositions[i] = laserArray[i].getRay();
            //Debug.Log(laserPositions[i]);
        }
    }

    float calculateScansPerSecond() {
        return scansPerSlice* 1/Time.fixedDeltaTime;
    }

    LaserLine spawnLaserBeam() {
        LaserLine spawn = Instantiate<LaserLine>(laserLinePrefab);
        spawn.transform.localPosition = transform.position;
        spawn.transform.localRotation = Quaternion.Euler(tiltAngle);
        spawn.transform.parent = gameObject.transform;

        return spawn;    
    }

    void createLidarScan() {
        laserArray = new LaserLine[scansPerSlice];
        laserPositions = new Vector3[scansPerSlice]; 
        for (int i = 0; i < scansPerSlice; i++) {
            tiltAngle = new Vector3(verticalFOVRange[0]+i*verticalAngularRes, 0, 0);
            laserArray[i] = spawnLaserBeam();        
        }  
    }
}
