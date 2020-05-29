using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Image gameOverBackground;

    public GameObject gameOverElements;

    public static GameManager instance;

    private GameObject player;

    private GameObject inGameCanvas;

    public GameData gameData;

    public PlayerMovement playerMovement;
    public UpdateScore updateScore;

    private const int DISTANCE_BEFORE_SPEED_MULTIPLIER = 100;
    private const float SPEED_MULTIPLIER_INCREMENT = 0.01f;
    private int lastSpeedMultiplerDistance = 0;

    public GameOver gameOver;

    private Animator fadeSystem;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager");
            return;
        }
        instance = this;

        gameOverBackground.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        gameOverElements.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        inGameCanvas = GameObject.FindGameObjectWithTag("InGameCanvas");

        inGameCanvas.SetActive(false);
        player.SetActive(false);

        Time.timeScale = 1f;

        gameData = SaveSystem.LoadGame();
        if  (gameData == null)
        {
            gameData = new GameData(0, 0);
        }
        updateScore.UpdateLongestDistanceRan(gameData.longestDistanceRan);

        lastSpeedMultiplerDistance = 0;

        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    public void StartGame()
    {
        fadeSystem.SetTrigger("StartFadeTransition");
        // inGameCanvas.SetActive(true);
        // player.SetActive(true);
        StartCoroutine(LaunchGame());
    }

    private IEnumerator LaunchGame()
    {
        yield return new WaitForSeconds(1f);
        inGameCanvas.SetActive(true);
        player.SetActive(true);
        SceneManager.LoadScene("GameScene");
    }

    public void GameIsOver()
    {


        player.GetComponent<PlayerMovement>().isMoving = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        Time.timeScale = 0f;

        gameOverBackground.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        gameOverElements.SetActive(true);


        int distanceRan = playerMovement.currentDistance;
        float maxSpeed = playerMovement.speedMultiplier;
        int score = Mathf.RoundToInt(distanceRan * maxSpeed);
        bool isHighScore = score > gameData.highScore;
        gameOver.GameIsOver(distanceRan, maxSpeed, score, isHighScore, gameData.highScore);

        gameData.highScore = Mathf.Max(score, gameData.highScore);
        gameData.longestDistanceRan = Mathf.Max(distanceRan, gameData.longestDistanceRan);
        updateScore.UpdateLongestDistanceRan(gameData.longestDistanceRan);
        SaveSystem.SaveGame(gameData);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverElements.SetActive(false);
        Time.timeScale = 1f;
        lastSpeedMultiplerDistance = 0;
        playerMovement.speedMultiplier = 1f;
    }

    public void UpdateScore(int distance)
    {
        updateScore.UpdateDistance(distance);
        if (distance > gameData.highScore && gameData.highScore > 0)
        {
            updateScore.IsHighScoring();
        }
        if  (distance  % DISTANCE_BEFORE_SPEED_MULTIPLIER == 0 && distance != lastSpeedMultiplerDistance)
        {
            lastSpeedMultiplerDistance = distance;
            playerMovement.speedMultiplier += SPEED_MULTIPLIER_INCREMENT;
        }
        updateScore.SetSpeed(playerMovement.speedMultiplier);
    }

    public void ResetGameData()
    {
        updateScore.UpdateLongestDistanceRan(0);
        gameData.highScore = 0;
        gameData.longestDistanceRan = 0;
        UpdateScore(0);
    }
}
