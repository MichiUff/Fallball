using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject targetObject;
    private float distanceToTarget;

    // Use this for initialization
    void Start ()
    {
        distanceToTarget = transform.position.y - targetObject.transform.position.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float targetObjectY = targetObject.transform.position.y;
        Vector3 newCameraPosition = transform.position;
        newCameraPosition.y = targetObjectY + distanceToTarget;
        transform.position = newCameraPosition;
    }
}
