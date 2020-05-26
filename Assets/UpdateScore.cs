using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{

    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI highscoreText;

    public void UpdateDistance(int distance)
    {
        distanceText.SetText(distance.ToString() + "m");
    }

    public void UpdateHighscore(int highscore)
    {
        if (highscore > 0)
        {
            highscoreText.SetText("highscore: " + highscore.ToString() + "m");
        } else
        {
            highscoreText.SetText("no highscore yet...");
        }
    }

    public void IsHighScoring()
    {
        highscoreText.SetText("new highscore!!");
    }
}
