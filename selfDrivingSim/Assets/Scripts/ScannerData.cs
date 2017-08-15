using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScannerData {

    Dictionary<int, Vector3[]> ScannerPointsDict;

    public int size;
    Vector3[] points;

	public ScannerData(int count) {
        ScannerPointsDict = new Dictionary<int , Vector3[]>();
        points = new Vector3[count];
        size = count;
    }
        
	
	public void addPointsAtAngle(int angle, Vector3[] values) {
        Vector3[] impacts = new Vector3[values.Length];
        values.CopyTo(impacts, 0);
        ScannerPointsDict[angle] = impacts; 
    }

    public Vector3 readPoint(int angle, int channel ) {
        return ScannerPointsDict[angle][channel];
    }


    //public Vector3[] returnDictAsArray() {
    //    int count = 0;
    //    int[] allKeys = ScannerPointsDict.Keys.ToArray();
    //    for (int i = 0; i < allKeys.Length; i++) { 
    //        for (int j = 0; j < ScannerPointsDict[allKeys[i]].Length; ++j) {
    //            points[count++] = ScannerPointsDict[allKeys[i]][j];
    //        }            
    //    }        
    //    return points;
    //}

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
