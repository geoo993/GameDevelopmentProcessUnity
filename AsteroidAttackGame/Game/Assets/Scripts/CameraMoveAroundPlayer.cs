using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveAroundPlayer : MonoBehaviour {

    public float sensitivity = 0.05f;
    public Transform player;
    
	// Update is called once per frame
	void Update () {
    /*
		Camera cam = GetComponent<Camera>();
 
        Vector3 vp = cam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        vp.x -= 0.5f;
        vp.y -= 0.5f;
        vp.x *= sensitivity;
        vp.y *= sensitivity;
        vp.x += 0.5f;
        vp.y += 0.5f;
        Vector3 sp = cam.ViewportToScreenPoint(vp);
        
        Vector3 v = cam.ScreenToWorldPoint(sp);
        transform.LookAt(v, Vector3.up);
        */
        
        /*
        Camera cam = GetComponent<Camera>();
 
        Vector3 vp = cam.ScreenToViewportPoint(new Vector3(
            Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane
        ));
        
        vp.x -= 0.5f;
        vp.y -= 0.5f;
        vp.x *= sensitivity;
        vp.y *= sensitivity;
        vp.x += 0.5f;
        vp.y += 0.5f;
        Vector3 sp = cam.ViewportToScreenPoint(vp);


        var playerCoord = cam.WorldToViewportPoint(player.position);
        Debug.Log(playerCoord);
        if (
            playerCoord.x < 0.1f || playerCoord.x > 0.9f || 
            playerCoord.y < 0.1f || playerCoord.y > 0.9f
        ) 
        return; 
               
        Vector3 v = cam.ScreenToWorldPoint(sp);
        transform.LookAt(v, Vector3.up);
        */
        
	}
}
