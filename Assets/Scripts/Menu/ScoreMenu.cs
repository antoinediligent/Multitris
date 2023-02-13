using UnityEngine;

public class ScoreMenu : MonoBehaviour
{
    public void LoadGameSummary()
    {
        HighScoreBoardData highScoreBoardData = SaveSystem.LoadHighScoreBoard();
        Debug.Log(highScoreBoardData.onePlayerBoard.Length);

        int i = 0;
        foreach (GameSummaryData gameSummaryData in highScoreBoardData.onePlayerBoard)
        {
            if (gameSummaryData != null)
            {
                Debug.Log(i + " " + gameSummaryData.lines + " " + gameSummaryData.score);
            }
        }
    }
}
