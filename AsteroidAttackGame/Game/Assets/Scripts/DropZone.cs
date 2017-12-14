using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DropZone : MonoBehaviour {

    public float xmin;
    public float xmax;
    [Range(0.0001f, 0.0005f)] public float speed;

    private bool stopDropping = false;
    
	 private Rigidbody rigidBody
    {
        get {
            return GetComponent<Rigidbody>();
        }

    }
    void FixedUpdate()
    {
        if (transform.position.y > 0.0f && stopDropping == false)
        {
            float dropZoneArea = Random.Range(xmin, xmax);
            Vector3 otherPosition = new Vector3(dropZoneArea, 0.0f, transform.position.z);
            rigidBody.velocity += speed * Time.fixedTime * (otherPosition - transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lane1" || 
            collision.gameObject.tag == "Lane2" || 
            collision.gameObject.tag == "Lane3" || 
            collision.gameObject.tag == "Lane4" ||
            collision.gameObject.tag == "Ramp") {

            stopDropping = true;
         }
    }
}
