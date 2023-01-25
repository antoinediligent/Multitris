using Menu;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    private Vector2Int boardSize;

    public Sprite playerOneSprite;
    public Sprite playerTwoSprite;

    private Tilemap tilemap;
    private Vector3Int spawnSpot = new Vector3Int(4, 19);

    private float lastUpdate;
    private Piece activePiece;

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
        Debug.Log("numberOfPlayers=" + numberOfPlayers);

        GameObject background = GameObject.Find("Background");
        SpriteRenderer backgroundSpriteRenderer = background.GetComponent<SpriteRenderer>();
        GameObject grid = GameObject.Find("Grid");

        if (numberOfPlayers == 1)
        {
            boardSize = new Vector2Int(10, 20);
            // background.transform.position.Set(5, 10, 0);
            backgroundSpriteRenderer.size = new Vector2(10, 20);
            grid.transform.position = new Vector3(5, 0);
        }
        else if (numberOfPlayers == 2)
        {
            boardSize = new Vector2Int(20, 20);
            // background.transform.position.Set(10, 10, 0);
            backgroundSpriteRenderer.size = new Vector2(20, 20);
            grid.transform.position = new Vector3(0, 0);
        }

        tilemap = GetComponentInChildren<Tilemap>();

        activePiece = NewPiece();
        activePiece.SetTiles(tilemap);
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
}