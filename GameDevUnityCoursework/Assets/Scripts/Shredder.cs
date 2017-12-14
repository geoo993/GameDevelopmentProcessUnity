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
    
    
    private float forwardSpeed = 0.0f;
    
    private GameObject[] ramps;
    private GameObject[] boxes;
    private GameObject[] blocks;

    private bool damagePlayer = true;
    
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
            float offset = (size.z + size.z) * 0.75f;
			float boundary = block.transform.position.z + offset;
            if (transform.position.z > boundary){
                Destroy(block);
            }
        }
        
    }
    
    public void SetSpeed(float speed){
        forwardSpeed = speed;
    }
   
	// Update is called once per frame
	void Update () {

        if (GameManager.endOfAsteroidAttack == false)
        {
            transform.Translate(0.0f, 0.0f, forwardSpeed);
			
            //ShredBoxes();
			//ShredRamps();
            ShredBlocks();
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        //print(collision.gameObject.name);
        if (damagePlayer)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.name == "HoverboardBodyBlue" || collision.gameObject.name == "HoverboardBodyGreen")
            {

                FindObjectOfType<GameManager>().SetHealth(100.0f, false);
            }

            damagePlayer = false;
        }
    
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
