using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum PlayerType { Blue, Green };
    private PlayerType playerType = PlayerType.Blue;
    private HoverBoardControl playerHoverBoard = null;

    public LevelManager levelManager;
    public GameObject bluePlayer;
    public GameObject greenPlayer;
    public GameObject mainCamera;
    public Shredder shredder;
    public FallingObjects faller;
    
    public Text crystalsText;
    private int crystalsCount = 0;
    private int crystalsMaxCount = 50;
    
    public Text scoreText;
    private int scoreCount = 0;
    
    public Slider healthBar;
    private float healthCount = 100.0f;

    public GameObject candyBarGUI = null;
    
    public Slider speedBoosterBar = null;
    public GameObject speedBoosterGUI = null;
    
    public Slider shieldBoosterBar = null;
	public GameObject shieldBoosterGUI = null;
    
    public GameObject trophy;
    
    private int difficulty = 2;
    
    public static GameObject selectedButton;
    private GameObject[] buttons{
        get{
            return GameObject.FindGameObjectsWithTag("Button");
        }
    }
    
    private MusicManager musicManager
    {
        get {
            return FindObjectOfType<MusicManager>();
        }
    }
    
    static GameManager instance = null;
    public static bool endOfAsteroidAttack = false;
    
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
        
        if (playerHoverBoard){
            playerHoverBoard.gameManager = GetComponent<GameManager>();
        }

		int playerBody = PlayerPrefsManager.GetPlayerBody();
		SetPlayerBody(playerBody);
        
        
        float diff = PlayerPrefsManager.GetDifficulty();
        SetDifficulty(diff);

        DisableCollectableGUIs();
        candyBarGUI.SetActive(true);

    }

    void Update()
    {
        GameLose();
    }

    void CreateCamera(Transform target){
        GameObject cam = Instantiate(mainCamera, Vector3.zero, Quaternion.identity) as GameObject;
        cam.name = mainCamera.name;
        cam.GetComponent<MouseOrbit>().target = target;
    }
    
    void CreatePlayer(PlayerType player){

        GameObject newPlayer = null;

        string deactivePlayerGUI = (player == PlayerType.Green) ? "BluePlayerGUI" : "GreenPlayerGUI";
        GameObject.FindGameObjectWithTag(deactivePlayerGUI).SetActive(false);
        
        if (player == PlayerType.Blue)
        {
            newPlayer = Instantiate(bluePlayer, bluePlayer.transform.position, bluePlayer.transform.rotation) as GameObject;
            newPlayer.name = bluePlayer.name;
        }
        
        if (player == PlayerType.Green)
        {
            newPlayer = Instantiate(greenPlayer, greenPlayer.transform.position, greenPlayer.transform.rotation) as GameObject;
            newPlayer.name = greenPlayer.name;
        }

        if (newPlayer)
        {
            playerHoverBoard = newPlayer.GetComponent<HoverBoardControl>();
            CreateCamera(newPlayer.transform);
        }
       
    }
    
    public void SetPlayerBody(int body){

        playerType = (body == 0) ? PlayerType.Blue : PlayerType.Green;
        CreatePlayer(playerType);
    }
    
    public void SetDifficulty(float diff){
        
        difficulty = Mathf.RoundToInt(diff);

        if (difficulty <= 1){
            EasyDifficulty();
        }else if (difficulty == 2){
            NormalDifficulty();
        }else if (difficulty == 3){
            HardDifficulty();
        }else {
            return;
        }

        endOfAsteroidAttack = false;
    }
    
    private void EasyDifficulty(){
		shredder.SetSpeed(0.2f); // move slow
		faller.SetTimers(0.1f, 2.0f); // fall in average speed rate and less fallers
		playerHoverBoard.SetSpeed(4000, 1500, 300); // player moves slow  
    }
    
    private void NormalDifficulty(){
		shredder.SetSpeed(0.4f); // move average
		faller.SetTimers(0.05f, 1.5f); // fall in average speed rate and average fallers
		playerHoverBoard.SetSpeed(6000, 2000, 500); // player moves average
    }
    
    private void HardDifficulty(){
        shredder.SetSpeed(0.6f); // move fast
        faller.SetTimers(0.01f, 1.0f); // fall in fast speed rate and lots of fallers
        playerHoverBoard.SetSpeed(10000, 2500, 700); // player moves fast
    }
    
    public int GetCurrentDifficulty(){
        return difficulty;
    }
    
    public void SetScore(int score, string item){
        scoreCount += score;
        scoreText.text = scoreCount.ToString();
        
        /*
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
        */
    }
    
    public void SetCrystalsCollected(int collected, string item){
		crystalsCount += collected;
        
        if (crystalsCount >= crystalsMaxCount) {
            crystalsCount = crystalsMaxCount;
            endOfAsteroidAttack = true;
        }
        
        crystalsText.text = crystalsCount.ToString() + " / " + crystalsMaxCount.ToString();
        
        
        /*
        GameObject crystalsCollectedText = null;
        foreach(GameObject button in buttons){
            if (button.name == item){
                crystalsCollectedText = button.transform.Find("CrystalsText").gameObject;
                break;
            }
        }

        if (crystalsCollectedText){
            crystalsCollectedText.GetComponent<CrystalsCollection>().SetCrystalsCollected(collected);
        }
        */
    }
    
    
	public void OnMouseDown(GameObject tapped)
	{
        /*
		if (tapped.name == "Trophy")
		{
			return;
		}
		
		foreach (GameObject button in buttons){
			button.GetComponent<Image>().color = Color.black;
		}
		
		tapped.GetComponent<Image>().color = Color.white;
		selectedButton = tapped;
		*/
	}
   
    public void SetHealth(float health, bool shouldIncrease){

        healthCount = shouldIncrease ? (healthCount + health) : (healthCount - health);
        
        if (healthCount > 100.0f) { healthCount = 100.0f; }
        healthBar.value = healthCount;
    }
    
    
    public void SetSpeedBooster(float speed, float timer){
        if (playerHoverBoard.gameManager == null){
			playerHoverBoard.gameManager = GetComponent<GameManager>();
        }
        if (playerHoverBoard) {
            playerHoverBoard.ApplySpeedBooster(speed, timer, true);
            SetSpeedBoosterGUI(true);
        }
        if (speedBoosterBar){
			speedBoosterBar.maxValue = timer;
        }
    }
    public void SetSpeedBoosterGUI(bool active){
        speedBoosterGUI.SetActive(active);
        candyBarGUI.SetActive(!active);
        
        if (active)
        {
            shieldBoosterGUI.SetActive(false);
        }
    }
    public void SetSpeedBoosterBar(float value){
        if (speedBoosterBar){
            speedBoosterBar.value = value;
        }
    }

    public void SetShieldBooster(float shield, float timer){
        if (playerHoverBoard.gameManager == null){
            playerHoverBoard.gameManager = GetComponent<GameManager>();
        }
        if (playerHoverBoard) {
            playerHoverBoard.ApplyShieldBooster(shield, timer, true);
            SetShieldBoosterGUI(true);
        }
        if (shieldBoosterBar){
            shieldBoosterBar.maxValue = timer;
        }
    }
    public void SetShieldBoosterGUI(bool active){
         shieldBoosterGUI.SetActive(active);
         candyBarGUI.SetActive(!active);
         
         if (active)
         {
            speedBoosterGUI.SetActive(false);
         }
    }
    public void SetShieldBoosterBar(float value){
        if (shieldBoosterBar){
            shieldBoosterBar.value = value;
        }
    }
    
    
    public void SetBonus(int point, string item){
        DisableCollectableGUIs();
        trophy.SetActive(true);
        SetScore(point, item);
        GameWin();
    }
    
    void DisableCollectableGUIs() {
        trophy.SetActive(false);
        shieldBoosterGUI.SetActive(false);
        candyBarGUI.SetActive(false);
        speedBoosterGUI.SetActive(false);
    }
    
    void GameWin(){

        if (endOfAsteroidAttack)
        {
             Invoke("ActivateGameWin", 5); // inseconds
        }
        
    }
    
    void GameLose(){
    
        if (healthCount <= 0){
            // load game lose screen
            ActivateGameLose();
        }
    }
    
    public void ActivateGameWin(){
        levelManager.LoadLevel("Win");
    }
    
    
    public void ActivateGameLose(){
        levelManager.LoadLevel("Lose");
    }
    
}
