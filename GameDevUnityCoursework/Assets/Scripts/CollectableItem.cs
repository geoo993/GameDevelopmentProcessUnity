using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour {
    
    
    private Rigidbody rigidBody{
        get
        {
            return GetComponent<Rigidbody>();
        }
    }

    public enum ItemType { None, BlueCrystal, RedCrystal, GreenCrystal, Trophy, Gold, Coin };
    public ItemType itemType = ItemType.None;

    void Update()
    {
        transform.Rotate(0.0f, 1.0f, 0.0f);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.name == "HoverboardBodyBlue" || other.gameObject.name == "HoverboardBodyGreen"){
            Destroy(gameObject);
        }
    }
    
}
