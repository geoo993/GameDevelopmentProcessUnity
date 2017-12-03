using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHover : MonoBehaviour {
    
    private Rigidbody rigidBody{
        get
        {
            return GetComponent<Rigidbody>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //print("Trigger entered "+ other.gameObject.name);
    }
}
