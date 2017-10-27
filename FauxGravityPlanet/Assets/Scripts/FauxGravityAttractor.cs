﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {

    public float gravity = -10.0f;
    [Range(20.0f, 100.0f)] public float rotationSpeed = 50.0f;
    
    public void Jump(Transform body, float jumpSpeed){
    
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
        
        //body.GetComponent<Rigidbody>().velocity += jumpSpeed * bodyUp * Time.deltaTime;
        body.GetComponent<Rigidbody>().AddForce(bodyUp * jumpSpeed);
    }
    
    public void Attract(Transform body){
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
        
        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
        
        // give us the rotation between these two vectors or up positions
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp);

        // we want to add the body current rotation to the targetRotation
        Quaternion rotation = targetRotation * body.rotation;

        // we want to make these bodies rotation move towards this target rotation smoothly
        // basically lerp, also known as spherical interpolation
        // we want to go from the body current rotation to the target rotation
        // also be frame rate inderpendent by using Time.deltaTime
        body.rotation = Quaternion.Slerp(body.rotation, rotation, rotationSpeed * Time.deltaTime); 
    }
    
}
