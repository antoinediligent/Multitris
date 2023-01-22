using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Vector2Int boardSize = new Vector2Int(10, 20);

    public Sprite playerOneSprite;
    public Sprite playerTwoSprite;

    private Tilemap tilemap;
    private Vector3Int spawnSpot = new Vector3Int(4, 19);

    private float lastUpdate;
    private Piece activePiece;

    private float lastInput;

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
        if (Time.time > lastInput + 0.2f)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                activePiece.MoveLeft(tilemap);
                lastInput = Time.time;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                activePiece.MoveRight(tilemap);
                lastInput = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            activePiece.Rotate(tilemap);
        }

        if (goingDown && lastUpdate + 0.03f < Time.time)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                lastUpdate = Time.time;
                ok = activePiece.Down(tilemap);

                if (!ok)
                {
                    activePiece = NewPiece();
                    activePiece.SetTiles(tilemap);
                    goingDown = false;
                }
            }
        }
        else if (lastUpdate + 0.2f < Time.time)
        {
            ok = true;
            if (Input.GetKey(KeyCode.DownArrow))
            {
                lastUpdate = Time.time;
                ok = activePiece.Down(tilemap);
                goingDown = true;
            }

            /*ok = activePiece.Down(tilemap);
            lastUpdate = Time.time;

            // Give the player an opportunity to move his piece just before it's sealed
            /*if (Input.GetKey(KeyCode.LeftArrow))
            {
                activePiece.MoveLeft(tilemap);
                ok = activePiece.Down(tilemap);
                lastInput = Time.time;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                activePiece.MoveRight(tilemap);
                ok = activePiece.Down(tilemap);
                lastInput = Time.time;
            }*/

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