using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Shredder : MonoBehaviour {

    private BoxCollider boxCollider {
        get{
            return GetComponent<BoxCollider>();
        }
    }
    
    [SerializeField, Range(0.0f, 10.0f)] float forwardSpeed;
    
    private GameObject[] ramps;
    private GameObject[] boxes;
    private GameObject[] blocks;
    
    void ShredRamps(){
        ramps = GameObject.FindGameObjectsWithTag("Ramp");
        foreach (GameObject ramp in ramps){
            if (transform.position.z > ramp.transform.position.z){
                Destroy(ramp);
            }
        }
    }
    
    void ShredBoxes(){
        boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject box in boxes){
            if (transform.position.z > box.transform.position.z){
                //Destroy(box);
            }
        }
        
    }
    
    void ShredBlocks(){
        Vector3 size = boxCollider.bounds.size;
        blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks){
			float boundary = block.transform.position.z + size.z;
            if (transform.position.z > boundary){
                Destroy(block);
            }
        }
        
    }
   
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0.0f, 0.0f, forwardSpeed));

        ShredRamps();
        ShredBoxes();
        ShredBlocks();
    }

    void OnCollisionEnter(Collision collision)
    {
		//print(collision.gameObject.name);
        
        /*
        if (collision.gameObject.tag != "Wall" ||
        collision.gameObject.tag != "Lane1" ||
        collision.gameObject.tag != "Lane2" ||
        collision.gameObject.tag != "Lane3" ||
        collision.gameObject.tag != "Lane4"  
        ){
        
			Destroy(collision.gameObject);
        }
        */
    
    }

    void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.tag);
        //Destroy(other.gameObject);
    }


    void OnTriggerExit(Collider other)
    {
        
    }
    
    
}
