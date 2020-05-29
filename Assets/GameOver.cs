using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{

    public TextMeshProUGUI scoreBreakdownText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    public void GameIsOver(int distanceRan, float maxSpeed, int score, bool isHighscore, int highscore)
    {
        scoreBreakdownText.SetText("Distance: " + distanceRan.ToString() + "m" + "\n" + "Max speed: " + maxSpeed.ToString() + "x");
        finalScoreText.SetText("Final Score: " + score);
        if (isHighscore)
        {
            highScoreText.SetText("NEW HIGH SCORE!");
            highScoreText.fontSize = 100;
        } else
        {
            highScoreText.SetText("High Score: " + highscore.ToString());
            highScoreText.fontSize = 70;
        }
    }
}
