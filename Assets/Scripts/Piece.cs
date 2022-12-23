using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino
{
    I,
    O,
    T,
    J,
    L,
    S,
    Z,
}

public class Piece
{

    private Tetromino type;
    private int rotatePosition;
    private int x, y;
    private Tile tile;

    public Piece(Tetromino type, int x, int y, Sprite sprite)
    {
        this.type = type;
        this.rotatePosition = 1;
        this.x = x;
        this.y = y;
        tile = new Tile();
        tile.sprite = sprite;
    }

    public void SetTiles(Tilemap tilemap, Tile tileToSet)
    {
        switch (type) {
            case Tetromino.I:
                if (rotatePosition == 1)
                {
                    tilemap.SetTile(new Vector3Int(x, y), tileToSet);
                    tilemap.SetTile(new Vector3Int(x, y - 1), tileToSet);
                    tilemap.SetTile(new Vector3Int(x, y - 2), tileToSet);
                    tilemap.SetTile(new Vector3Int(x, y - 3), tileToSet);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, y), tileToSet);
                    tilemap.SetTile(new Vector3Int(x + 1, y), tileToSet);
                    tilemap.SetTile(new Vector3Int(x + 2, y), tileToSet);
                    tilemap.SetTile(new Vector3Int(x + 3, y), tileToSet);
                }
                break;
        }
    }

    public void SetTiles(Tilemap tilemap)
    {
        SetTiles(tilemap, tile);
    }

    public bool Down(Tilemap tilemap)
    {
        switch (type)
        {
            case Tetromino.I:
                if (rotatePosition == 1)
                {
                    TileBase t = tilemap.GetTile(new Vector3Int(x, y - 4));

                    if (t != null)
                    {
                        // Collision
                        return false;
                    }

                    SetTiles(tilemap, null);
                    y--;
                    SetTiles(tilemap, tile);
                }
                else if (rotatePosition == 2)
                {
                    SetTiles(tilemap, null);
                    y--;
                    SetTiles(tilemap, tile);
                }
                break;
        }

        return IsAtBottom();
    }

    public bool IsAtBottom()
    {
        switch (type)
        {
            case Tetromino.I:
                if (rotatePosition == 1)
                {
                    return (y == 3);
                }
                else if (rotatePosition == 2)
                {
                    return (y == 0);
                }
                break;
        }

        return false;
    }

    public bool MoveLeft(Tilemap tilemap)
    {
        switch (type)
        {
            case Tetromino.I:
                if (rotatePosition == 1)
                {
                    if (x == 0)
                    {
                        return false;
                    }

                    for (int i = y; i <= y+3; i++)
                    {
                        TileBase t = tilemap.GetTile(new Vector3Int(x, i));
                        if (t != null)
                        {
                            return false;
                        }
                    }

                    SetTiles(tilemap, null);
                    x -= 1;
                    SetTiles(tilemap, tile);
                }
                else if (rotatePosition == 2)
                {
                    if (x == 0)
                    {
                        return false;
                    }
                }
                break;
        }
        return true;
    }

    public void Rotate(Tilemap tilemap)
    {
        switch (type)
        {
            case Tetromino.I:
                if (rotatePosition == 1)
                {
                    SetTiles(tilemap, null);
                    x -= 1;
                    rotatePosition = 2;
                    SetTiles(tilemap, tile);
                }
                else if (rotatePosition == 2)
                {
                    SetTiles(tilemap, null);
                    x += 1;
                    rotatePosition = 1;
                    SetTiles(tilemap, tile);
                }
                break;
        }
    }
}
