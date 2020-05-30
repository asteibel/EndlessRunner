using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private SpriteRenderer graphics;

    private void Awake()
    {
        graphics = GetComponent<SpriteRenderer>();
        graphics.flipX = Random.value < 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gameManager = GameManager.instance;
        if (collision.CompareTag("Player"))
        {
            gameManager.GameIsOver();

            string levelPartName;
            if  (transform.parent != null)
            {
                levelPartName = transform.parent.name;
            } else
            {
                levelPartName = "no_parent";
                
            }
            Debug.Log("has hit an obsctable in " + levelPartName);

            int distance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentDistance;
            int highScore = gameManager.gameData.highScore;

            Firebase.Analytics.Parameter[] gameOVerParameters = {
                new Firebase.Analytics.Parameter(
                "LevelPart", levelPartName),
                new Firebase.Analytics.Parameter(
                 "DeathType", "obstacle"),
                new Firebase.Analytics.Parameter(
                 "Score", distance),
                new Firebase.Analytics.Parameter(
                 "HighScore", highScore)
                };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("GameOver", gameOVerParameters);
        }
    }
}
