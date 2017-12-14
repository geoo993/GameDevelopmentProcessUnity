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

    public enum ItemType { None, BlueCrystal, RedCrystal, GreenCrystal, Trophy, Gold, Coin, CandyBar, ShieldBooster, SpeedBooster };
    public ItemType itemType = ItemType.None;
    
    public enum CollectionType { Points, Collectable, Health, Speed, Shield, Bonus };
    public CollectionType collectionType = CollectionType.Points;

    [Range(1, 5000)]
    public int value;

    void Update()
    {
        transform.Rotate(0.0f, 1.0f, 0.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.name == "HoverboardBodyBlue" || other.gameObject.name == "HoverboardBodyGreen"){

            switch (collectionType) {
            case CollectionType.Points: 
                FindObjectOfType<GameManager>().SetScore(value, gameObject.name);
                    break;
            case CollectionType.Collectable:
                FindObjectOfType<GameManager>().SetCrystalsCollected(value, gameObject.name);
                    break;
            case CollectionType.Health:
                FindObjectOfType<GameManager>().SetHealth(value, true);
                    break;
            case CollectionType.Speed:
                FindObjectOfType<GameManager>().SetSpeedBooster(value, 10.0f);
                    break;
            case CollectionType.Shield:
                FindObjectOfType<GameManager>().SetShieldBooster(value, 10.0f);
                    break;
            case CollectionType.Bonus:
                FindObjectOfType<GameManager>().SetBonus(value, gameObject.name);
                    break;
                    
           }
            
            Destroy(gameObject);
        }
    }
    
}
