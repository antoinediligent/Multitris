using UnityEngine;
using UnityEngine.Tilemaps;
public class Player
{
    private int number;

    private Sprite sprite;
    public Vector3Int spawnSpot;
    public Piece piece;

    public bool GoingDown;

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

    public Piece NewPiece(Board board, Tilemap tilemap)
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

        return piece;
    }

    public int getPlayerNumber()
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