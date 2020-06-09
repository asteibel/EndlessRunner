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
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.gameData.numberOfCactusesHit += 1;

            if (GameManager.instance.gameData.numberOfCactusesHit >= 50)
            {
                GPGSManager.UnlockAchievement(GPGSManager.Achievements.CACTUS_LOVER);
            }

            GameManager.instance.lifeManagerUI.UseLife();

            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth.TakeDamage(1)) // Game is over
            {
                string levelPartName;
                if (transform.parent != null)
                {
                    levelPartName = transform.parent.name;
                }
                else
                {
                    levelPartName = "no_parent";

                }
                Debug.Log("has hit an obsctable in " + levelPartName);

                int distance = collision.GetComponent<PlayerMovement>().currentDistance;
                int highScore = GameManager.instance.gameData.highScore;

                Analytics.instance.LogGameOver(levelPartName, "obstacle", distance, highScore);
            } else
            {
                string soundToPlay;
                if (Random.value <  0.5f)
                {
                    soundToPlay = "PlayerHurt";

                } else
                {
                    soundToPlay = "PlayerHurt2";
                }
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play(soundToPlay);
                collision.GetComponent<Animator>().SetTrigger("PlayerHurt");
            }

            
        }
    }
}
