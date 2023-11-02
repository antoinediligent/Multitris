using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    public Camera camera;
    private Vector2Int boardSize;

    private int numberOfPlayers;
    public List<Player> players = new List<Player>();

    public Sprite[] playerSprites;

    private Tilemap tilemap;

    private GameObject pauseMenu;
    private bool isGamePaused = false;

    // Game push the piece down
    private float lastDownUpdate;

    private ScoreCalculator scoreCalculator;
    private float gameBeginning;

    private GameObject gameOverMenu;
    private bool isGameOver;

    private GameObject scoreScreenCanvas;
    private GameObject saveCanvas;
    private GameObject inputErrorExplanation;

    private RectInt Bounds {
        get
        {
            Vector2Int position = new Vector2Int(0, 0);
            return new RectInt(position, boardSize);
        }
    }

    public void PlayerMove(int playerIndex, Vector2 vector2)
    {
        // Protection if more gamepads than player are plugged
        if (players.Count <= playerIndex)
        {
            return;
        }

        Player currentPlayer = players[playerIndex];
        if (currentPlayer.lastDirInputTime + 0.1f < Time.time)
        {
            if (vector2.x >= 1)
            {
                currentPlayer.piece.MoveRight(tilemap);
                currentPlayer.lastDirInputTime = Time.time;
            }
            else if (vector2.x <= -1)
            {
                currentPlayer.piece.MoveLeft(tilemap);
                currentPlayer.lastDirInputTime = Time.time;
            }
            else if (vector2.y >= 1)
            {
                currentPlayer.piece.Rotate(tilemap);
                currentPlayer.lastDirInputTime = Time.time;
            }
            else if (vector2.y <= -1)
            {
                currentPlayer.piece.Down(tilemap);
                currentPlayer.lastDirInputTime = Time.time;
            }
        }
    }

    public void PlayerSlowTap(int playerIndex, string phase, string controlName)
    {
        // Protection if more gamepads than player are plugged
        if (players.Count <= playerIndex)
        {
            return;
        }

        Player currentPlayer = players[playerIndex];
        if (controlName.Contains("right"))
        {
            if (phase.Equals("Performed"))
            {
                currentPlayer.movingDirection = Player.MOVING_RIGHT;
                currentPlayer.lastDirInputTime = Time.time;
            }
            else
            {
                currentPlayer.movingDirection = Player.NOT_MOVING;
                currentPlayer.lastDirInputTime = Time.time;
            }
        }
        else if (controlName.Contains("left"))
        {
            if (phase.Equals("Performed"))
            {
                currentPlayer.movingDirection = Player.MOVING_LEFT;
                currentPlayer.lastDirInputTime = Time.time;
            }
            else
            {
                currentPlayer.movingDirection = Player.NOT_MOVING;
                currentPlayer.lastDirInputTime = Time.time;
            }
        }
        else if (controlName.Contains("down"))
        {
            if (phase.Equals("Performed"))
            {
                currentPlayer.movingDirection = Player.MOVING_DOWN;
                currentPlayer.lastDirInputTime = Time.time;
            }
            else
            {
                currentPlayer.movingDirection = Player.NOT_MOVING;
                currentPlayer.lastDirInputTime = Time.time;
            }
        }
    }

    void Start()
    {
        numberOfPlayers = MenuStaticClass.NumberOfPlayers;
        if (numberOfPlayers == 0)
        {
            numberOfPlayers = 1;
        }
        Debug.Log("numberOfPlayers=" + numberOfPlayers);

        boardSize = new Vector2Int(numberOfPlayers * 10, 20);

        GameObject background = GameObject.Find("Background");
        SpriteRenderer backgroundSpriteRenderer = background.GetComponent<SpriteRenderer>();
        backgroundSpriteRenderer.size = new Vector2(numberOfPlayers * 10, 20);

        GameObject grid = GameObject.Find("Grid");
        tilemap = GetComponentInChildren<Tilemap>();

        GameObject levelLabel = GameObject.Find("LevelLabel");
        GameObject levelTextContainer = GameObject.Find("Level");
        TMP_Text levelText = levelTextContainer.GetComponent<TMP_Text>();

        GameObject scoreLabel = GameObject.Find("ScoreLabel");
        GameObject scoreTextContainer = GameObject.Find("Score");
        TMP_Text scoreText = scoreTextContainer.GetComponent<TMP_Text>();

        GameObject lineLabel = GameObject.Find("LineLabel");
        GameObject lineTextContainer = GameObject.Find("Line");
        TMP_Text lineText = lineTextContainer.GetComponent<TMP_Text>();

        scoreCalculator = new ScoreCalculator(levelText, scoreText, lineText, numberOfPlayers);

        if (numberOfPlayers == 1)
        {
            grid.transform.position = new Vector3(5, 0);
            camera.orthographicSize = 12;

            int onePlayerAlignX = 22;
            levelLabel.transform.position = new Vector3(onePlayerAlignX, 19);
            levelTextContainer.transform.position = new Vector3(onePlayerAlignX, 17);

            scoreLabel.transform.position = new Vector3(onePlayerAlignX, 14);
            scoreTextContainer.transform.position = new Vector3(onePlayerAlignX, 12);

            lineLabel.transform.position = new Vector3(onePlayerAlignX, 9);
            lineTextContainer.transform.position = new Vector3(onePlayerAlignX, 7);
        }
        else if (numberOfPlayers == 2)
        {
            grid.transform.position = new Vector3(0, 0);
            camera.orthographicSize = 12;
        }
        else if (numberOfPlayers == 3)
        {
            grid.transform.position = new Vector3(-5, 0);
            camera.orthographicSize = 14;

            int threePlayerAlignX = 31;
            levelLabel.transform.position = new Vector3(threePlayerAlignX, 19);
            levelTextContainer.transform.position = new Vector3(threePlayerAlignX, 17);

            scoreLabel.transform.position = new Vector3(threePlayerAlignX, 14);
            scoreTextContainer.transform.position = new Vector3(threePlayerAlignX, 12);

            lineLabel.transform.position = new Vector3(threePlayerAlignX, 9);
            lineTextContainer.transform.position = new Vector3(threePlayerAlignX, 7);
        }
        else if (numberOfPlayers == 4)
        {
            grid.transform.position = new Vector3(-10, 0);
            camera.orthographicSize = 16;

            int fourPlayerAlignX = 36;
            levelLabel.transform.position = new Vector3(fourPlayerAlignX, 19);
            levelTextContainer.transform.position = new Vector3(fourPlayerAlignX, 17);

            scoreLabel.transform.position = new Vector3(fourPlayerAlignX, 14);
            scoreTextContainer.transform.position = new Vector3(fourPlayerAlignX, 12);

            lineLabel.transform.position = new Vector3(fourPlayerAlignX, 9);
            lineTextContainer.transform.position = new Vector3(fourPlayerAlignX, 7);
        }

        for (int i = 1; i <= numberOfPlayers; i++)
        {
            Player player = new Player(i, playerSprites[i-1]);
            player.NewPiece(this, tilemap);
            players.Add(player);
        }

        pauseMenu = FindInActiveObjectByName("PauseMenuCanvas");
        // PauseMenu must be enabled in the editor for the GameObject.Find to work
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }

        gameOverMenu = FindInActiveObjectByName("GameOverMenuCanvas");
        if (gameOverMenu.activeSelf)
        {
            gameOverMenu.SetActive(false);
        }

        scoreScreenCanvas = FindInActiveObjectByName("ScoreScreenCanvas");
        if (scoreScreenCanvas.activeSelf)
        {
            scoreScreenCanvas.SetActive(false);
        }

        inputErrorExplanation = FindInActiveObjectByName("InputErrorExplanation");
        inputErrorExplanation.SetActive(false);

        saveCanvas = FindInActiveObjectByName("SaveCanvas");
        if (saveCanvas.activeSelf)
        {
            saveCanvas.SetActive(false);
        }

        gameBeginning = Time.time;
    }

    void Update()
    {
        // Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGameOver)
            {
                if (pauseMenu.activeSelf)
                {
                    OutOfPause();
                }
                else
                {
                    pauseMenu.SetActive(true);
                    isGamePaused = true;
                }
            }
        }
        // End Pause Menu

        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (!isGamePaused && players[i].movingDirection != Player.NOT_MOVING)
            {
                if (Time.time > players[i].lastDirInputTime + 0.05f)
                {
                    MovePlayer(i);
                    players[i].lastDirInputTime = Time.time;
                }
            }
        }

        if (!isGamePaused && Time.time > lastDownUpdate + (1.0f - scoreCalculator.GetLevel() * 0.05))
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                if (players[i].piece == null)
                {
                    Debug.Log("players[i].piece == null");
                    Application.Quit();
                }

                int collisionResult = players[i].piece.Down(tilemap);
                if (collisionResult == Piece.BOARD_COLLISION)
                {
                    CheckLines(players[i].GetPlayerNumber(), numberOfPlayers);
                    if (!IsGameOver())
                    {
                        players[i].NewPiece(this, tilemap);
                        players[i].movingDirection = Player.NOT_MOVING;
                    }
                    else
                    {
                        GameOver();
                    }
                }
            }

            lastDownUpdate = Time.time;
        }

        // Calculate current level
        // Start at level one
        if (!isGamePaused)
        {
            float currentLevel = ((Time.time - gameBeginning) / 30) + 1;
            scoreCalculator.SetLevel((int)Math.Round(currentLevel));
        }
    }

    public void OutOfPause()
    {
        pauseMenu.SetActive(false);
        isGamePaused = false;
    }

    void MovePlayer(int playerNumber)
    {
        if (players[playerNumber].movingDirection == Player.MOVING_LEFT)
        {
            players[playerNumber].piece.MoveLeft(tilemap);
        }
        else if (players[playerNumber].movingDirection == Player.MOVING_RIGHT)
        {
            players[playerNumber].piece.MoveRight(tilemap);
        }
        else if (players[playerNumber].movingDirection == Player.MOVING_DOWN)
        {
            int collisionResult = players[playerNumber].piece.Down(tilemap);
            if (collisionResult == Piece.BOARD_COLLISION)
            {
                CheckLines(players[playerNumber].GetPlayerNumber(), numberOfPlayers);
                if (!IsGameOver())
                {
                    players[playerNumber].NewPiece(this, tilemap);
                    players[playerNumber].movingDirection = Player.NOT_MOVING;
                }
                else
                {
                    GameOver();
                }
            }
        }
    }

    public int IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        // The position is only valid if every cell is valid
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            // An out of bounds tile is invalid
            if (!bounds.Contains((Vector2Int)tilePosition)) {
                Debug.Log("IsValidPosition false bounds " + tilePosition);
                return Piece.BOARD_COLLISION;
            }

            // A tile already occupies the position, thus invalid
            if (tilemap.HasTile(tilePosition)) {

                GameTile gameTile = (GameTile) tilemap.GetTile(tilePosition);
                if (gameTile.gameTag.Equals(GameTile.ACTIVE_PIECE))
                {
                    return Piece.OTHER_PLAYER_COLLISION;
                }

                return Piece.BOARD_COLLISION;
            }
        }

        return Piece.NO_COLLISION;
    }

    /**
     * Check if one or more lines are finished
     * trigPlayer = the player who triggered this verification,
     * needed to avoid a bug when hiding his piece
     */
    public void CheckLines(int trigPlayer, int numberOfPlayers)
    {
        ArrayList linesToClear = new ArrayList(4);
        for (int i = 0; i < tilemap.size.y - 1; i++)
        {
            int nbFilled = 0;
            for (int j = 0; j < tilemap.size.x; j++)
            {
                if (tilemap.HasTile(new Vector3Int(j, i)))
                {
                    nbFilled++;
                }
            }

            if (nbFilled == numberOfPlayers * 10)
            {
                linesToClear.Add(i);
            }
        }

        if (linesToClear.Count == 0)
        {
            return;
        }

        linesToClear.Sort();
        linesToClear.Reverse();

        scoreCalculator.LineFinished(trigPlayer, linesToClear.Count);

        // Can't hide the triggering player piece, it needs to move with the line
        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (players[i].GetPlayerNumber() != trigPlayer)
            {
                players[i].HidePiece(tilemap);
            }
        }

        foreach (int lineNumber in linesToClear)
        {
            for (int i = lineNumber; i < tilemap.size.y - 1; i++)
            {
                for (int j = 0; j < tilemap.size.x; j++)
                {
                    Vector3Int copiedCell = new Vector3Int(j, i + 1);
                    Vector3Int copyingCell = new Vector3Int(j, i);

                    tilemap.SetTile(copyingCell, tilemap.GetTile(copiedCell));
                }
            }
        }

        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (players[i].GetPlayerNumber() != trigPlayer)
            {
                players[i].ShowPiece(tilemap);
            }
        }
    }

    bool IsGameOver()
    {
        for (int i = 0; i < tilemap.size.x; i++)
        {
            int secondToLastLine = boardSize.y - 1;
            Vector3Int tilePosition = new Vector3Int(i, secondToLastLine);
            if (tilemap.HasTile(tilePosition))
            {
                GameTile gameTile = (GameTile) tilemap.GetTile(tilePosition);
                if (!gameTile.gameTag.Equals(GameTile.ACTIVE_PIECE))
                {
                    return true;
                }
            }
        }

        return false;
    }

    // When game is over, pause the game and display a menu
    void GameOver()
    {
        isGamePaused = true;
        isGameOver = true;
        gameOverMenu.SetActive(true);
    }

    public void ScoreScreen()
    {
        gameOverMenu.SetActive(false);
        scoreScreenCanvas.SetActive(true);

        TMP_Text playerOnePiecesNumberText = GameObject.Find("PlayerOnePiecesNumber").GetComponent<TMP_Text>();
        TMP_Text playerOneLinesNumberText = GameObject.Find("PlayerOneLinesNumber").GetComponent<TMP_Text>();
        playerOnePiecesNumberText.text = scoreCalculator.GetPlayerStats(1).pieces.ToString();
        playerOneLinesNumberText.text = scoreCalculator.GetPlayerStats(1).lines.ToString();

        if (numberOfPlayers >= 2)
        {
            TMP_Text playerTwoPiecesNumberText = GameObject.Find("PlayerTwoPiecesNumber").GetComponent<TMP_Text>();
            TMP_Text playerTwoLinesNumberText = GameObject.Find("PlayerTwoLinesNumber").GetComponent<TMP_Text>();
            playerTwoPiecesNumberText.text = scoreCalculator.GetPlayerStats(2).pieces.ToString();
            playerTwoLinesNumberText.text = scoreCalculator.GetPlayerStats(2).lines.ToString();
        }

        if (numberOfPlayers >= 3)
        {
            TMP_Text playerThreePiecesNumberText = GameObject.Find("PlayerThreePiecesNumber").GetComponent<TMP_Text>();
            TMP_Text playerThreeLinesNumberText = GameObject.Find("PlayerThreeLinesNumber").GetComponent<TMP_Text>();
            playerThreePiecesNumberText.text = scoreCalculator.GetPlayerStats(3).pieces.ToString();
            playerThreeLinesNumberText.text = scoreCalculator.GetPlayerStats(3).lines.ToString();
        }

        if (numberOfPlayers == 4)
        {
            TMP_Text playerTwoPiecesNumberText = GameObject.Find("PlayerTwoPiecesNumber").GetComponent<TMP_Text>();
            TMP_Text playerTwoLinesNumberText = GameObject.Find("PlayerTwoLinesNumber").GetComponent<TMP_Text>();
            playerTwoPiecesNumberText.text = scoreCalculator.GetPlayerStats(2).pieces.ToString();
            playerTwoLinesNumberText.text = scoreCalculator.GetPlayerStats(2).lines.ToString();
        }

        TMP_Text totalScoreText = GameObject.Find("TotalScore").GetComponent<TMP_Text>();
        totalScoreText.text = scoreCalculator.GetScore().ToString();

        TMP_Text levelText = GameObject.Find("Level").GetComponent<TMP_Text>();
        levelText.text = scoreCalculator.GetLevel().ToString();

        TMP_Text totalLinesText = GameObject.Find("TotalLines").GetComponent<TMP_Text>();
        totalLinesText.text = scoreCalculator.GetLines().ToString();

        TMP_Text highscoreWarningText = GameObject.Find("HighscoreWarning").GetComponent<TMP_Text>();
        GameObject scoreScreenSaveButton = GameObject.Find("ScoreScreenSaveButton");


        GameSummaryData gameSummaryData = new GameSummaryData(numberOfPlayers, scoreCalculator);
        HighScoreBoardData highScoreBoardData = SaveSystem.LoadHighScoreBoard();

        // Display only if this game is in the high scores
        if (highScoreBoardData.IsGameScoreEligible(gameSummaryData))
        {
            highscoreWarningText.text = "Eligible au tableau des records";
            scoreScreenSaveButton.SetActive(true);
        }
        else
        {
            highscoreWarningText.text = "Non eligible au tableau des records";
            scoreScreenSaveButton.SetActive(false);
        }
    }

    public void SaveScreen()
    {
        scoreScreenCanvas.SetActive(false);
        saveCanvas.SetActive(true);
        inputErrorExplanation.SetActive(false);

        GameObject playerTwoNameInputField = GameObject.Find("PlayerTwoNameInputField");
        GameObject playerThreeNameInputField = GameObject.Find("PlayerThreeNameInputField");
        GameObject playerFourNameInputField = GameObject.Find("PlayerFourNameInputField");

        if (numberOfPlayers == 1)
        {
            GameObject.Find("PlayerTwoLabel").SetActive(false);
            playerTwoNameInputField.SetActive(false);
        }

        if (numberOfPlayers <= 2)
        {
            GameObject.Find("PlayerThreeLabel").SetActive(false);
            playerThreeNameInputField.SetActive(false);
        }

        if (numberOfPlayers <= 3)
        {
            GameObject.Find("PlayerFourLabel").SetActive(false);
            playerFourNameInputField.SetActive(false);
        }
    }

    /**
     * When the player click on the Save button
     */
    public void SaveGameSummary()
    {
        GameSummaryData gameSummaryData = new GameSummaryData(numberOfPlayers, scoreCalculator);

        TMP_Text playerOneNameTypedText = GameObject.Find("PlayerOneNameTypedText").GetComponent<TMP_Text>();
        // Use the number 4 here, because the input field always count character with +1
        if (playerOneNameTypedText.text.Length < 4)
        {
            inputErrorExplanation.SetActive(true);
            return;
        }
        else
        {
            gameSummaryData.playerNames[0] = playerOneNameTypedText.text;
        }

        if (numberOfPlayers >= 2)
        {
            TMP_Text playerTwoNameTypedText = GameObject.Find("PlayerTwoNameTypedText").GetComponent<TMP_Text>();
            if (playerTwoNameTypedText.text.Length < 4)
            {
                inputErrorExplanation.SetActive(true);
                return;
            }
            else
            {
                gameSummaryData.playerNames[1] = playerTwoNameTypedText.text;
            }
        }

        if (numberOfPlayers >= 3)
        {
            TMP_Text playerThreeNameTypedText = GameObject.Find("PlayerThreeNameTypedText").GetComponent<TMP_Text>();
            if (playerThreeNameTypedText.text.Length < 4)
            {
                inputErrorExplanation.SetActive(true);
                return;
            }
            else
            {
                gameSummaryData.playerNames[2] = playerThreeNameTypedText.text;
            }
        }

        if (numberOfPlayers >= 4)
        {
            TMP_Text playerFourNameTypedText = GameObject.Find("PlayerFourNameTypedText").GetComponent<TMP_Text>();
            if (playerFourNameTypedText.text.Length < 4)
            {
                inputErrorExplanation.SetActive(true);
                return;
            }
            else
            {
                gameSummaryData.playerNames[3] = playerFourNameTypedText.text;
            }
        }

        HighScoreBoardData highScoreBoardData = SaveSystem.LoadHighScoreBoard();
        highScoreBoardData.AddScoreToBoard(gameSummaryData);
        SaveSystem.SaveHighScoreBoard(highScoreBoardData);

        scoreScreenCanvas.SetActive(true);
        saveCanvas.SetActive(false);

        // Hide HighscoreWarning
        GameObject highscoreWarningText = FindInActiveObjectByName("HighscoreWarning");
        highscoreWarningText.SetActive(false);

        // ScoreScreenSaveButton
        GameObject scoreScreenSaveButton = FindInActiveObjectByName("ScoreScreenSaveButton");
        scoreScreenSaveButton.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /**
     * Needed to find inactive objects
     */
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