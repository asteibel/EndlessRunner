using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private int numberOfLives = int.MinValue;

    /**
     * return if the game is over
     * */
    public bool TakeDamage(int damageToTake)
    {
        if (numberOfLives == int.MinValue)
        {
            numberOfLives = Inventory.instance.numberOfLivesAvailable;
        }

        numberOfLives -= damageToTake;
        if (numberOfLives < 0)
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
