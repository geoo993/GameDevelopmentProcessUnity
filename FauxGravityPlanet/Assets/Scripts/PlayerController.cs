using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// http://answers.unity3d.com/questions/1316251/making-the-camera-follow-the-mouse-but-stay-near-t.html
// https://unity3d.com/learn/tutorials/topics/multiplayer-networking/creating-player-movement-single-player

public class PlayerController : MonoBehaviour {

    private Camera mainCamera;
    public GameObject cam;
    
    public float moveSpeed = 10.0f;
    private Vector3 moveDirection;

    private Rigidbody rigidBody;
	private FauxGravityBody body;
    
    void Awake()
    {
         //anim = GetComponent<Animator>();
    }
         
    void Start(){
        mainCamera = cam.GetComponent<Camera>();
        body = this.GetComponent<FauxGravityBody>();
    }
    
    void Update () {
    
        Move();
        
        MoveTurn();
        
    }

    void FixedUpdate()
    {
		rigidBody = this.GetComponent<Rigidbody>();
        //rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
       
    }


    void Move()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;

    }
    
    void MoveTurn(){
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
		
		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
     
     }
 
   


}

