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
    private GameObject[] buttons{
        get{
            return GameObject.FindGameObjectsWithTag("Button");
        }
    }
    
    private int difficulty = 2;
    private MusicManager musicManager
    {
        get {
            return FindObjectOfType<MusicManager>();
        }
    }
    
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

    }
    void CreatePlayer(PlayerType player){

        if (player == PlayerType.Blue)
        {
            GameObject blueplayer = Instantiate(bluePlayer, bluePlayer.transform.position, bluePlayer.transform.rotation) as GameObject;
            //Camera.main.GetComponent<MouseOrbit>().target = blueplayer.transform;
        }
        
        if (player == PlayerType.Green){
            GameObject greenplayer = Instantiate(greenPlayer, greenPlayer.transform.position, greenPlayer.transform.rotation) as GameObject;
            //Camera.main.GetComponent<MouseOrbit>().target = greenplayer.transform;
        }
    }
    
    public void SetPlayerBody(int body){

        playerType = (body == 0) ? PlayerType.Blue : PlayerType.Green;
        CreatePlayer(playerType);
    }
    
    public void SetDifficulty(float diff){
        difficulty = Mathf.RoundToInt(diff);
        
        // set asteroid falling amount
        // set max player speed
        // set road speed
        if (difficulty == 1){
        
        }else if (difficulty == 2){
        
        }else if (difficulty == 3){
        
        }else {
        
        }
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
