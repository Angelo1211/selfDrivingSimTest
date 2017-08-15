using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PointCloud : MonoBehaviour {

    private Mesh mesh;
    private ScannerData data;
    private int numPoints;
    private int[] indecies;
    private Color[] colors;
    private Vector3[] points;

    // Use this for initialization
    void Start() {
        mesh = new Mesh();
        data = GameObject.Find("Scanner").GetComponent<Scanner>().dataDict;
        numPoints = data.size;
        GetComponent<MeshFilter>().mesh = mesh;
        CreateMesh();
    }
    private void Update() {
        updateMesh();
    }

    void updateMesh() {

        points = data.returnDictAsArray();
        for (int i = 0; i < points.Length; ++i) {
            float mag = points[i].magnitude;
            colors[i] = new Color((points[i].x/mag)+0.5f, (points[i].y/mag)+0.5f, 0, 1.0f);
        }
        
        mesh.vertices = points;
        mesh.colors = colors;
    }

    void CreateMesh() {
        points = new Vector3[numPoints];
        indecies = new int[numPoints];
        colors = new Color[numPoints];
        points = data.returnDictAsArray();
        for (int i = 0; i < points.Length; ++i) {
            indecies[i] = i;
            colors[i] = Color.white;
        }

        mesh.vertices = points;
        mesh.colors = colors;
        mesh.SetIndices(indecies, MeshTopology.Points, 0);

    }
}
