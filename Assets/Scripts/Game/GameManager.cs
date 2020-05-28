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

    private GameData gameData;

    public PlayerMovement playerMovement;
    public UpdateScore updateScore;

    private const int DISTANCE_BEFORE_SPEED_MULTIPLIER = 150;
    private const float SPEED_MULTIPLIER_INCREMENT = 0.015f;
    private int lastSpeedMultiplerDistance = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("");
            return;
        }
        instance = this;
        gameOverBackground.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        gameOverElements.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        inGameCanvas = GameObject.FindGameObjectWithTag("InGameCanvas");
        Time.timeScale = 1f;

        gameData = SaveSystem.LoadGame();
        if  (gameData == null)
        {
            gameData = new GameData(0);
        }
        updateScore.UpdateHighscore(gameData.highScore);

        lastSpeedMultiplerDistance = 0;
    }

    public void GameIsOver()
    {
        gameOverBackground.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        gameOverElements.SetActive(true);
        player.GetComponent<PlayerMovement>().isMoving = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        // inGameCanvas.SetActive(false);
        Time.timeScale = 0f;
        int totalDistance = playerMovement.currentDistance;
        gameData.highScore = Mathf.Max(totalDistance, gameData.highScore);
        updateScore.UpdateHighscore(gameData.highScore);
        SaveSystem.SaveGame(gameData);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverElements.SetActive(false);
        // inGameCanvas.SetActive(true);
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
}
