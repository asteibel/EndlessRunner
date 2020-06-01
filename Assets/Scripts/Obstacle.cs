using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private SpriteRenderer graphics;

    // chance to keep cactus
    // 0 -> 250, 50%; 250 -> 500: 60%; 500 -> 1000: 75%; 1000 -> 1500: 90%; 1500+ 100%.

    private void Awake()
    {
        graphics = GetComponent<SpriteRenderer>();
        graphics.flipX = Random.value < 0.5f;
        Debug.Log("spawning catctus");
        float shouldDestroyCactus = Random.value;
        int currentDistance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentDistance;
        if (currentDistance < 250 && shouldDestroyCactus < 0.5f)
        {
            Destroy(gameObject);
        } else if (currentDistance < 500 && shouldDestroyCactus < 0.4f)
        {
            Destroy(gameObject);
        } else if (currentDistance < 1000 && shouldDestroyCactus < 0.35f)
        {
            Destroy(gameObject);
        } else if (currentDistance < 1500 && shouldDestroyCactus < 0.1f)
        {
            Destroy(gameObject);
        }
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
