using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino
{
    I,
    O,
    T,
    S,
    Z,
    J,
    L,
}

public class Piece
{
    protected Tetromino type;

    protected int rotatePosition;
    public Vector3Int position { get; set; }

    protected Tile tile;

    public Board board;
    public Vector3Int[] cells { get; protected set; }

    public Piece(Tetromino type, Vector3Int position, Sprite sprite)
    {
        this.type = type;
        rotatePosition = 1;
        this.position = position;
        tile = Tile.CreateInstance<Tile>();
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

    public bool Down(Tilemap tilemap)
    {
        SetTiles(tilemap, null);

        Vector3Int nextPosition = position + Vector3Int.down;
        if (board.IsValidPosition(this, nextPosition))
        {
            position = nextPosition;
            SetTiles(tilemap, tile);

            return true;
        }

        SetTiles(tilemap, tile);

        return false;
    }

    public bool MoveLeft(Tilemap tilemap)
    {
        SetTiles(tilemap, null);

        Vector3Int nextPosition = position + Vector3Int.left;
        if (board.IsValidPosition(this, nextPosition))
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
        if (board.IsValidPosition(this, nextPosition))
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
