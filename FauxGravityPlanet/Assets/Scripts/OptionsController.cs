using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    public Slider volumeSlider;
    public Slider difficultySlider;
    public LevelManager levelManager;
    private MusicManager musicManager;
    private GameManager gameManager;
    
	// Use this for initialization
	void Start () {
        musicManager = FindObjectOfType<MusicManager>();
        gameManager = FindObjectOfType<GameManager>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        difficultySlider.value = PlayerPrefsManager.GetDifficulty();
	}
	
	// Update is called once per frame
	void Update () {
        if (musicManager){
            musicManager.SetVolume(volumeSlider.value);
        }

        if (gameManager){
            gameManager.SetDifficulty(difficultySlider.value);
        }
	}
    
    public void ResetOptions(){
        volumeSlider.value = 0.5f;
        difficultySlider.value = 2.0f;
    }
    
    public void SaveAndExit(){
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        PlayerPrefsManager.SetDifficulty(difficultySlider.value);
        levelManager.LoadLevel("Start");
    }
    
}
