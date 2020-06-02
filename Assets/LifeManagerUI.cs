using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManagerUI : MonoBehaviour
{

    public HeartUI[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseLife()
    {
        foreach (HeartUI heart in hearts)
        {
            if (!heart.hasBeenUsed)
            {
                heart.UseHeart();
                break;
            }
        }
    }

    public void Reset()
    {
        foreach (HeartUI heart in hearts)
        {
            heart.Reset();
        }        
    }
}
