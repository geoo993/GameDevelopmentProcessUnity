using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// http://answers.unity3d.com/questions/1316251/making-the-camera-follow-the-mouse-but-stay-near-t.html

public class PlayerController : MonoBehaviour {


    //private Animator anim;
     
    private Camera mainCamera;
    public GameObject cam;
     
    public float moveSpeed = 10.0f;
    public float jumpSpeed = 5.0f;
    private Vector3 moveDir;

    private Rigidbody rigidBody;
    
    void Awake()
    {
         //anim = GetComponent<Animator>();
    }
         
    void Start(){
        mainCamera = cam.GetComponent<Camera>();
    }
    
    void Update () {
    
        Move();
        //Jump();
        
    }

    void FixedUpdate()
    {
		rigidBody = this.GetComponent<Rigidbody>();
        rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);

    }
    
    
     void Move()
     {
		moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;
        //moveDir = new Vector3(0.0f, 0.0f, Input.GetAxisRaw("Vertical")).normalized;
    
     }
 
    
     void Jump()
     {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //print("Jump");
            rigidBody.velocity += jumpSpeed * Vector3.up * Time.deltaTime;
            //rigidBody.AddForce(Vector3.up * jumpSpeed * Time.deltaTime);
        }
    }


}

