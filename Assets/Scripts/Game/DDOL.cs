using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{

    public GameObject[] gameObjects;

    private void Awake()
    {
        foreach (GameObject go in gameObjects)
        {
            DontDestroyOnLoad(go);
        }
        SceneManager.LoadScene("Menu");
    }
}
