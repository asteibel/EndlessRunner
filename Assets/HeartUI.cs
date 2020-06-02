using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{

    private Image image;

    public bool hasBeenUsed = false;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseHeart()
    {
        hasBeenUsed = true;
        image.color = new Color(0f, 0f, 0f, 0.5f);
    }

    public void Reset()
    {
        hasBeenUsed = false;
        image.color = new Color(1f, 1f, 1f, 1f);
    }
}
