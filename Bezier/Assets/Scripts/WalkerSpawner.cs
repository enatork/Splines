using UnityEngine;
using System.Collections;

public class WalkerSpawner : MonoBehaviour
{

    public SplineWalker walker;
    public BezierSpline spline;
    public int maxSpawn = 25;
    float time = 0;

    private int spawned = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 1f && spawned <= maxSpawn)
        {
            SplineWalker splineWalker = (SplineWalker)Instantiate(walker, transform.position, Quaternion.identity);
            splineWalker.spline = spline;
            splineWalker.mode = SplineWalkerMode.Loop;

            time = 0;
            spawned++;
        }
    }


}
