using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour {

    [Range(0.1f, 50f)]
    public float rotationFrequency;

    

    [Range(0.08f, 20f)]
    public float horizontalAngularRes;
    [Range(0.4f, 26.9f)]
    public float verticalAngularRes;

    public LaserLine laserLinePrefab;

    public Gradient coloring;

    float[] verticalFOVRange = {-15f,15f};
    float totalVertFOV = 0f;
    float[] horizontalFOVRange = { 0f, 360f };
    float totalHorFOV = 0f;
    [SerializeField]
    float scansPerSecond = 0;
    [SerializeField]
    public float scanArea;
    int scansPerSlice = 0;
    int numSlice = 0;
    Vector3 tiltAngle;
    Transform transform;
    int count = 0;

	// Use this for initialization
	void Start () {
        
        transform = gameObject.GetComponent<Transform>();
        InitializeLidarScan();
        createLidarScan();
	}
	
	// Update is called once per frame
	void Update () {
        scanArea = 360 * rotationFrequency * Time.deltaTime;
        Vector3 rotation = new Vector3(0, scanArea);

        transform.Rotate(rotation);
        scansPerSecond = calculateScansPerSecond();

    }

    float calculateScansPerSecond() {
        return 64* 1/Time.deltaTime;
    }

    void spawnLaserBeam() {
        LaserLine spawn = Instantiate<LaserLine>(laserLinePrefab);
        spawn.transform.localPosition = transform.position;
        spawn.transform.localRotation = Quaternion.Euler(tiltAngle);
        spawn.transform.parent = gameObject.transform; 
        
            
    }

    void createLidarScan() {
        
        for (int i = 0; i < numSlice; i++) {
            for (int j = 0; j < scansPerSlice; j++) {

                tiltAngle = new Vector3(verticalFOVRange[0]+j*verticalAngularRes, i*horizontalAngularRes, 0);
                spawnLaserBeam();

            }
        }
        
    }
    
    void InitializeLidarScan() {
        totalHorFOV = Mathf.Abs(horizontalFOVRange[1] - horizontalFOVRange[0]);
        totalVertFOV = Mathf.Abs(verticalFOVRange[1] - verticalFOVRange[0]);
        scansPerSlice = (int)Mathf.Floor(totalVertFOV / verticalAngularRes);
        numSlice = 1;//(int)Mathf.Floor(totalHorFOV / horizontalAngularRes);
        
    }
}
