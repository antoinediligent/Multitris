using System.Collections.Generic;
using TMPro;

namespace Game
{
    public class ScoreCalculator
    {
        private int level;
        private int score;
        private int lines;

        private TMP_Text levelText;
        private TMP_Text scoreText;
        private TMP_Text lineText;

        private Dictionary<int, PlayerStats> playersStats = new Dictionary<int, PlayerStats>();

        public ScoreCalculator(TMP_Text levelText, TMP_Text scoreText, TMP_Text lineText, int numberOfPlayers)
        {
            // Always begins at level 1
            level = 1;
            this.levelText = levelText;
            this.scoreText = scoreText;
            this.lineText = lineText;

            for (int i = 1; i <= numberOfPlayers; i++)
            {
                playersStats.Add(i, new PlayerStats());
            }
        }

        public void LineFinished(int playerNumber, int numberOfLines)
        {
            if (numberOfLines == 1)
            {
                score += level * 40;
            }
            else if (numberOfLines == 2)
            {
                score += level * 100;
            }
            else if (numberOfLines == 3)
            {
                score += level * 300;
            }
            else if (numberOfLines == 4)
            {
                score += level * 1200;
            }

            lines += numberOfLines;

            scoreText.text = score.ToString();
            lineText.text = lines.ToString();

            playersStats[playerNumber].lines += numberOfLines;
        }

        public int GetLevel()
        {
            return level;
        }

        public void SetLevel(int newLevel)
        {
            level = newLevel;
            levelText.text = level.ToString();
        }

        public void AddPiece(int playerNumber)
        {
            playersStats[playerNumber].pieces += 1;
        }

        public PlayerStats GetPlayerStats(int playerNumber)
        {
            return playersStats[playerNumber];
        }
    }

    public class PlayerStats
    {
        public int pieces;
        public int lines;
    }
}