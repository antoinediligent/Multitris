using UnityEngine;
using UnityEngine.Tilemaps;

public class Piece
{

    protected int rotatePosition;
    public Vector3Int position { get; set; }

    protected GameTile tile;

    public Board board;
    public Vector3Int[] cells { get; protected set; }

    public const int NO_COLLISION = 0;
    public const int BOARD_COLLISION = 1;
    public const int OTHER_PLAYER_COLLISION = 2;

    public Piece(Vector3Int position, Sprite sprite)
    {
        rotatePosition = 1;
        this.position = position;
        tile = GameTile.CreateInstance<GameTile>();
        tile.sprite = sprite;

        cells = new Vector3Int[4];
        SetCells(1);
    }

    protected virtual void SetCells(int rotatePosition)
    {

    }

    public void SetTiles(Tilemap tilemap, Tile tileToSet)
    {
        tilemap.SetTile(position + cells[0], tileToSet);
        tilemap.SetTile(position + cells[1], tileToSet);
        tilemap.SetTile(position + cells[2], tileToSet);
        tilemap.SetTile(position + cells[3], tileToSet);
    }

    public void SetTiles(Tilemap tilemap)
    {
        SetTiles(tilemap, tile);
    }

    public int Down(Tilemap tilemap)
    {
        SetTiles(tilemap, null);

        Vector3Int nextPosition = position + Vector3Int.down;
        int isValidState = board.IsValidPosition(this, nextPosition);
        if (isValidState == NO_COLLISION)
        {
            position = nextPosition;
            SetTiles(tilemap, tile);

            return NO_COLLISION;
        }

        if (isValidState == BOARD_COLLISION)
        {
            tile.gameTag = GameTile.BOARD_PIECE;
        }

        SetTiles(tilemap, tile);

        return isValidState;
    }

    public bool MoveLeft(Tilemap tilemap)
    {
        SetTiles(tilemap, null);

        Vector3Int nextPosition = position + Vector3Int.left;
        if (board.IsValidPosition(this, nextPosition) == NO_COLLISION)
        {
            position = nextPosition;
            SetTiles(tilemap, tile);

            return true;
        }

        SetTiles(tilemap, tile);

        return false;
    }

    public bool MoveRight(Tilemap tilemap)
    {
        SetTiles(tilemap, null);

        Vector3Int nextPosition = position + Vector3Int.right;
        if (board.IsValidPosition(this, nextPosition) == NO_COLLISION)
        {
            position = nextPosition;
            SetTiles(tilemap, tile);

            return true;
        }

        SetTiles(tilemap, tile);

        return false;
    }

    public virtual bool Rotate(Tilemap tilemap)
    {
        Debug.Log("Rotate is not supposed to be called");
        return true;
    }
}
