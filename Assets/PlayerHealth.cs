using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int numberOfLives = 3;

    /**
     * return if the game is over
     * */
    public bool TakeDamage(int damagetToTake)
    {
        numberOfLives -= damagetToTake;
        if (numberOfLives <= 0)
        {
            GameManager.instance.GameIsOver();
            return true;
        }
        return false;
    }

    public void ResetHealth(int _numberOfLives) 
    {
        numberOfLives = _numberOfLives;
    }
}
