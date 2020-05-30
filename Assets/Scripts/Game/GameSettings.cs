using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSettings
{

    public bool hasMusicEnabled;
    public bool hasSoundsEnabled;

    public GameSettings(bool _hasMusicEnabled, bool _hasSoundsEnabled)
    {
        hasMusicEnabled = _hasMusicEnabled;
        hasSoundsEnabled = _hasSoundsEnabled;
    }
}
