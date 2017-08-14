using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerData {

      Dictionary<float, Vector3[]> ScannerPointsDict;

	public ScannerData() {
        ScannerPointsDict = new Dictionary<float, Vector3[]>();
    }
        
	
	public void addPointsAtAngle(float angle,Vector3[] points) {
        //if (ScannerPointsDict[angle] != null) {
        //    ScannerPointsDict.Remove(angle);
        //}
        ScannerPointsDict[angle] = points;
    }

    public Vector3 readPoint(float angle, int channel ) {
        return ScannerPointsDict[angle][channel];
    }

    public Vector3[] returnDictAsArray() {
        Vector3[] points = new Vector3[ScannerPointsDict.Keys.Count * 64];
        int count = 0;
        foreach (KeyValuePair<float, Vector3[]> entry in ScannerPointsDict) {
            foreach(Vector3 point in entry.Value) {
                points[count++] = point;
            }
        }
        return points;
    }
}
