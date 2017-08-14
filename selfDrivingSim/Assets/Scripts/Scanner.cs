using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour {

    [Range(0.1f, 50f)]
    public float rotationFrequency;
    public float verticalAngularRes;

    public LaserLine laserLinePrefab;

    public Gradient coloring;

    float[] verticalFOVRange = {-15f,15f};
    float totalVertFOV = 0f;
    float[] horizontalFOVRange = { 0f, 360f };
    float totalHorFOV = 0f;
    public float scansPerSecond = 0;
    [SerializeField]
    public float scanArea;
    int scansPerSlice = 0;
    Vector3 tiltAngle;
    Transform transform;
    int count = 0;
    public ScannerData dataDict;
    LaserLine[] laserArray;
    Vector3[] laserPositions;
    

	// Use this for initialization
	void Start () {
        scansPerSlice = 64;
        verticalAngularRes = 0.4f;
        transform = gameObject.GetComponent<Transform>();
        createLidarScan();
        dataDict = new ScannerData();
        InvokeRepeating("OutputScans", 1.0f, 1.0f);
    }
	
	void FixedUpdate () {
        scanArea = 360 * rotationFrequency * Time.deltaTime;    
        Vector3 rotation = new Vector3(0, scanArea);
        transform.Rotate(rotation);

        dataDict.addPointsAtAngle(transform.localRotation.eulerAngles[1], laserImpactLocations(laserArray));
        scansPerSecond = calculateScansPerSecond();
        count++;

    }

    Vector3[] laserImpactLocations(LaserLine[] laserArray) {
        for (int i = 0; i < laserArray.Length; i++) {
            laserPositions[i] = laserArray[i].endPosition;
        }
        return laserPositions;
    }



    void OutputScans() {
        Debug.Log(count);
        count = 0;
    }

    float calculateScansPerSecond() {
        return 64* 1/Time.deltaTime;
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
