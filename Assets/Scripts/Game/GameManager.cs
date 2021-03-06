﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Image gameOverBackground;

    public GameObject gameOverElements;
    public Animator gameOverAnimator;

    public static GameManager instance;

    private GameObject player;

    private GameObject inGameCanvas;

    public GameData gameData;
    public GameSettings gameSettings;

    public PlayerMovement playerMovement;
    public UpdateScore updateScore;

    private const int DISTANCE_BEFORE_SPEED_MULTIPLIER = 100;
    private const float SPEED_MULTIPLIER_INCREMENT = 0.01f;
    private int lastSpeedMultiplerDistance = 0;

    public GameOver gameOver;

    private Animator fadeSystem;

    private AudioManager audioManager;

    public LifeManagerUI lifeManagerUI;

    public FirstTimeRunHint firstTimeRunHint;

    private Animator playerAnimator;

    public bool gameIsOver = false;

    private void Awake()
    {
        Debug.Log("AWAKE game manager");
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager");
            return;
        }
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        inGameCanvas = GameObject.FindGameObjectWithTag("InGameCanvas");
        gameOverAnimator = gameOverElements.GetComponent<Animator>();

        inGameCanvas.SetActive(false);
        player.SetActive(false);

        gameData = SaveSystem.LoadGame();
        if  (gameData == null)
        {
            gameData = new GameData(0, 0, 0, 0, 0);
        }
        updateScore.UpdateLongestDistanceRan(gameData.longestDistanceRan);

        lastSpeedMultiplerDistance = 0;

        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        gameSettings = SaveSystem.LoadGameSettings();
        if (gameSettings == null)
        {
            gameSettings = new GameSettings(true, true);
        }
    }

    public void StartGame()
    {
        fadeSystem.SetTrigger("StartFadeTransition");
        StartCoroutine(LaunchGame());
        Analytics.getInstance().LogGameStarted();
    }

    private IEnumerator LaunchGame()
    {
        yield return new WaitForSeconds(1f);
        inGameCanvas.SetActive(true);
        player.SetActive(true);
        SceneManager.LoadScene("GameScene");
        firstTimeRunHint.FadeOut();
    }

    public void GameIsOver()
    {
        gameIsOver = true;

        audioManager.Stop("GameTheme");
        audioManager.Play("PlayerDeath");

        player.GetComponent<PlayerMovement>().isMoving = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        playerAnimator.SetTrigger("PlayerDie");

        gameOverAnimator.ResetTrigger("ResetGameOverScreen");
        gameOverAnimator.SetTrigger("GameIsOver");


        int distanceRan = playerMovement.currentDistance;
        float maxSpeed = playerMovement.speedMultiplier;
        int score = ComputeScore(distanceRan, maxSpeed);
        bool isHighScore = score > gameData.highScore;
        gameOver.GameIsOver(distanceRan, maxSpeed, score, isHighScore, gameData.highScore);

        if (isHighScore)
        {
            GPGSManager.PostToLeaderBoard(score);
            StartCoroutine(PlayHighScoreSound());
        }

        gameData.numberOfRuns += 1;
        gameData.totalDistanceRan += distanceRan;

        if (gameData.numberOfRuns == 1)
        {
            GPGSManager.UnlockAchievement(GPGSManager.Achievements.NOVICE);
        } else if (gameData.numberOfRuns == 10)
        {
            GPGSManager.UnlockAchievement(GPGSManager.Achievements.ADEPT);
        } else if (gameData.numberOfRuns == 50)
        {
            GPGSManager.UnlockAchievement(GPGSManager.Achievements.PRO);
        } else if (gameData.numberOfRuns == 100)
        {
            GPGSManager.UnlockAchievement(GPGSManager.Achievements.EXPERT);
        }

        if (distanceRan >= 250)
        {
            GPGSManager.UnlockAchievement(GPGSManager.Achievements.NICE_RUN);
        }
        if (distanceRan >= 500)
        {
            GPGSManager.UnlockAchievement(GPGSManager.Achievements.GREAT_RUN);
        }
        if (distanceRan >= 1000)
        {
            GPGSManager.UnlockAchievement(GPGSManager.Achievements.AWESOME_RUN);
        }
        if (distanceRan >= 2500)
        {
            GPGSManager.UnlockAchievement(GPGSManager.Achievements.EPIC_RUN);
        }
        if(distanceRan >= 5000)
        {
            GPGSManager.UnlockAchievement(GPGSManager.Achievements.LEGENDARY_RUN);
        }

        gameData.highScore = Mathf.Max(score, gameData.highScore);
        gameData.longestDistanceRan = Mathf.Max(distanceRan, gameData.longestDistanceRan);
        updateScore.UpdateLongestDistanceRan(gameData.longestDistanceRan);
        SaveSystem.SaveGame(gameData);
    }

    public IEnumerator PlayHighScoreSound()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        audioManager.Play("Win");
    }

    public int ComputeScore(int distanceRan, float maxSpeed)
    {
        // int score = Mathf.RoundToInt(distanceRan * maxSpeed);
        int score = distanceRan; // TODO add more things to the score
        return score;
    }

    public void Restart()
    {

        gameOverAnimator.ResetTrigger("GameIsOver");
        gameOverAnimator.SetTrigger("ResetGameOverScreen");

        fadeSystem.SetTrigger("StartFadeTransition");

        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator RestartCoroutine()
    {
        yield return new WaitForSeconds(0.75f);

        playerAnimator.ResetTrigger("PlayerDie");
        playerAnimator.SetTrigger("PlayerRestart");

        firstTimeRunHint.Reset();
        firstTimeRunHint.FadeOut();

        audioManager.Play("GameTheme");
        updateScore.UpdateDistance(0);

        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        lastSpeedMultiplerDistance = 0;
        playerMovement.speedMultiplier = 1f;
        player.GetComponent<PlayerHealth>().ResetHealth(Inventory.instance.numberOfLivesAvailable);
        lifeManagerUI.Reset();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield return new WaitForSeconds(0.25f);

        gameIsOver = false;
    }

    public void UpdateScore(int distance)
    {
        updateScore.UpdateDistance(distance);
        if (distance > gameData.longestDistanceRan && gameData.longestDistanceRan > 0)
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

    public void GoBackToMenu()
    {
        fadeSystem.SetTrigger("StartFadeTransition");
        gameOverAnimator.SetTrigger("ResetGameOverScreen");
        updateScore.UpdateDistance(0);
        StartCoroutine(GoToMenu());
    }

    private IEnumerator GoToMenu()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        inGameCanvas.SetActive(false);
        player.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}
