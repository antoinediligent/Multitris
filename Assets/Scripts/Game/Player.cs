using UnityEngine;
using UnityEngine.Tilemaps;
public class Player
{
    private int number;

    private Sprite sprite;
    public Vector3Int spawnSpot;
    public Piece piece;

    // Move variables
    public float lastDirInputTime;
    public int movingDirection;
    public bool superMove;

    public const int NOT_MOVING = 0;
    public const int MOVING_LEFT = 1;
    public const int MOVING_RIGHT = 2;
    public const int MOVING_DOWN = 3;

    public Player(int number, Sprite sprite)
    {
        this.number = number;
        this.sprite = sprite;

        switch (number)
        {
            case 1:
                spawnSpot = new Vector3Int(4, 19);
                break;
            case 2:
                spawnSpot = new Vector3Int(14, 19);
                break;
            case 3:
                spawnSpot = new Vector3Int(24, 19);
                break;
            case 4:
                spawnSpot = new Vector3Int(34, 19);
                break;
        }
    }

    public void NewPiece(Board board, Tilemap tilemap)
    {
        int pieceNumber = Random.Range(1, 8);

        switch (pieceNumber)
        {
            case 1:
                piece = new I(spawnSpot, sprite);
                break;

            case 2:
                piece = new J(spawnSpot, sprite);
                break;

            case 3:
                piece = new L(spawnSpot, sprite);
                break;

            case 4:
                piece = new O(spawnSpot, sprite);
                break;

            case 5:
                piece = new S(spawnSpot, sprite);
                break;

            case 6:
                piece = new T(spawnSpot, sprite);
                break;

            default:
                piece = new Z(spawnSpot, sprite);
                break;
        }

        piece.board = board;
        piece.SetTiles(tilemap);
    }

    public int GetPlayerNumber()
    {
        return number;
    }

    public void HidePiece(Tilemap tilemap)
    {
        piece.SetTiles(tilemap, null);
    }

    public void ShowPiece(Tilemap tilemap)
    {
        piece.SetTiles(tilemap);
    }
}