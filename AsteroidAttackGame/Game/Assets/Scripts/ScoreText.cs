using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    private int scoreValue;
    
	// Use this for initialization
	public void SetScore (int value) {
        scoreValue += value;
        GetComponent<Text>().text = scoreValue.ToString();
	}
	
}
