using System.Collections;
using Menu;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Camera camera;
    private Vector2Int boardSize;

    public Sprite playerOneSprite;
    public Sprite playerTwoSprite;

    private Tilemap tilemap;
    private Vector3Int spawnSpot = new Vector3Int(4, 19);

    private float lastUpdate;
    private Piece activePiece;

    private GameObject pauseMenu;
    private bool isGamePaused = false;

    private float lastHInput;
    private float lastVInput;

    int debugCounter = 0;
    bool ok = true;
    private bool goingDown = false;

    public RectInt Bounds {
        get
        {
            Vector2Int position = new Vector2Int(0, 0);
            return new RectInt(position, boardSize);
        }
    }

    void Start()
    {
        int numberOfPlayers = StaticClass.NumberOfPlayers;
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

        tilemap = GetComponentInChildren<Tilemap>();

        activePiece = NewPiece();
        activePiece.SetTiles(tilemap);

        pauseMenu = GameObject.Find("PauseMenuCanvas");
        // PauseMenu must be enabled in the editor for the GameObject.Find to work
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
    }

    Piece NewPiece()
    {
        int pieceNumber = Random.Range(1, 8);

        Piece piece;

        switch (pieceNumber)
        {
            case 1:
                piece = new I(spawnSpot, playerOneSprite);
                break;

            case 2:
                piece = new J(spawnSpot, playerOneSprite);
                break;

            case 3:
                piece = new L(spawnSpot, playerOneSprite);
                break;

            case 4:
                piece = new O(spawnSpot, playerOneSprite);
                break;

            case 5:
                piece = new S(spawnSpot, playerOneSprite);
                break;

            case 6:
                piece = new T(spawnSpot, playerOneSprite);
                break;

            default:
                piece = new Z(spawnSpot, playerOneSprite);
                break;
        }

        piece.board = this;

        return piece;
    }

    void Update()
    {
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

        if (Time.time > lastHInput + 0.2f)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                activePiece.MoveLeft(tilemap);
                lastHInput = Time.time;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                activePiece.MoveRight(tilemap);
                lastHInput = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            activePiece.Rotate(tilemap);
        }

        if (goingDown && lastVInput + 0.03f < Time.time)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                lastVInput = Time.time;
                ok = activePiece.Down(tilemap);

                if (!ok)
                {
                    CheckLines();
                    activePiece = NewPiece();
                    activePiece.SetTiles(tilemap);
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
                ok = activePiece.Down(tilemap);
                goingDown = true;
            }

            if (!ok)
            {
                CheckLines();
                activePiece = NewPiece();
                activePiece.SetTiles(tilemap);
                goingDown = false;
            }
        }

        if (Time.time > lastUpdate + 1.0f)
        {
            ok = activePiece.Down(tilemap);
            lastUpdate = Time.time;

            if (!ok)
            {
                CheckLines();
                activePiece = NewPiece();
                activePiece.SetTiles(tilemap);
                goingDown = false;
            }
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
     */
    public void CheckLines()
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

        linesToClear.Sort();
        linesToClear.Reverse();

        if (linesToClear.Count > 1)
        {
            Debug.Log("Lines combo of " + linesToClear.Count);
        }

        foreach (int lineNumber in linesToClear)
        {
            for (int i = lineNumber; i < tilemap.size.y - 1; i++)
            {
                for (int j = 0; j < tilemap.size.x; j++)
                {
                    tilemap.SetTile(new Vector3Int(j, i), tilemap.GetTile(new Vector3Int(j, i+1)));
                }
            }
        }
    }
}