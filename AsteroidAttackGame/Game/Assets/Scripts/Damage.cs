using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public float damageToPlayer;
    public bool autoDestroy = true;

    private bool damagePlayer = true;

    GameManager gameManager
    {
        get
        {
            return FindObjectOfType<GameManager>();
        }
    }
                
    
    void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.tag == "Player" || collision.gameObject.name == "HoverboardBodyBlue" || collision.gameObject.name == "HoverboardBodyGreen"){

            if (damagePlayer)
            {
                if (gameManager.useShield == false)
                {
                    gameManager.SetHealth(damageToPlayer, false);
                }
                
                if (autoDestroy){
                    Destroy(gameObject);
                }
                
                damagePlayer = false;
            }
        }
        
    }
    
}
