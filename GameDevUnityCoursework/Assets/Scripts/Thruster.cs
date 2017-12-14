using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {


    public float thrusterStrength;
    public bool enableJump;

    private bool IsJump = true;
    private float distanceFromGroundCurve = 0.0f;
    
    private GameObject parent {
        get{
            return transform.parent.gameObject;
        }
    }
    private Rigidbody rigidBody{
        get
        {
            return parent.GetComponent<Rigidbody>();
        }
    }
    
    private float thrusterDistance{
        get {
            return parent.GetComponent<HoverController>().distanceFromGround;
        }
    }
    
    
    void FixedUpdate () 
    {
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsJump){
                
                distanceFromGroundCurve = 2.0f;
				rigidBody.AddForce(Vector3.up * (distanceFromGroundCurve) * rigidBody.mass, ForceMode.Impulse);
                //rigidBody.AddForce(Vector3.up * (distanceFromGroundCurve));
                IsJump = false;
            }
            
        }

        distanceFromGroundCurve -= 150.0f * Time.fixedDeltaTime;
        
        if (distanceFromGroundCurve <= 0.0f){
            distanceFromGroundCurve = 0.0f;
            IsJump = true;
        }
        float maxDistance = thrusterDistance + distanceFromGroundCurve;


        if (IsJump == true)
        {

            RaycastHit hit;

            Vector3 downwardForce;
            float distancePercentage;


            if (Physics.Raycast(transform.position, transform.up * -1.0f, out hit, maxDistance))
            {

                // the thruster is within thrusterDistance to the ground. How far away?
                distancePercentage = 1.0f - (hit.distance / maxDistance);

                //calculate how much force to push:
                downwardForce = transform.up * thrusterStrength * distancePercentage;

                //correct the force for the mass of the car and deltatime:
                downwardForce = rigidBody.mass * downwardForce * Time.deltaTime;


                //apply the force where the thruster is :
                rigidBody.AddForceAtPosition(downwardForce, transform.position);

            }
        }
        
        

    }

}
