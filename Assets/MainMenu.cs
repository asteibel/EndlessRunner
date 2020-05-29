using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private GameManager gameManager;
    private CameraFollow cameraFollow;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        cameraFollow.cameraMode = CameraFollow.CameraMode.mainMenu;
    }

    public void StartGame()
    {
        gameManager.StartGame();
        // cameraFollow.cameraMode = CameraFollow.CameraMode.followPlayer;
        // SceneManager.LoadScene("GameScene");
        StartCoroutine(ChangeCameraToFollowPlayer());
    }

    public IEnumerator ChangeCameraToFollowPlayer()
    {
        yield return new WaitForSeconds(1f);
        cameraFollow.cameraMode = CameraFollow.CameraMode.followPlayer;
    }
}
