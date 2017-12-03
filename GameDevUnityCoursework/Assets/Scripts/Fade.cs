using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    [SerializeField, Range(0.0f, 10.0f)] float fadeInTime;
    private Image fadePanel;
    private Color color = Color.black;
    
	void Start () {
		fadePanel = GetComponent<Image>();
        gameObject.SetActive(true);
	}
	
	void Update () {
        if (Time.timeSinceLevelLoad < fadeInTime){
            //fade
            float alphaChange = Time.deltaTime / fadeInTime;
            color.a -= alphaChange;
            fadePanel.color = color;
            
        }else{
            gameObject.SetActive(false);
        }
        
	}
}
