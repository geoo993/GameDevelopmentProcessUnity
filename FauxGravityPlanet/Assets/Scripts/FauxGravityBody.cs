using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=gHeQ8Hr92P4
public class FauxGravityBody : MonoBehaviour {

    public enum BodyType { StaticBody = 0, MovingBody = 1 }
    public BodyType body = BodyType.MovingBody;
    
    public FauxGravityAttractor attractor;
    [Range(1.0f, 10.0f)] public float jumpSpeed = 2.0f;
    [Range(20.0f, 100.0f)] public float rotationSpeed = 50.0f;
    private Transform myTransform;

    public bool isGrounded = false;
    private bool isJump = false;

    void Start()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        this.GetComponent<Rigidbody>().useGravity = false;
        myTransform = transform;
    }

    void Update () {

        if (body == BodyType.MovingBody)
        {
            Jump();

            float jumping = isJump ? -jumpSpeed : 1.0f;
			attractor.Attract(myTransform, jumping, rotationSpeed);
            
        }else{
            attractor.Attract(myTransform, 1.0f, rotationSpeed);
        }
        
    }
    
    void Jump(){

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJump = true;
            }

        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJump = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        print("collision with "+collision.gameObject.name);
        if (collision.gameObject.name == "Planet" || collision.gameObject.name == "World"){
            print("on planet");
            isGrounded = true;
        }
        
    }

    void OnCollisionExit(Collision collision)
    {
        print("collided with "+collision.gameObject.name);
        if (collision.gameObject.name == "Planet" || collision.gameObject.name == "World" ){
            print("off planet");
            isGrounded = false;
        }
    }

}
