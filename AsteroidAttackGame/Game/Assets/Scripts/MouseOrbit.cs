﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOrbit : MonoBehaviour {

    public Transform target = null;
    public float distance = 10.0f;

    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20.0f;
    public float yMaxLimit = 80.0f;

    private float x = 0.0f;
    private float y = 0.0f;
    private bool stopTrackingMouse = false;

    /*
    This is the same as the script from Unity's Standard Assets
    With one addition to allow for zooming with mouse wheel
*/
	// Use this for initialization
	void Start () {

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;


        // Make the rigid body not change rotation, if we have one
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        
    }

    void Update()
    {
        Invoke("StopTrackingMousePosition", 5);
    }

    void LateUpdate () {
    
        if (target) {

            if (stopTrackingMouse == false)
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }
            
            distance += Input.mouseScrollDelta.y * 0.2f; // Line added
            Vector3 offset = new Vector3(0.0f, 0.0f, -distance);
            
            y = ClampAngle(y, yMinLimit, yMaxLimit);
                   
            var rotation = Quaternion.Euler(y, x, 0.0f);
            var position = rotation * offset + target.position;
            
            transform.rotation = rotation;
            transform.position = position;
        }
        
    }

    static float ClampAngle (float angle, float min, float max) {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp (angle, min, max);
    }
    
    void StopTrackingMousePosition(){
        stopTrackingMouse = true;
    }

}
