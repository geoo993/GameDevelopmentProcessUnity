using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private int difficulty = 2;
    private MusicManager musicManager;
    
	static GameManager instance = null;
    
    // Make this game object and all its transform children
    // survive when loading a new scene.
    void Awake () {
        if (instance != null){
            Destroy(gameObject);
            Debug.Log("Duplicate MusicPlayer self-destructed");
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();

        if (musicManager){
            float volume = PlayerPrefsManager.GetMasterVolume();
            musicManager.SetVolume(volume);
        }else{
            Debug.LogWarning("No music manager found in this scene");
        }

        float diff = PlayerPrefsManager.GetDifficulty();
        SetDifficulty(diff);
    }

    public void SetDifficulty(float diff){
        difficulty = Mathf.RoundToInt(diff);
    }
    
    public int GetCurrentDifficulty(){
        return difficulty;
    }
    

}
