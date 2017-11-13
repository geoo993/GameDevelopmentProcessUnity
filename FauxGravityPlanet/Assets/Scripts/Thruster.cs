using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {


    public float thrusterStrength;
    public bool enableJump;

    private bool IsJump = false;
    private float distanceFromGroundCurve = 0.0f;
    private float ceiling = 10.0f;
    private float jumpStrength = 5.0f;
    
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
            IsJump = true;
            if (distanceFromGroundCurve > 4.0f){
                IsJump = false;
            }
        }

        if (IsJump)
        {
            distanceFromGroundCurve += 4.0f;
            IsJump = false;
        }
		
        distanceFromGroundCurve -= 10.0f * Time.fixedDeltaTime;
        

        if (distanceFromGroundCurve <= 0.0f){
            distanceFromGroundCurve = 0.0f;
        }
        float maxDistance = thrusterDistance + distanceFromGroundCurve;
    
    
        RaycastHit hit;
      
        Vector3 downwardForce;
        float distancePercentage;


        if (Physics.Raycast (transform.position, transform.up * -1.0f, out hit, maxDistance)) {

            // the thruster is within thrusterDistance to the ground. How far away?
            distancePercentage = 1.0f - (hit.distance / maxDistance);

            //calculate how much force to push:
            downwardForce = transform.up * thrusterStrength * distancePercentage;

            //correct the force for the mass of the car and deltatime:
            downwardForce = rigidBody.mass * downwardForce * Time.deltaTime ;


            //apply the force where the thruster is :
            rigidBody.AddForceAtPosition(downwardForce, transform.position);


            if (enableJump)
            {
                if (Input.GetButtonDown("Jump"))
                {

                    float upForce = 1.0f - Mathf.Clamp(rigidBody.transform.position.y / ceiling, 0.0f, 1.0f);
                    upForce = Mathf.Lerp(0.0f, jumpStrength, upForce) * rigidBody.mass;

                    Vector3 jumpForce = Physics.gravity * upForce * Time.deltaTime;
                    rigidBody.AddForceAtPosition(-jumpForce, transform.position, ForceMode.Impulse);

                }
            }

            
        }
        

    }

}
