using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private const float PLAYER_DISTANCE_TO_LAST_LEVEL_PART = 100f;

    public Transform[] levelParts;

    private Vector3 lastEndPosition;

    public Transform firstLastLevel;

    private GameObject player;

    public int forceLevel = -1;
    public bool debugMode = false;
    public int debugLevelPartIndex = 0;

    private float playerStartPosition;
    private GameManager gameManager;

    private void Awake()
    {
        lastEndPosition = firstLastLevel.Find("EndOfPlatform").position;
        player = GameObject.FindGameObjectWithTag("Player");

        playerStartPosition = player.transform.position.x;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_TO_LAST_LEVEL_PART)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Transform chosenPart;

        if (debugMode)
        {
            chosenPart = levelParts[debugLevelPartIndex];
            debugLevelPartIndex = Mathf.Min(debugLevelPartIndex + 1, levelParts.Length - 1);
        } else if (forceLevel > -1)
        {
            chosenPart = levelParts[forceLevel];
        } else
        {
            chosenPart = levelParts[Random.Range(0, levelParts.Length)];
        }

        Debug.Log("Spawning level " + chosenPart.name);
        Transform lastLevelSpawnedTransform = SpawnLevelPart(chosenPart, lastEndPosition);

        lastEndPosition = lastLevelSpawnedTransform.Find("EndOfPlatform").position;


        /** float startOfPlatform = lastLevelSpawnedTransform.Find("StartOfPlatform").position.x;
        bool isHighScore = (startOfPlatform - playerStartPosition <= gameManager.gameData.longestDistanceRan) &&
            (lastEndPosition.x - playerStartPosition >= gameManager.gameData.longestDistanceRan);
        Debug.Log("distance from start to end  level: " + (lastEndPosition.x - playerStartPosition));
        Debug.Log("Is highscore: " + isHighScore); */ // TODO Show marker when crossing highscore
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 position)
    {
        Transform levelPartTransform = Instantiate(levelPart, position, Quaternion.identity);
        return levelPartTransform;
    }
}
