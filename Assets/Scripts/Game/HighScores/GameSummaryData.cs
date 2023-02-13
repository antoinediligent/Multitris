using Game;

[System.Serializable]
public class GameSummaryData
{
    public int numberOfPlayers;
    public int level;
    public int score;
    public int lines;

    public PlayerStats[] playerStats;
    public string[] playerNames;

    public GameSummaryData(int numberOfPlayers, ScoreCalculator scoreCalculator)
    {
        this.numberOfPlayers = numberOfPlayers;
        level = scoreCalculator.GetLevel();
        score = scoreCalculator.GetScore();
        lines = scoreCalculator.GetLines();

        playerStats = new PlayerStats[4];
        playerStats[0] = scoreCalculator.GetPlayerStats(1);
        if (numberOfPlayers > 1)
        {
            playerStats[1] = scoreCalculator.GetPlayerStats(2);
        }
        if (numberOfPlayers > 2)
        {
            playerStats[2] = scoreCalculator.GetPlayerStats(3);
        }
        if (numberOfPlayers > 3)
        {
            playerStats[3] = scoreCalculator.GetPlayerStats(4);
        }

        playerNames = new string[4];
    }

    public void setPlayerName(int playerNumber, string playerName)
    {
        playerNames[playerNumber] = playerName;
    }
}
