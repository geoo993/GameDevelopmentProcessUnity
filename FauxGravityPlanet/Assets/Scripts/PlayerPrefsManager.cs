using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {

    const string MASTER_VOLUME_KEY = "master_volume";
    const string DIFFICULTY_KEY = "difficulty";
    const string LEVEL_KEY = "level_unlocked_";
   
    // Master Volume
    public static void SetMasterVolume(float volume){
        if (volume >= 0.0f && volume <= 1.0f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }else{
            Debug.LogError("Master volume out of range");
        }
    }
    
    public static float GetMasterVolume(){
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }
    
    
    // Difficulty
    public static void SetDifficulty(float difficulty){
        if (difficulty >= 1.0f && difficulty <= 3.0f)
        {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }else{
            Debug.LogError("Difficulty out of range");
        }
    }
    
    public static float GetDifficulty(){
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }
    
    
    // UnLocked Level
    public static void UnLockedLevel(int level){
    
        if (level > 0 && level < SceneManager.sceneCount)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1); // use 1 for true
        }else{
            Debug.LogError("Trying to unlocked level that's not in build settings");
        }
    }
    
    public static bool IsLevelUnLocked(int level){

        if (level > 0 && level < SceneManager.sceneCount)
        {
            return (PlayerPrefs.GetInt(LEVEL_KEY + level.ToString()) == 1);
        }else {
            Debug.LogError("Trying to query level that's not in build settings");
            return false;
        }
    }
}
