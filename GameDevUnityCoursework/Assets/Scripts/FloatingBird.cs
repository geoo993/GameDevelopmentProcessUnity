using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=6-pJu0GwK5k

public class FloatingBird : MonoBehaviour {
    
    [Range(1.0f, 200.0f)] public float movementSpeed = 100.0f;
    [Range(1.0f, 200.0f)] public float rotationSpeed = 100.0f;
    [Range(1.0f, 20.0f)] public float verticalSpeed;
    [Range(1.0f, 5.0f)] public float groundOffset;
    [Range(1.0f, 10.0f)] public float amplitude;
    public bool autoMove;

    private Rigidbody rigidBody{
        get {
            return GetComponent<Rigidbody>();
        }
    }

	void Start () {
    
	}
	
	void FixedUpdate () {
    
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        transform.Rotate(0.0f, x, 0.0f);

        
        if (autoMove)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime); // auto move forward 
        }
        else
        {
			float moveForward = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed; // manuel move forward
			transform.Translate(0.0f, 0.0f, moveForward);
        }
        
		float hovering = (amplitude + (Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude)) + groundOffset; // hovering
        transform.position = new Vector3(transform.position.x, hovering, transform.position.z);
	}
}
