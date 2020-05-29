using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{

    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI speedText;

    public void UpdateDistance(int distance)
    {
        distanceText.SetText(distance.ToString() + "m");
    }

    public void UpdateLongestDistanceRan(int longestDistanceRan)
    {
        if (longestDistanceRan > 0)
        {
            highscoreText.SetText("longest distance ran: " + longestDistanceRan.ToString() + "m");
        } else
        {
            highscoreText.SetText("first run...");
        }
    }

    public void IsHighScoring()
    {
        highscoreText.SetText("new record!!");
    }

    public void SetSpeed(float speed)
    {
        speedText.SetText("speed: " + speed + "x");
    }
}
