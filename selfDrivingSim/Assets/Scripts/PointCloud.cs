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
        numPoints =(int)GameObject.Find("Scanner").GetComponent<Scanner>().scansPerSecond;
        GetComponent<MeshFilter>().mesh = mesh;
        CreateMesh();
    }
    private void Update() {
        data = GameObject.Find("Scanner").GetComponent<Scanner>().dataDict;
        numPoints = (int)GameObject.Find("Scanner").GetComponent<Scanner>().scansPerSecond;
        updateMesh();
    }

    void updateMesh() {
        points = data.returnDictAsArray();
        mesh.vertices = data.returnDictAsArray();
        mesh.SetIndices(indecies, MeshTopology.Points, 0);

        for (int i = 0; i < points.Length; ++i) {
            indecies[i] = i;
            colors[i] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
        }

        mesh.vertices = points;
        mesh.colors = colors;
    }

    void CreateMesh() {
        Vector3[] points = new Vector3[numPoints];
        int[] indecies = new int[numPoints];
        Color[] colors = new Color[numPoints];
        points = data.returnDictAsArray();
        for (int i = 0; i < points.Length; ++i) {
            indecies[i] = i;
            colors[i] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
        }

        mesh.vertices = points;
        mesh.colors = colors;
        mesh.SetIndices(indecies, MeshTopology.Points, 0);

    }
}
