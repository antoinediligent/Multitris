using TMPro;
using UnityEngine;

public class RecordsMenu : MonoBehaviour
{
    public static string PSEUDO_PLACEHOLDER = "";
    public static string SCORE_PLACEHOLDER = "0";

    public void DisplayRecordsBoard(int numberOfPlayers)
    {
        GameObject recordsMenu = GameObject.Find("RecordsMenu");
        recordsMenu.SetActive(false);

        GameObject recordsBoard = FindInActiveObjectByName("RecordsBoard");
        recordsBoard.SetActive(true);


        TMP_Text numberOfPlayersLabel = GameObject.Find("NumberOfPlayersLabel").GetComponent<TMP_Text>();
        if (numberOfPlayers == 1)
        {
            numberOfPlayersLabel.text = "Records 1 joueur";
        }
        else if (numberOfPlayers == 2)
        {
            numberOfPlayersLabel.text = "Records 2 joueurs";
        }
        else if (numberOfPlayers == 3)
        {
            numberOfPlayersLabel.text = "Records 3 joueurs";
        }
        else if (numberOfPlayers == 4)
        {
            numberOfPlayersLabel.text = "Records 4 joueurs";
        }

        HighScoreBoardData highScoreBoardData = SaveSystem.LoadHighScoreBoard();
        GameSummaryData[] highScoreBoard = highScoreBoardData.GetHighScoreBoard(numberOfPlayers);

        // One
        TMP_Text onePseudoText = GameObject.Find("OnePseudo").GetComponent<TMP_Text>();
        TMP_Text oneScoreText = GameObject.Find("OneScore").GetComponent<TMP_Text>();
        if (highScoreBoard[0] != null)
        {
            onePseudoText.SetText(highScoreBoard[0].GetPlayerNamesForHighScoreBoard());
            oneScoreText.SetText(highScoreBoard[0].score.ToString());
        }
        else
        {
            onePseudoText.SetText(PSEUDO_PLACEHOLDER);
            oneScoreText.SetText(SCORE_PLACEHOLDER);
        }

        // Two
        TMP_Text twoPseudoText = GameObject.Find("TwoPseudo").GetComponent<TMP_Text>();
        TMP_Text twoScoreText = GameObject.Find("TwoScore").GetComponent<TMP_Text>();
        if (highScoreBoard[1] != null)
        {
            twoPseudoText.SetText(highScoreBoard[1].GetPlayerNamesForHighScoreBoard());
            twoScoreText.SetText(highScoreBoard[1].score.ToString());
        }
        else
        {
            twoPseudoText.SetText(PSEUDO_PLACEHOLDER);
            twoScoreText.SetText(SCORE_PLACEHOLDER);
        }

        // Three
        TMP_Text threePseudoText = GameObject.Find("ThreePseudo").GetComponent<TMP_Text>();
        TMP_Text threeScoreText = GameObject.Find("ThreeScore").GetComponent<TMP_Text>();
        if (highScoreBoard[2] != null)
        {
            threePseudoText.SetText(highScoreBoard[2].GetPlayerNamesForHighScoreBoard());
            threeScoreText.SetText(highScoreBoard[2].score.ToString());
        }
        else
        {
            threePseudoText.SetText(PSEUDO_PLACEHOLDER);
            threeScoreText.SetText(SCORE_PLACEHOLDER);
        }

        // Four
        TMP_Text fourPseudoText = GameObject.Find("FourPseudo").GetComponent<TMP_Text>();
        TMP_Text fourScoreText = GameObject.Find("FourScore").GetComponent<TMP_Text>();
        if (highScoreBoard[3] != null)
        {
            fourPseudoText.SetText(highScoreBoard[3].GetPlayerNamesForHighScoreBoard());
            fourScoreText.SetText(highScoreBoard[3].score.ToString());
        }
        else
        {
            fourPseudoText.SetText(PSEUDO_PLACEHOLDER);
            fourScoreText.SetText(SCORE_PLACEHOLDER);
        }

        // Five
        TMP_Text fivePseudoText = GameObject.Find("FivePseudo").GetComponent<TMP_Text>();
        TMP_Text fiveScoreText = GameObject.Find("FiveScore").GetComponent<TMP_Text>();
        if (highScoreBoard[4] != null)
        {
            fivePseudoText.SetText(highScoreBoard[4].GetPlayerNamesForHighScoreBoard());
            fiveScoreText.SetText(highScoreBoard[4].score.ToString());
        }
        else
        {
            fivePseudoText.SetText(PSEUDO_PLACEHOLDER);
            fiveScoreText.SetText(SCORE_PLACEHOLDER);
        }

        // Six
        TMP_Text sixPseudoText = GameObject.Find("SixPseudo").GetComponent<TMP_Text>();
        TMP_Text sixScoreText = GameObject.Find("SixScore").GetComponent<TMP_Text>();
        if (highScoreBoard[5] != null)
        {
            sixPseudoText.SetText(highScoreBoard[5].GetPlayerNamesForHighScoreBoard());
            sixScoreText.SetText(highScoreBoard[5].score.ToString());
        }
        else
        {
            sixPseudoText.SetText(PSEUDO_PLACEHOLDER);
            sixScoreText.SetText(SCORE_PLACEHOLDER);
        }

        // Seven
        TMP_Text sevenPseudoText = GameObject.Find("SevenPseudo").GetComponent<TMP_Text>();
        TMP_Text sevenScoreText = GameObject.Find("SevenScore").GetComponent<TMP_Text>();
        if (highScoreBoard[6] != null)
        {
            sevenPseudoText.SetText(highScoreBoard[6].GetPlayerNamesForHighScoreBoard());
            sevenScoreText.SetText(highScoreBoard[6].score.ToString());
        }
        else
        {
            sevenPseudoText.SetText(PSEUDO_PLACEHOLDER);
            sevenScoreText.SetText(SCORE_PLACEHOLDER);
        }

        // Eight
        TMP_Text eightPseudoText = GameObject.Find("EightPseudo").GetComponent<TMP_Text>();
        TMP_Text eightScoreText = GameObject.Find("EightScore").GetComponent<TMP_Text>();
        if (highScoreBoard[7] != null)
        {
            eightPseudoText.SetText(highScoreBoard[7].GetPlayerNamesForHighScoreBoard());
            eightScoreText.SetText(highScoreBoard[7].score.ToString());
        }
        else
        {
            eightPseudoText.SetText(PSEUDO_PLACEHOLDER);
            eightScoreText.SetText(SCORE_PLACEHOLDER);
        }

        // Nine
        TMP_Text ninePseudoText = GameObject.Find("NinePseudo").GetComponent<TMP_Text>();
        TMP_Text nineScoreText = GameObject.Find("NineScore").GetComponent<TMP_Text>();
        if (highScoreBoard[8] != null)
        {
            ninePseudoText.SetText(highScoreBoard[8].GetPlayerNamesForHighScoreBoard());
            nineScoreText.SetText(highScoreBoard[8].score.ToString());
        }
        else
        {
            ninePseudoText.SetText(PSEUDO_PLACEHOLDER);
            nineScoreText.SetText(SCORE_PLACEHOLDER);
        }

        // Ten
        TMP_Text tenPseudoText = GameObject.Find("TenPseudo").GetComponent<TMP_Text>();
        TMP_Text tenScoreText = GameObject.Find("TenScore").GetComponent<TMP_Text>();
        if (highScoreBoard[9] != null)
        {
            tenPseudoText.SetText(highScoreBoard[9].GetPlayerNamesForHighScoreBoard());
            tenScoreText.SetText(highScoreBoard[9].score.ToString());
        }
        else
        {
            tenPseudoText.SetText(PSEUDO_PLACEHOLDER);
            tenScoreText.SetText(SCORE_PLACEHOLDER);
        }
    }

    GameObject FindInActiveObjectByName(string searchedName)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == searchedName)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
