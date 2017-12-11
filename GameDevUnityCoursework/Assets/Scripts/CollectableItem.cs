using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableItem : MonoBehaviour {
    
    
    private Rigidbody rigidBody{
        get
        {
            return GetComponent<Rigidbody>();
        }
    }

    public enum ItemType { None, BlueCrystal, RedCrystal, GreenCrystal, Trophy, Gold, Coin };
    public ItemType itemType = ItemType.None;
    
    public enum CollectionType { Points, Collectable };
    public CollectionType collectionType = CollectionType.Points;

    [Range(1, 20)]
    public int scoreValue;

    private int collectionValue = 1;
    
    private int totalCollected = 0;

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

            switch (collectionType) {
            case CollectionType.Points: 
                FindObjectOfType<GameManager>().SetScore(scoreValue, gameObject.name);
                    break;
            case CollectionType.Collectable:
                FindObjectOfType<GameManager>().SetCrystalsCollected(collectionValue, gameObject.name);
                    break;
            default:
                    break;
           }
            
            Destroy(gameObject);
        }
    }
    
}
