using System.Collections;
using System.Collections.Generic;
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

    public Piece(Tetromino type, int x, int y, Sprite sprite)
    {
        this.type = type;
        this.rotatePosition = 1;
        this.x = x;
        this.y = y;
        tile = Tile.CreateInstance<Tile>();
        tile.sprite = sprite;
    }

    public virtual void SetTiles(Tilemap tilemap, Tile tileToSet)
    {
        Debug.Log("SetTiles is not supposed to be called");
    }

    public void SetTiles(Tilemap tilemap)
    {
        SetTiles(tilemap, tile);
    }

    public virtual bool Down(Tilemap tilemap)
    {
        Debug.Log("Down is not supposed to be called");
        return true;
    }

    public virtual bool IsAtBottom()
    {
        Debug.Log("IsAtBottom is not supposed to be called");
        return false;
    }

    public virtual bool MoveLeft(Tilemap tilemap)
    {
        Debug.Log("MoveLeft is not supposed to be called");
        return true;
    }

    public virtual bool MoveRight(Tilemap tilemap)
    {
        Debug.Log("MoveRight is not supposed to be called");
        return true;
    }

    public virtual bool Rotate(Tilemap tilemap)
    {
        Debug.Log("Rotate is not supposed to be called");
        return true;
    }
}
