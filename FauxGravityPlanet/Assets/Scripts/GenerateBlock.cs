using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBlock : MonoBehaviour {

    public GameObject block;
    
    
    private int blockCreated = 2;
    private bool ShouldCollider = true;
    
	

    void GenerateNewBlock(){
        Vector3 blockPosition = new Vector3(0.0f, 0.0f, 100.0f * blockCreated);
        GameObject newBlock = Instantiate(block, blockPosition, Quaternion.identity) as GameObject;
        
        blockCreated += 1;
    }

    void ResetCollider()
    {
        ShouldCollider = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlockCollider"){
            if (ShouldCollider)
            {
				Destroy(other.gameObject);
                GenerateNewBlock();
                ShouldCollider = false;
                Invoke("ResetCollider", 1.0f);
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BlockCollider"){
        
        }
        
    }
}
