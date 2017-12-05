using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    public Slider volumeSlider;
    public Slider difficultySlider;
    public Dropdown dropdown;
    public LevelManager levelManager;
    
    
    private MusicManager musicManager{
        get{
            return FindObjectOfType<MusicManager>();
        }
    }
    private GameManager gameManager{
        get {
            return FindObjectOfType<GameManager>();
        }
    }
    
	// Use this for initialization
	void Start () {
        
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        difficultySlider.value = PlayerPrefsManager.GetDifficulty();
        dropdown.value = PlayerPrefsManager.GetPlayerBody();
	}
	
	// Update is called once per frame
	void Update () {
        if (musicManager){
            musicManager.SetVolume(volumeSlider.value);
        }

        if (gameManager){
            gameManager.SetDifficulty(difficultySlider.value);
            gameManager.SetPlayerBody(dropdown.value);
        }
        
	}
    
    public void ResetOptions(){
        volumeSlider.value = 0.5f;
        difficultySlider.value = 2.0f;
        dropdown.value = 0;
    }
    
    public void SaveAndExit(){
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        PlayerPrefsManager.SetDifficulty(difficultySlider.value);
        PlayerPrefsManager.SetPlayerBody(dropdown.value);
        levelManager.LoadLevel("Start");
    }
    
}
