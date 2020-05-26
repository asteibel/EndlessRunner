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
        gameData.highScore = 0; // TODO remove
        updateScore.UpdateHighscore(gameData.highScore);
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
    }

    public void UpdateScore(int distance)
    {
        updateScore.UpdateDistance(distance);
        if (distance > gameData.highScore && gameData.highScore > 0)
        {
            updateScore.IsHighScoring();
        }
    }
}
