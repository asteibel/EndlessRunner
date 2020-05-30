using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{

    private static string saveFileName = "/game.save";
    private static string path = Application.persistentDataPath + saveFileName;

    private static string saveSettingsFileName = "/game_settings.save";
    private static string gameSettingsPath = Application.persistentDataPath + saveSettingsFileName;


    public static void SaveGame(GameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("save file not found at " + path);
            return null;
        }
    }

    public static void SaveGameSettings(GameSettings gameSettings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(gameSettingsPath, FileMode.Create);

        formatter.Serialize(stream, gameSettings);
        stream.Close();
    }

    public static GameSettings LoadGameSettings()
    {
        if (File.Exists(gameSettingsPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(gameSettingsPath, FileMode.Open);
            GameSettings gameSettings = formatter.Deserialize(stream) as GameSettings;
            stream.Close();
            return gameSettings;
        }
        else
        {
            Debug.LogError("save file not found at " + path);
            return null;
        }
    }
}
