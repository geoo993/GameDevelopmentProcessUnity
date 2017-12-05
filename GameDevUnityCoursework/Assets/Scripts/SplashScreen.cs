using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefsManager.SetMasterVolume(0.5f); // from 0 to 1
        PlayerPrefsManager.SetDifficulty(2); // can only be 1, 2 or 3
        PlayerPrefsManager.SetPlayerBody(0); // can only be 0 or 1
	}

}
