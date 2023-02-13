using Game;

[System.Serializable]
public class HighScoreBoardData
{
    public GameSummaryData[] onePlayerBoard;
    public GameSummaryData[] twoPlayerBoard;
    public GameSummaryData[] threePlayerBoard;
    public GameSummaryData[] fourPlayerBoard;

    public HighScoreBoardData()
    {
        onePlayerBoard = new GameSummaryData[10];
        twoPlayerBoard = new GameSummaryData[10];
        threePlayerBoard = new GameSummaryData[10];
        fourPlayerBoard = new GameSummaryData[10];
    }

    public bool IsGameScoreEligible(GameSummaryData gameSummaryData)
    {
        return true;
    }

    public GameSummaryData[] AddScoreToBoard(GameSummaryData[] boardToEdit, GameSummaryData gameSummaryData)
    {
        int i = 0;
        while (true)
        {
            if (boardToEdit[i] == null || boardToEdit[i].score < gameSummaryData.score)
            {
                break;
            }
            i++;
        }

        if (i > 9)
        {
            return boardToEdit;
        }

        if (boardToEdit[i] == null)
        {
            boardToEdit[i] = gameSummaryData;

            return boardToEdit;
        }

        for (int j = 9; j > i; j--)
        {
            boardToEdit[j] = boardToEdit[j - 1];
        }

        boardToEdit[i] = gameSummaryData;

        return boardToEdit;
    }

    public void AddScoreToBoard(GameSummaryData gameSummaryData)
    {
        if (gameSummaryData.numberOfPlayers == 1)
        {
            onePlayerBoard = AddScoreToBoard(onePlayerBoard, gameSummaryData);
        }
        else if (gameSummaryData.numberOfPlayers == 2)
        {
            twoPlayerBoard = AddScoreToBoard(twoPlayerBoard, gameSummaryData);
        }
        else if (gameSummaryData.numberOfPlayers == 3)
        {
            threePlayerBoard = AddScoreToBoard(threePlayerBoard, gameSummaryData);
        }
        else
        {
            fourPlayerBoard = AddScoreToBoard(fourPlayerBoard, gameSummaryData);
        }
    }
}
