using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// http://answers.unity3d.com/questions/1316251/making-the-camera-follow-the-mouse-but-stay-near-t.html
// https://unity3d.com/learn/tutorials/topics/multiplayer-networking/creating-player-movement-single-player

public class PlayerController : MonoBehaviour {

    [Range(1.0f, 200.0f)] public float turnSpeed = 100.0f;
    [Range(1.0f, 20.0f)] public float moveSpeed = 5.0f;
    public bool autoMove = true;
    private Vector3 moveDirection;

    private Rigidbody rigidBody;
	private FauxGravityBody body;
 
    void Start(){
        body = this.GetComponent<FauxGravityBody>();
        rigidBody = this.GetComponent<Rigidbody>();
    }
    
    void Update () {
    
        Move();
    }

    void FixedUpdate()
    {
		//rigidBody = this.GetComponent<Rigidbody>();
        //rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
       
    }


    void Move()
    {
        //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;
        moveDirection = Vector3.forward;
        
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed; // direction
        transform.Rotate(0.0f, x, 0.0f);

        if (autoMove)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime); // auto move forward 
        }
        else
        {
			var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed; // manuel move
            transform.Translate(0.0f, 0.0f, z);
        }
     
     }

}

