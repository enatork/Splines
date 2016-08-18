using UnityEngine;
using System;
using System.Collections.Generic;

public class Points : MonoBehaviour
{

    // Update is called once per frame
    private Color c1 = Color.yellow;
    private Color c2 = Color.red;

    private List<Vector3> points;
    LineRenderer lineRenderer;
    private bool IsPlacing = false;

    void Start()
    {
        points = new List<Vector3>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.enabled = false;
    }

    public void CreateLine(Transform p1, Transform p2)
    {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer.enabled == false)
        {
            lineRenderer.enabled = true;
        }
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetVertexCount(points.Count);
        lineRenderer.SetPositions(points.ToArray());
    }

    public void AddPosition(Transform p1)
    {
        points.Add(p1.position);
    }

    public void RemovePosition(int index)
    {
        points.RemoveAt(index);
        lineRenderer.SetPositions(points.ToArray());
    }

    public void updateChildrenIndices()
    {
        SpherePoint[] spherePoints = gameObject.GetComponentsInChildren<SpherePoint>();
        for (int i = 0; i < spherePoints.Length; i++) {
            spherePoints[i].index = i;
        }
    }

    public void updatePoint(int index)
    {
        Transform child = gameObject.transform.GetChild(index).transform;
        points[index] = child.position;
        lineRenderer.SetPositions(points.ToArray());
    }
}
