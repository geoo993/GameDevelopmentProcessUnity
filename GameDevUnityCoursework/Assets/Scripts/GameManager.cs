using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum PlayerType { Blue, Green };
    private PlayerType playerType = PlayerType.Blue;
    
	public GameObject bluePlayer;
    public GameObject greenPlayer;

    public Text scoreText;
    private int scoreCount = 0;
    
    public static GameObject selectedButton;
    private GameObject[] buttons;
    
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
        buttons = GameObject.FindGameObjectsWithTag("Button");
        
        musicManager = FindObjectOfType<MusicManager>();

        if (musicManager){
            float volume = PlayerPrefsManager.GetMasterVolume();
            musicManager.SetVolume(volume);
        }else{
            Debug.LogWarning("No music manager found in this scene");
        }

        float diff = PlayerPrefsManager.GetDifficulty();
        SetDifficulty(diff);

        int playerBody = PlayerPrefsManager.GetPlayerBody();
        SetPlayerBody(playerBody);

        //SetScore(scoreCount);
    }
    
    public void SetPlayerBody(int body){

        playerType = (body == 0) ? PlayerType.Blue : PlayerType.Green;
        
        if (playerType == PlayerType.Blue){
            Camera.main.GetComponent<MouseOrbit>().target = bluePlayer.transform;
            bluePlayer.SetActive(true);
            greenPlayer.SetActive(false);
        }else {
			Camera.main.GetComponent<MouseOrbit>().target = greenPlayer.transform;
            bluePlayer.SetActive(false);
            greenPlayer.SetActive(true);
        }
    }
    
    public void SetDifficulty(float diff){
        difficulty = Mathf.RoundToInt(diff);
    }
    
    public int GetCurrentDifficulty(){
        return difficulty;
    }
    
    public void SetScore(int score, string item){
        scoreCount += score;
        scoreText.text = scoreCount.ToString();
        
        
        GameObject itemScoreText = null;
        foreach(GameObject button in buttons){
            if (button.name == item){
                itemScoreText = button.transform.Find("ScoreText").gameObject;
                break;
            }
        }

        if (itemScoreText){
            itemScoreText.GetComponent<ScoreText>().SetScore(score);
        }
    }
    
    
    
    public void OnMouseDown(GameObject tapped)
    {
        foreach (GameObject button in buttons){
            button.GetComponent<Image>().color = Color.black;
        }
        
        tapped.GetComponent<Image>().color = Color.white;
        selectedButton = tapped;
        
    }

}
