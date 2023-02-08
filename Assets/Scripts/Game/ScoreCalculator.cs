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

        public ScoreCalculator(TMP_Text levelText, TMP_Text scoreText, TMP_Text lineText)
        {
            // Always begins at level 1
            level = 1;
            this.levelText = levelText;
            this.scoreText = scoreText;
            this.lineText = lineText;
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
    }
}