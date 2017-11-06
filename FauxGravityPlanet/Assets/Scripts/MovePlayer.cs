using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    private Animator anim;
    [SerializeField, Range(1.0f, 100.0f)] float moveSpeed = 20.0f;
    
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {


        anim.speed = moveSpeed * 2.0f;
  
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); // auto move forward 
            
        if (Input.GetKeyDown(KeyCode.Space)){
            anim.SetTrigger("Jump");
        }
	}
}
