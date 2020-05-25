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

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("");
            return;
        }
        instance = this;
        gameOverBackground.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        gameOverElements.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        inGameCanvas = GameObject.FindGameObjectWithTag("InGameCanvas");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameIsOver()
    {
        gameOverBackground.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        gameOverElements.SetActive(true);
        player.GetComponent<PlayerMovement>().isMoving = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        inGameCanvas.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverElements.SetActive(false);
        Debug.Log("Restart");
        inGameCanvas.SetActive(true);
    }
}
