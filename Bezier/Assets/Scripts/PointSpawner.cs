using UnityEngine;
using System.Collections;

public class PointSpawner : MonoBehaviour
{

    public Points points;
    public SpherePoint pointBody;
    public GameObject FocusedObject { get; private set; }
    private Transform previousPoint;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;
            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
            {
                FocusedObject = hitInfo.collider.gameObject;
                FocusedObject.SendMessage("SnapToPlayer");
            }
            else
            {
                FocusedObject = null;
                SpherePoint p = (SpherePoint)Instantiate(pointBody, transform.position, transform.rotation);
                p.transform.parent = points.transform;
                p.parentObject = points;
                p.index = points.transform.childCount - 1;
                p.player = this.gameObject;
                points.AddPosition(p.transform);
                if (points.transform.childCount > 1)
                {
                    points.CreateLine(points.transform.GetChild(0), p.transform);
                }
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;
            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
            {
                FocusedObject = hitInfo.collider.gameObject;
                FocusedObject.SendMessage("RemovePoint");
            }
        }

    }
}
