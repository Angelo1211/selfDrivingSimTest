//Author: Angel Ortiz
//Date: 08/15/17

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Class that wraps dictionary of Vector3 arrays accessed with int keys representing the angle of scan.
//Used to store lidar data.
public class ScannerData {

    Dictionary<int, Vector3[]> ScannerPointsDict;

    public int size;
    Vector3[] points;

	public ScannerData(int count) {
        ScannerPointsDict = new Dictionary<int , Vector3[]>();
        points = new Vector3[count];
        size = count;
    }
        
	//Adds the laser impact location array of a given scan at the current angle.
    //Will overwrite if values already exist.
	public void addPointsAtAngle(int angle, Vector3[] values) {
        Vector3[] impacts = new Vector3[values.Length];
        values.CopyTo(impacts, 0); //Directly copying value to new arrays to avoid reference conflicts.
        ScannerPointsDict[angle] = impacts; 
    }

    //Returns a Vector3 point value at a given angle and laser channel representing distance to laser impact location.
    public Vector3 readPoint(int angle, int channel ) {
        return ScannerPointsDict[angle][channel];
    }
    //Copies all the contents of the dictionary into an array of the size of all of its values. 
    public Vector3[] returnDictAsArray() {
        int count = 0;
        foreach(KeyValuePair<int, Vector3[]> entry in ScannerPointsDict) {
            foreach(Vector3 point in entry.Value) {
                points[count++] = point;
            }
        }
        return points;
    }


}
