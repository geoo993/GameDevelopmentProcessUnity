using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalsCollection : MonoBehaviour {

	private int crystalsValue;
    
    // Use this for initialization
    public void SetCrystalsCollected (int value) {
        crystalsValue += value;
        GetComponent<Text>().text = crystalsValue.ToString();
    }
}
