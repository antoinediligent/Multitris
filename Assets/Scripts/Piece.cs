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
    protected int x, y;
    protected Tile tile;

    public Board board;
    public Vector3Int[] cells { get; protected set; }
    // public Vector3Int position { get; private set; }

    public Piece(Tetromino type, int x, int y, Sprite sprite)
    {
        this.type = type;
        this.rotatePosition = 1;
        this.x = x;
        this.y = y;
        tile = Tile.CreateInstance<Tile>();
        tile.sprite = sprite;

        cells = new Vector3Int[4];
        SetCells(1);
    }

    protected virtual void SetCells(int rotatePosition)
    {

    }

    public virtual void SetTiles(Tilemap tilemap, Tile tileToSet)
    {
        Vector3Int position = new Vector3Int(x, y);
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

        Vector3Int nextPosition = new Vector3Int(x, y - 1);
        if (board.IsValidPosition(this, nextPosition))
        {
            y--;
            SetTiles(tilemap, tile);

            return true;
        }

        SetTiles(tilemap, tile);

        return false;
    }

    public bool MoveLeft(Tilemap tilemap)
    {
        SetTiles(tilemap, null);

        Vector3Int nextPosition = new Vector3Int(x - 1, y);
        if (board.IsValidPosition(this, nextPosition))
        {
            x--;
            SetTiles(tilemap, tile);

            return true;
        }

        SetTiles(tilemap, tile);

        return false;
    }

    public bool MoveRight(Tilemap tilemap)
    {
        SetTiles(tilemap, null);

        Vector3Int nextPosition = new Vector3Int(x + 1, y);
        if (board.IsValidPosition(this, nextPosition))
        {
            x++;
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
