using System.Collections;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{

    private GameManager gameManager;
    private CameraFollow cameraFollow;

    public GameObject highScore;
    public TextMeshProUGUI highScoreValue;

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
    }

    public void StartGame()
    {
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
}
