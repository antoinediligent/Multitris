
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

    public bool IsGameScoreEligible(GameSummaryData[] boardToEdit, GameSummaryData gameSummaryData)
    {
        int i = 0;
        while (true)
        {
            if (i == 10 || boardToEdit[i] == null || boardToEdit[i].score < gameSummaryData.score)
            {
                break;
            }
            i++;
        }

        if (i > 9)
        {
            return false;
        }

        return true;
    }

    public bool IsGameScoreEligible(GameSummaryData gameSummaryData)
    {
        bool isGameScoreEligible;
        if (gameSummaryData.numberOfPlayers == 1)
        {
            isGameScoreEligible = IsGameScoreEligible(onePlayerBoard, gameSummaryData);
        }
        else if (gameSummaryData.numberOfPlayers == 2)
        {
            isGameScoreEligible = IsGameScoreEligible(twoPlayerBoard, gameSummaryData);
        }
        else if (gameSummaryData.numberOfPlayers == 3)
        {
            isGameScoreEligible = IsGameScoreEligible(threePlayerBoard, gameSummaryData);
        }
        else
        {
            isGameScoreEligible = IsGameScoreEligible(fourPlayerBoard, gameSummaryData);
        }

        return isGameScoreEligible;
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

    public GameSummaryData[] GetHighScoreBoard(int numberOfPlayers)
    {
        if (numberOfPlayers == 1)
        {
            return onePlayerBoard;
        }

        if (numberOfPlayers == 2)
        {
            return twoPlayerBoard;
        }

        if (numberOfPlayers == 3)
        {
            return threePlayerBoard;
        }

        if (numberOfPlayers == 4)
        {
            return fourPlayerBoard;
        }

        return null;
    }
}
