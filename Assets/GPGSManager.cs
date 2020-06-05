using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;

public class GPGSManager : MonoBehaviour
{

    public static void Authenticate(System.Action<bool> callback)
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success, string message) =>
        {
            callback.Invoke(success);
            if (success)
            {
                Debug.Log("success");
            } else
            {
                Toaster.instance.ShowToast(message, 3);
                Debug.LogError("Error: " + message);
            }
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

    public static void ShowAchievementsUI()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    public static void UnlockAchievement(string achievementId)
    {
        PlayGamesPlatform.Instance.UnlockAchievement(achievementId);
    }

    public static class Achievements
    {
        public static string FIRST_RUN = "CgkIkoHks9QfEAIQBA";
    }
}
