using UnityEngine;
using System.Collections;

public class SpherePoint : MonoBehaviour {

    public Points parentObject;
    public int index;
    public GameObject player;
    private bool IsPlacing = false;

	// Update is called once per frame
	void Update () {
        if (IsPlacing)
        {
            transform.position = player.transform.position + Vector3.forward;
        }

        if (gameObject.transform.hasChanged)
        {
            parentObject.updatePoint(index);
            gameObject.transform.hasChanged = false;
        }
        
	}

    public void RemovePoint()
    {
        parentObject.RemovePosition(index);
        Destroy(gameObject);
    }

    public void SnapToPlayer()
    {
        IsPlacing = !IsPlacing;
    }

    void OnDestroy()
    {
        parentObject.updateChildrenIndices();
        print("Script was destroyed");
    }
}
