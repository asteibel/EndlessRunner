using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    public int numberOfLivesAvailable = 3;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager");
            return;
        }
        instance = this;
    }
}
