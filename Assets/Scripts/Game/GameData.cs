[System.Serializable]
public class GameData
{
    public int highScore;
    public int longestDistanceRan;

    public GameData(int _highScore, int _longestDistanceRan)
    {
        highScore = _highScore;
        longestDistanceRan = _longestDistanceRan;
    }
}