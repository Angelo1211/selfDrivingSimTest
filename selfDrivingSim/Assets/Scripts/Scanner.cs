//Author: Angel Ortiz
//Date: 08/15/17

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour {
    public LaserLine laserLinePrefab;

    [Range(5f, 50f)]
    public float rotationFrequency;
    [SerializeField]
    float scanArea;
    [SerializeField]
    float scansPerSecond;
    public ScannerData lidarDataDict;

    //Data obtained from HDL-64E S3 Velodine Lidar spec sheet.
    const float verticalAngularRes = 0.4f;
    const int laserChannels = 64;
    const float verticalStartPoint = -5f;
     
    Transform transform;       
    LaserLine[] laserArray;
    Vector3[] laserImpactLocs;
    Vector3 rotation;
    

	//Initializing object & lidar scan FOV and pre-calculating scans/s.
	void Awake() {
        transform = gameObject.GetComponent<Transform>();
        createLidarScan();        
        scansPerSecond = calculateScansPerSecond();        
    }
    //Every physics update the scanner will rotate, then query all the laserbeams and store results in
    //the lidar data structure. 
    void FixedUpdate() {
        transform.Rotate(rotation);
        int currentAngle = (int)transform.localRotation.eulerAngles.y;
        if (currentAngle % 2 == 1) currentAngle++; //Fixing floating point rounding issue.
        updateLaserImpactLocations();
        lidarDataDict.addPointsAtAngle(currentAngle, laserImpactLocs);
    }

    //Instantiate lidar Scanner and calculates the scanArea.
    //Also intantiates the data structure holding the lidar data.
    void createLidarScan() {
        laserArray = new LaserLine[laserChannels];
        laserImpactLocs = new Vector3[laserChannels];
        for (int i = 0; i < laserChannels; i++) {
            Vector3 tiltAngle = new Vector3(verticalStartPoint + i * verticalAngularRes,0);
            laserArray[i] = spawnLaserBeam(tiltAngle);
        }

        scanArea = 360 * rotationFrequency * Time.fixedDeltaTime; //Fixed rotation speed to match physics updates
        int numOfPoints = (int)(laserChannels * (360 / scanArea)); //Number of points rendered at any given time ste[
        lidarDataDict = new ScannerData(numOfPoints);
        rotation = new Vector3(0, scanArea, 0); 
    }

    //Queries each laser for their impact location and stores values
    void updateLaserImpactLocations() {
        for (int i = 0; i < laserArray.Length; i++) {
            laserImpactLocs[i] = laserArray[i].getRay();
        }
    }

    //Returns scans per second based on physics deltaTime.
    float calculateScansPerSecond() {
        return laserChannels* 1/Time.fixedDeltaTime;
    }

    //Spawning laserLine objects and assigning its position, rotation and parent transform.
    LaserLine spawnLaserBeam(Vector3 tiltAngle) {
        LaserLine spawn = Instantiate<LaserLine>(laserLinePrefab);
        spawn.transform.localPosition = transform.position;
        spawn.transform.localRotation = Quaternion.Euler(tiltAngle);
        spawn.transform.parent = gameObject.transform;

        return spawn;    
    }

    
}
