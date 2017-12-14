using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    public float damageToPlayer;
    public bool autoDestroy = true;

    private bool damagePlayer = true;
    
    void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.tag == "Player" || collision.gameObject.name == "HoverboardBodyBlue" || collision.gameObject.name == "HoverboardBodyGreen"){

            if (damagePlayer)
            {
                FindObjectOfType<GameManager>().SetHealth(damageToPlayer, false);
                
                if (autoDestroy){
                    Destroy(gameObject);
                }
                
                damagePlayer = false;
            }
        }
        
    }
    
}
