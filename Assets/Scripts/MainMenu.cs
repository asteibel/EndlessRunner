using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private GameManager gameManager;
    private CameraFollow cameraFollow;

    public GameObject highScore;
    public TextMeshProUGUI highScoreValue;

    private AudioManager audioManager;

    public TextMeshProUGUI changeMusic;
    public TextMeshProUGUI changeSounds;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        cameraFollow.cameraMode = CameraFollow.CameraMode.mainMenu;

        if (gameManager.gameData.highScore == 0)
        {
            highScore.SetActive(false);
        } else {
            highScore.SetActive(true);
            highScoreValue.SetText(gameManager.gameData.highScore.ToString());
        }

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("MenuTheme");

        if (gameManager.gameSettings.hasMusicEnabled)
        {
            changeMusic.SetText("Turn OFF");
        } else
        {
            changeMusic.SetText("Turn ON");
        }

        if (gameManager.gameSettings.hasSoundsEnabled)
        {
            changeSounds.SetText("Turn OFF");
        }
        else
        {
            changeSounds.SetText("Turn ON");
        }
    }

    public void StartGame()
    {
        audioManager.Stop("MenuTheme");
        audioManager.Play("GameTheme");
        gameManager.StartGame();
        StartCoroutine(ChangeCameraToFollowPlayer());
    }

    public IEnumerator ChangeCameraToFollowPlayer()
    {
        yield return new WaitForSeconds(1f);
        cameraFollow.cameraMode = CameraFollow.CameraMode.followPlayer;
    }

    public void ResetHighScore()
    {
        highScore.SetActive(false);
        gameManager.ResetGameData();
    }

    public void ChangeMusicSettings()
    {
        gameManager.gameSettings.hasMusicEnabled = !gameManager.gameSettings.hasMusicEnabled;
        if (gameManager.gameSettings.hasMusicEnabled)
        {
            audioManager.Play("MenuTheme");
            changeMusic.SetText("Turn OFF");
        }
        else
        {
            audioManager.Stop("MenuTheme");
            changeMusic.SetText("Turn ON");
        }
        SaveSystem.SaveGameSettings(gameManager.gameSettings);
    }

    public void ChangeSoundsSettings()
    {
        gameManager.gameSettings.hasSoundsEnabled = !gameManager.gameSettings.hasSoundsEnabled;
        if (gameManager.gameSettings.hasSoundsEnabled)
        {
            changeSounds.SetText("Turn OFF");
        }
        else
        {
            changeSounds.SetText("Turn ON");
        }
        SaveSystem.SaveGameSettings(gameManager.gameSettings);
    }

    public void ShowLeaderBoard()
    {
        GPGSManager.ShowLeaderBoardUI();
    }
}
