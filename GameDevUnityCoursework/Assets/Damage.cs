using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    public float damageToPlayer;

    private bool damagePlayer = true;
    
    void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.tag == "Player" || collision.gameObject.name == "HoverboardBodyBlue" || collision.gameObject.name == "HoverboardBodyGreen"){

            if (damagePlayer)
            {
                print("Player Hit With damage " + damageToPlayer);
                FindObjectOfType<GameManager>().SetHealth(damageToPlayer);
                //Destroy(gameObject);
                damagePlayer = false;
            }
        }
    }
    
}
