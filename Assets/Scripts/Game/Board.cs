using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Menu;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class Board : MonoBehaviour
{
    public Camera camera;
    private Vector2Int boardSize;

    private int numberOfPlayers;
    public List<Player> players = new List<Player>();

    public Sprite[] playerSprites;

    private Tilemap tilemap;

    // Game push the piece down
    private float lastDownUpdate;

    private GameObject pauseMenu;
    private bool isGamePaused = false;

    private ScoreCalculator scoreCalculator;
    private float gameBeginning;

    private PlayerControls controls;

    // TODO : player 1 keyboard stuff
    private float lastHInput;
    private float lastVInput;

    private bool goingDown;

    private GameObject gameOverMenu;

    private RectInt Bounds {
        get
        {
            Vector2Int position = new Vector2Int(0, 0);
            return new RectInt(position, boardSize);
        }
    }

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.MoveUp.performed += ctx => players[1].piece.Rotate(tilemap);

        controls.Gameplay.MoveLeft.started += ctx => StartMoveLeft();
        controls.Gameplay.MoveLeft.canceled += ctx => StopMove();

        controls.Gameplay.MoveRight.started += ctx => StartMoveRight();
        controls.Gameplay.MoveRight.canceled += ctx => StopMove();

        controls.Gameplay.MoveDown.started += ctx => StartMoveDown();
        controls.Gameplay.MoveDown.canceled += ctx => StopMove();
    }

    void StartMoveLeft()
    {
        players[1].movingDirection = Player.MOVING_LEFT;
        players[1].piece.MoveLeft(tilemap);
        players[1].lastDirInputTime = Time.time;
    }

    void StartMoveRight()
    {
        players[1].movingDirection = Player.MOVING_RIGHT;
        players[1].piece.MoveRight(tilemap);
        players[1].lastDirInputTime = Time.time;
    }

    void StartMoveDown()
    {
        players[1].movingDirection = Player.MOVING_DOWN;
        players[1].piece.Down(tilemap);
        players[1].lastDirInputTime = Time.time;
    }

    void StopMove()
    {
        players[1].movingDirection = Player.NOT_MOVING;
        players[1].superMove = false;
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Start()
    {
        numberOfPlayers = StaticClass.NumberOfPlayers;
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

        scoreCalculator = new ScoreCalculator(levelText, scoreText, lineText);

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

        pauseMenu = GameObject.Find("PauseMenuCanvas");
        // PauseMenu must be enabled in the editor for the GameObject.Find to work
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }

        gameOverMenu = GameObject.Find("GameOverMenuCanvas");
        if (gameOverMenu.activeSelf)
        {
            gameOverMenu.SetActive(false);
        }

        gameBeginning = Time.time;
    }

    void Update()
    {
        // Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu != null)
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
            else
            {
                Debug.Log("pauseMenu == null");
            }
        }
        // End Pause Menu

        // Gamepad controls
        if (!isGamePaused && numberOfPlayers > 1 && players[1].movingDirection != Player.NOT_MOVING)
        {
            if (Time.time > players[1].lastDirInputTime + 0.2f)
            {
                MovePlayer(1);
                players[1].lastDirInputTime = Time.time;
                players[1].superMove = true;
            }
            else if (players[1].superMove && Time.time > players[1].lastDirInputTime + 0.03f)
            {
                MovePlayer(1);
                players[1].lastDirInputTime = Time.time;
            }
        }
        // End Gamepad controls

        if (!isGamePaused && Time.time > lastHInput + 0.2f)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                players[0].piece.MoveLeft(tilemap);
                lastHInput = Time.time;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                players[0].piece.MoveRight(tilemap);
                lastHInput = Time.time;
            }
        }

        if (!isGamePaused && Input.GetKeyDown(KeyCode.UpArrow))
        {
            players[0].piece.Rotate(tilemap);
        }

        if (!isGamePaused && goingDown && lastVInput + 0.03f < Time.time)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                lastVInput = Time.time;
                int collisionResult = players[0].piece.Down(tilemap);

                if (collisionResult == Piece.BOARD_COLLISION)
                {
                    CheckLines(players[0].GetPlayerNumber());
                    if (!IsGameOver())
                    {
                        players[0].NewPiece(this, tilemap);
                        goingDown = false;
                    }
                    else
                    {
                        GameOver();
                    }
                }
            }
        }
        else if (!isGamePaused && lastVInput + 0.2f < Time.time)
        {
            int collisionResult = 0;
            if (Input.GetKey(KeyCode.DownArrow))
            {
                lastVInput = Time.time;
                collisionResult = players[0].piece.Down(tilemap);
                goingDown = true;
            }

            if (collisionResult == Piece.BOARD_COLLISION)
            {
                CheckLines(players[0].GetPlayerNumber());
                if (!IsGameOver())
                {
                    players[0].NewPiece(this, tilemap);
                    goingDown = false;
                }
                else
                {
                    GameOver();
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
                    CheckLines(players[i].GetPlayerNumber());
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
        float currentLevel = ((Time.time - gameBeginning) / 30) + 1;
        scoreCalculator.SetLevel((int) Math.Round(currentLevel));
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
                CheckLines(players[playerNumber].GetPlayerNumber());
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
    public void CheckLines(int trigPlayer)
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

            if (nbFilled == tilemap.size.x)
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
        gameOverMenu.SetActive(true);
    }
}