using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameManager gameManager = GameManager.instance;

        if (collision.CompareTag("Player"))
        {
            gameManager.GameIsOver();

            string levelPartName;
            if (transform.parent != null)
            {
                levelPartName = transform.parent.name;
            }
            else
            {
                levelPartName = "no_parent";

            }
            Debug.Log("has fallen in a death zone in " + levelPartName);

            int distance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentDistance;
            int highScore = gameManager.gameData.highScore;

            Analytics.getInstance().LogGameOver(levelPartName, "death_zone", distance, highScore);
        }
    }
}
