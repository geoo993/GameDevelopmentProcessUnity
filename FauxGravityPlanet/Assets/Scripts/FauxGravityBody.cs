using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=gHeQ8Hr92P4
public class FauxGravityBody : MonoBehaviour {

    public enum BodyType { StaticBody = 0, MovingBody = 1 }
    public BodyType body = BodyType.MovingBody;
    
    public FauxGravityAttractor attractor;
    public float jumpSpeed = 20.0f;
    private Transform myTransform;

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

            if (isJump)
            {
                attractor.Jump(myTransform, jumpSpeed);
            }
            else
            {
                attractor.Attract(myTransform);
            }
        }else{
            attractor.Attract(myTransform);
        }
        
    }
    
    void Jump(){
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJump = false;
        }
    }
    
    
}
