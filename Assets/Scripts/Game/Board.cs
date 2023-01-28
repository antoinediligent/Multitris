using System.Collections;
using System.Collections.Generic;
using Menu;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    private PlayerControls controls;

    // TODO : player 1 keyboard stuff
    private float lastHInput;
    private float lastVInput;

    int debugCounter = 0;
    bool ok = true;
    private bool goingDown;

    public RectInt Bounds {
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

        if (numberOfPlayers == 1)
        {
            grid.transform.position = new Vector3(5, 0);
            camera.orthographicSize = 12;
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
        }
        else if (numberOfPlayers == 4)
        {
            grid.transform.position = new Vector3(-10, 0);
            camera.orthographicSize = 16;
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
                    pauseMenu.SetActive(false);
                    isGamePaused = false;
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

        if (players[1].movingDirection != Player.NOT_MOVING)
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

        if (Time.time > lastHInput + 0.2f)
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            players[0].piece.Rotate(tilemap);
        }

        if (goingDown && lastVInput + 0.03f < Time.time)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                lastVInput = Time.time;
                ok = players[0].piece.Down(tilemap);

                if (!ok)
                {
                    CheckLines(players[0].GetPlayerNumber());
                    players[0].NewPiece(this, tilemap);
                    goingDown = false;
                }
            }
        }
        else if (lastVInput + 0.2f < Time.time)
        {
            ok = true;
            if (Input.GetKey(KeyCode.DownArrow))
            {
                lastVInput = Time.time;
                ok = players[0].piece.Down(tilemap);
                goingDown = true;
            }

            if (!ok)
            {
                CheckLines(players[0].GetPlayerNumber());
                players[0].NewPiece(this, tilemap);
                goingDown = false;
            }
        }

        if (Time.time > lastDownUpdate + 1.0f)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                if (players[i].piece == null)
                {
                    Debug.Log("players[i].piece == null");
                    Application.Quit();
                }

                ok = players[i].piece.Down(tilemap);

                if (!ok)
                {
                    CheckLines(players[i].GetPlayerNumber());
                    players[i].NewPiece(this, tilemap);
                    players[i].movingDirection = Player.NOT_MOVING;
                }
            }

            lastDownUpdate = Time.time;
        }
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
            players[playerNumber].piece.Down(tilemap);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        // The position is only valid if every cell is valid
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            // An out of bounds tile is invalid
            if (!bounds.Contains((Vector2Int)tilePosition)) {
                Debug.Log("IsValidPosition false bounds " + tilePosition);
                return false;
            }

            // A tile already occupies the position, thus invalid
            if (tilemap.HasTile(tilePosition)) {
                Debug.Log("IsValidPosition false HasTile");
                return false;
            }
        }

        return true;
    }

    /**
     * Check if one or more lines are finished
     * trigPlayer = the player who triggered this verification,
     * needed to avoid a bug when hiding his piece
     */
    public void CheckLines(int trigPlayer)
    {
        ArrayList linesToClear = new ArrayList();
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

        if (linesToClear.Count > 1)
        {
            Debug.Log("Lines combo of " + linesToClear.Count);
        }

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
}