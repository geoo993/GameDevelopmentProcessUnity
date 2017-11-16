using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHover : MonoBehaviour {

    public float thrusterStrength;
    
    
    private Rigidbody rigidBody{
        get
        {
            return GetComponent<Rigidbody>();
        }
    }

    private float thrusterDistance = 5.0f;

    /*
	void FixedUpdate () 
    {
    
       
        RaycastHit hit;

        Vector3 downwardForce;
        float distancePercentage;


        if (Physics.Raycast(transform.position, transform.up * -1.0f, out hit, thrusterDistance))
        {

            // the thruster is within thrusterDistance to the ground. How far away?
            distancePercentage = 1.0f - (hit.distance / thrusterDistance);

            //calculate how much force to push:
            downwardForce = transform.up * thrusterStrength * distancePercentage;

            //correct the force for the mass of the car and deltatime:
            downwardForce = rigidBody.mass * downwardForce * Time.deltaTime;


            //apply the force where the thruster is :
            rigidBody.AddForceAtPosition(downwardForce, transform.position);

        }
        
    }
    */



    void OnCollisionEnter(Collision collision)
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        print("Trigger entered "+ other.gameObject.name);
    }
}
