using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;

public class GPGSManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Authenticate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Authenticate()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success, string message) =>
        {
            if (success)
            {
                Debug.Log("success");
            } else
            {
                Debug.LogError("Error: " + message);
            }
            SceneManager.LoadScene("Menu");
        });
    }

    public static void PostToLeaderBoard(int score)
    {
        Social.ReportScore(score, GPGSIds.leaderboard_high_score, (bool success) =>
        {
            if (success)
            {
                Debug.Log("new score posted to leader board");
            } else
            {
                Debug.LogError("Unable to post to leaderboard");
            }
        });
    }

    public static void ShowLeaderBoardUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_high_score);
    }
}
