using UnityEngine;
using System;


public class SplineDecorator : MonoBehaviour
{

    public BezierSpline spline;

    public int frequency;

    public bool lookForward;

    public Transform[] items;

    private Transform[] spawnedItems;

    private int splinePointsLength;

    private void Awake()
    {
        spawnedItems = new Transform[frequency];
        splinePointsLength = spline.NumberOfPoints;
        Decorate();
    }

    private void Update()
    {
        if (splinePointsLength < spline.NumberOfPoints) {
            for (int f = 0; f < spawnedItems.Length; f++)
            {
                Destroy(spawnedItems[f].gameObject);
            }
            Array.Clear(spawnedItems, 0, spawnedItems.Length);
            Decorate();
        }

        if (spline.transform.hasChanged) {
            for (int f = 0; f < spawnedItems.Length; f++)
            {
                Destroy(spawnedItems[f].gameObject);
            }
            Array.Clear(spawnedItems, 0, spawnedItems.Length);
            Decorate();
            spline.transform.hasChanged = false;
        }
    }

    private void Decorate()
    {
        if (frequency <= 0 || items == null || items.Length == 0)
        {
            return;
        }
        float stepSize = frequency * items.Length;
        if (spline.Loop || stepSize == 1)
        {
            stepSize = 1f / stepSize;
        }
        else
        {
            stepSize = 1f / (stepSize - 1);

        } int p = 0;
        for (int f = 0; f < frequency; f++)
        {
            for (int i = 0; i < items.Length; i++)
            {
                p++;
                Transform item = Instantiate(items[i]) as Transform;
                Vector3 position = spline.GetPoint(p * stepSize);
                item.transform.localPosition = position;
                if (lookForward)
                {
                    item.transform.LookAt(position + spline.GetDirection(p * stepSize));
                }
                item.transform.parent = transform;
                spawnedItems[f] = item;
            }
        }
    }
}
