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

    public void SetPlayerName(int playerNumber, string playerName)
    {
        playerNames[playerNumber] = playerName;
    }

    public string GetPlayerNamesForHighScoreBoard()
    {
        string playerNamesString = playerNames[0];

        if (numberOfPlayers >= 2)
        {
            playerNamesString += ", " + playerNames[1];
        }

        if (numberOfPlayers >= 3)
        {
            playerNamesString += ", " + playerNames[2];
        }

        if (numberOfPlayers == 4)
        {
            playerNamesString += ", " + playerNames[3];
        }

        return playerNamesString;
    }
}
