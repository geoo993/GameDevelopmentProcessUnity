﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DropZone : MonoBehaviour {

    public float xmin;
    public float xmax;
    
	 private Rigidbody rigidBody
    {
        get {
            return GetComponent<Rigidbody>();
        }

    }
    void FixedUpdate()
    {
        if (transform.position.y > 0.0f)
        {
            Vector3 otherPosition = new Vector3(Random.Range(xmin, xmax), 0.0f, transform.position.z);
            rigidBody.velocity += 0.0005f * Time.fixedTime * (otherPosition - transform.position);
        }
    }
}
