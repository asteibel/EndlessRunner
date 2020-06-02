using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManagerUI : MonoBehaviour
{

    public GameObject[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        int numberOfLivesAvailable = Inventory.instance.numberOfLivesAvailable;
        int index = 0;
        Debug.Log("start, number of lives: " + numberOfLivesAvailable);
        foreach (GameObject heart in hearts)
        {
            int lenght = hearts.Length;
            if (hearts.Length - index > numberOfLivesAvailable)
            {
                Debug.Log("heart " + index + " false");
                heart.SetActive(false);
            } else
            {
                Debug.Log("heart " + index + " true");
                heart.SetActive(true);
            }

            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseLife()
    {
        foreach (GameObject heart in hearts)
        {
            HeartUI heartUI = heart.GetComponent<HeartUI>();
            if (!heartUI.hasBeenUsed && heart.activeInHierarchy)
            {
                heartUI.UseHeart();
                break;
            }
        }
    }

    public void Reset()
    {
        foreach (GameObject heart in hearts)
        {
            heart.GetComponent<HeartUI>().Reset();
        }        
    }
}
