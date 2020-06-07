[System.Serializable]
public class GameData
{
    public int highScore;
    public int longestDistanceRan;
    public int numberOfRuns;
    public int totalDistanceRan;
    public int numberOfCactusesHit;

    public GameData(int _highScore, int _longestDistanceRan, int _numberOfRuns, int _totalDistanceRan, int _numberOfCactusesHit)
    {
        highScore = _highScore;
        longestDistanceRan = _longestDistanceRan;
        numberOfRuns = _numberOfRuns;
        totalDistanceRan = _totalDistanceRan;
        numberOfCactusesHit = _numberOfCactusesHit;
    }
}