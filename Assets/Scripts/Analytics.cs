using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;

public class Analytics
{
    private static Analytics instance;

    public static Analytics getInstance()
    {
        if (instance == null)
        {
            instance = new Analytics();
        }
        return instance;
    }

    public void LogGameStarted()
    {
        FirebaseAnalytics.LogEvent("GameStarted");
    }

    public void LogGameOver(string levelPartName, string deathType, int distance, int highScore)
    {
        Firebase.Analytics.Parameter[] gameOVerParameters = {
                new Firebase.Analytics.Parameter(
                "LevelPart", levelPartName),
                new Firebase.Analytics.Parameter(
                 "DeathType", deathType),
                new Firebase.Analytics.Parameter(
                 "Score", distance),
                new Firebase.Analytics.Parameter(
                 "HighScore", highScore)
                };
        Firebase.Analytics.FirebaseAnalytics.LogEvent("GameOver", gameOVerParameters);
    }
}
