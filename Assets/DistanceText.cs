﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceText : MonoBehaviour
{

    public TextMeshProUGUI distanceText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateText(float distance)
    {
        int roundedDistance = Mathf.RoundToInt(distance);
        distanceText.SetText(roundedDistance.ToString() + "m");
    }
}
