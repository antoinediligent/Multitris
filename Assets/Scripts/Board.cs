using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public const int BOARD_WIDTH = 10;

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
        int pieceNumber = Random.Range(1, 3);

        Piece piece;

        if (pieceNumber == 1)
        {
            piece = new I(spawnSpot.x, spawnSpot.y, playerOneSprite);
        }
        else
        {
            piece = new O(spawnSpot.x, spawnSpot.y, playerOneSprite);
        }
        
        piece.board = this;

        return piece;
    }

    void Update()
    {
        if (Time.time > lastInput + 0.3f)
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

        if (lastUpdate + 0.2f < Time.time)
        {
            ok = true;
            if (Input.GetKey(KeyCode.DownArrow))
            {
                lastUpdate = Time.time;
                ok = activePiece.Down(tilemap);
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

            // 
            if (!ok)
            {
                activePiece = NewPiece();
                activePiece.SetTiles(tilemap);
            }
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        string piecePositions = "[";
        // The position is only valid if every cell is valid
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            piecePositions += " x=" + tilePosition.x + " y=" + tilePosition.y;

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

        piecePositions += "]";
        Debug.Log(piecePositions);

        return true;
    }
}