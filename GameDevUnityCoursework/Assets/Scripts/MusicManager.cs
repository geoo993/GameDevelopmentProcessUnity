using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip[] levelMusicChangeArray;
    private AudioSource audioSource;
    
    static MusicManager instance = null;
    
    void Awake()
    {
        if (instance != null){
            Destroy(gameObject);
            Debug.Log("Duplicate MusicPlayer self-destructed");
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
    }

    void OnLevelWasLoaded(int level)
    {
        //int activeScene = SceneManager.GetActiveScene().buildIndex;

        if (level < levelMusicChangeArray.Length)
        {
            AudioClip thisAudioClip = levelMusicChangeArray[level];

            if (thisAudioClip)
            {
                audioSource.clip = thisAudioClip;
                audioSource.loop = true;
                audioSource.Play();
            }

        }
        
    }
    
    public void SetVolume(float volume){
        audioSource.volume = volume;
    }
    
}
