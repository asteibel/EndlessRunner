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

    private void Awake()
    {
        lastEndPosition = firstLastLevel.Find("EndOfPlatform").position;
        player = GameObject.FindGameObjectWithTag("Player");
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

        Transform chosenPart = levelParts[Random.Range(0, levelParts.Length)];
        if (forceLevel > -1)
        {
            chosenPart = levelParts[forceLevel];
        }
        Transform lastLevelSpawnedTransform = SpawnLevelPart(chosenPart, lastEndPosition);
        lastEndPosition = lastLevelSpawnedTransform.Find("EndOfPlatform").position;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 position)
    {
        Transform levelPartTransform = Instantiate(levelPart, position, Quaternion.identity);
        return levelPartTransform;
    }
}
