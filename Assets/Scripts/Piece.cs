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

/**
 * I piece rotatePosition :
 * 1) x
 *    x
 *    x
 *    x
 *   
 * 2) xxxx
 * 
 * 
 * T piece rotatePosition :
 * 1)  x
 *    xxx
 *    
 * 2) x
 *    xx
 *    x
 * 
 * 3) xxx
 *     x
 *     
 * 4)  x
 *    xx
 *     x
 */
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
        switch (type)
        {
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

            case Tetromino.T:

                tilemap.SetTile(new Vector3Int(x, y), tileToSet);
                if (rotatePosition == 1)
                {
                    tilemap.SetTile(new Vector3Int(x - 1, y - 1), tileToSet);
                    tilemap.SetTile(new Vector3Int(x, y - 1), tileToSet);
                    tilemap.SetTile(new Vector3Int(x + 1, y - 1), tileToSet);
                }
                else if (rotatePosition == 2)
                {
                    tilemap.SetTile(new Vector3Int(x, y - 1), tileToSet);
                    tilemap.SetTile(new Vector3Int(x, y - 2), tileToSet);
                    tilemap.SetTile(new Vector3Int(x + 1, y - 1), tileToSet);
                }
                else if (rotatePosition == 3)
                {
                    tilemap.SetTile(new Vector3Int(x + 1, y), tileToSet);
                    tilemap.SetTile(new Vector3Int(x + 2, y), tileToSet);
                    tilemap.SetTile(new Vector3Int(x + 1, y - 1), tileToSet);
                }
                else if (rotatePosition == 4)
                {
                    tilemap.SetTile(new Vector3Int(x, y - 1), tileToSet);
                    tilemap.SetTile(new Vector3Int(x - 1, y - 1), tileToSet);
                    tilemap.SetTile(new Vector3Int(x, y - 2), tileToSet);
                }
                break;

            case Tetromino.S:

                tilemap.SetTile(new Vector3Int(x, y), tileToSet);
                if (rotatePosition == 1)
                {
                    tilemap.SetTile(new Vector3Int(x, y - 1), tileToSet);
                    tilemap.SetTile(new Vector3Int(x - 1, y - 1), tileToSet);
                    tilemap.SetTile(new Vector3Int(x - 1, y - 2), tileToSet);
                }
                else if (rotatePosition == 2)
                {
                    tilemap.SetTile(new Vector3Int(x + 1, y), tileToSet);
                    tilemap.SetTile(new Vector3Int(x + 1, y - 1), tileToSet);
                    tilemap.SetTile(new Vector3Int(x + 2, y - 1), tileToSet);
                }
                break;

            case Tetromino.Z:

                tilemap.SetTile(new Vector3Int(x, y), tileToSet);
                if (rotatePosition == 1)
                {
                    tilemap.SetTile(new Vector3Int(x, y - 1), tileToSet);
                    tilemap.SetTile(new Vector3Int(x + 1, y - 1), tileToSet);
                    tilemap.SetTile(new Vector3Int(x + 1, y - 2), tileToSet);
                }
                else if (rotatePosition == 2)
                {
                    tilemap.SetTile(new Vector3Int(x + 1, y), tileToSet);
                    tilemap.SetTile(new Vector3Int(x, y - 1), tileToSet);
                    tilemap.SetTile(new Vector3Int(x - 1, y - 1), tileToSet);
                }
                break;

            case Tetromino.L:

                break;
        }
    }

    public void SetTiles(Tilemap tilemap)
    {
        SetTiles(tilemap, tile);
    }

    public virtual bool Down(Tilemap tilemap)
    {
        switch (type)
        {
            case Tetromino.I:
                if (rotatePosition == 1)
                {
                    TileBase it = tilemap.GetTile(new Vector3Int(x, y - 4));

                    if (it != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 2)
                {
                    TileBase it1 = tilemap.GetTile(new Vector3Int(x, y - 1));
                    TileBase it2 = tilemap.GetTile(new Vector3Int(x + 1, y - 1));
                    TileBase it3 = tilemap.GetTile(new Vector3Int(x + 2, y - 1));
                    TileBase it4 = tilemap.GetTile(new Vector3Int(x + 3, y - 1));

                    if (it1 != null || it2 != null || it3 != null || it4 != null)
                    {
                        return false;
                    }
                }

                SetTiles(tilemap, null);
                y--;
                SetTiles(tilemap, tile);
                break;

            case Tetromino.T:
                if (rotatePosition == 1)
                {
                    TileBase tt1 = tilemap.GetTile(new Vector3Int(x - 1, y - 2));
                    TileBase tt2 = tilemap.GetTile(new Vector3Int(x, y - 2));
                    TileBase tt3 = tilemap.GetTile(new Vector3Int(x + 1, y - 2));

                    if (tt1 != null || tt2 != null || tt3 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 2)
                {
                    TileBase tt1 = tilemap.GetTile(new Vector3Int(x, y - 3));
                    TileBase tt2 = tilemap.GetTile(new Vector3Int(x + 1, y - 2));

                    if (tt1 != null || tt2 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 3)
                {
                    TileBase tt1 = tilemap.GetTile(new Vector3Int(x, y - 1));
                    TileBase tt2 = tilemap.GetTile(new Vector3Int(x + 1, y - 2));
                    TileBase tt3 = tilemap.GetTile(new Vector3Int(x + 2, y - 1));

                    if (tt1 != null || tt2 != null || tt3 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 4)
                {
                    TileBase tt1 = tilemap.GetTile(new Vector3Int(x, y - 3));
                    TileBase tt2 = tilemap.GetTile(new Vector3Int(x - 1, y - 2));

                    if (tt1 != null || tt2 != null)
                    {
                        return false;
                    }
                }

                SetTiles(tilemap, null);
                y--;
                SetTiles(tilemap, tile);
                break;

            case Tetromino.S:
                if (rotatePosition == 1)
                {
                    TileBase st1 = tilemap.GetTile(new Vector3Int(x - 1, y - 3));
                    TileBase st2 = tilemap.GetTile(new Vector3Int(x, y - 2));

                    if (st1 != null || st2 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 2)
                {
                    TileBase st1 = tilemap.GetTile(new Vector3Int(x, y - 1));
                    TileBase st2 = tilemap.GetTile(new Vector3Int(x + 1, y - 2));
                    TileBase st3 = tilemap.GetTile(new Vector3Int(x + 2, y - 2));

                    if (st1 != null || st2 != null || st3 != null)
                    {
                        return false;
                    }
                }

                SetTiles(tilemap, null);
                y--;
                SetTiles(tilemap, tile);
                break;

            case Tetromino.Z:
                if (rotatePosition == 1)
                {
                    TileBase zt1 = tilemap.GetTile(new Vector3Int(x, y - 2));
                    TileBase zt2 = tilemap.GetTile(new Vector3Int(x + 1, y - 3));

                    if (zt1 != null || zt2 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 2)
                {
                    TileBase zt1 = tilemap.GetTile(new Vector3Int(x - 1, y - 2));
                    TileBase zt2 = tilemap.GetTile(new Vector3Int(x, y - 2));
                    TileBase zt3 = tilemap.GetTile(new Vector3Int(x + 1, y - 1));

                    if (zt1 != null || zt2 != null || zt3 != null)
                    {
                        return false;
                    }
                }

                SetTiles(tilemap, null);
                y--;
                SetTiles(tilemap, tile);
                break;
        }

        return true;
    }

    public virtual bool IsAtBottom()
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

            case Tetromino.T:
                if (rotatePosition == 1 || rotatePosition == 3)
                {
                    return (y == 1);
                }
                else
                {
                    return (y == 2);
                }
            case Tetromino.S:
                if (rotatePosition == 1)
                {
                    return (y == 2);
                }
                else if (rotatePosition == 2)
                {
                    return (y == 1);
                }
                break;
            case Tetromino.Z:
                if (rotatePosition == 1)
                {
                    return (y == 2);
                }
                else if (rotatePosition == 2)
                {
                    return (y == 1);
                }
                break;
        }

        return false;
    }

    public virtual bool MoveLeft(Tilemap tilemap)
    {
        switch (type)
        {
            case Tetromino.I:

                if (x == 0)
                {
                    return false;
                }

                if (rotatePosition == 1)
                {
                    for (int i = y; i >= y - 3; i--)
                    {
                        TileBase t = tilemap.GetTile(new Vector3Int(x - 1, i));
                        if (t != null)
                        {
                            return false;
                        }
                    }
                }
                else if (rotatePosition == 2)
                {
                    TileBase t = tilemap.GetTile(new Vector3Int(x - 1, y));
                    if (t != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 3)
                {
                    TileBase t = tilemap.GetTile(new Vector3Int(x - 1, y));
                    if (t != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 4)
                {
                    TileBase t = tilemap.GetTile(new Vector3Int(x - 1, y));
                    if (t != null)
                    {
                        return false;
                    }
                }

                SetTiles(tilemap, null);
                x -= 1;
                SetTiles(tilemap, tile);

                break;

            case Tetromino.T:

                if (rotatePosition == 1)
                {
                    if (x == 1)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x - 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x - 2, y - 1));

                    if (t1 != null || t2 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 2)
                {
                    if (x == 0)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x - 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x - 1, y - 1));
                    TileBase t3 = tilemap.GetTile(new Vector3Int(x - 1, y - 2));

                    if (t1 != null || t2 != null || t3 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 3)
                {
                    if (x == 0)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x - 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x, y - 1));

                    if (t1 != null || t2 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 4)
                {
                    if (x == 1)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x - 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x - 2, y - 1));
                    TileBase t3 = tilemap.GetTile(new Vector3Int(x - 1, y - 2));

                    if (t1 != null || t2 != null || t3 != null)
                    {
                        return false;
                    }
                }

                SetTiles(tilemap, null);
                x -= 1;
                SetTiles(tilemap, tile);

                break;

            case Tetromino.S:

                if (rotatePosition == 1)
                {
                    if (x == 1)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x - 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x - 2, y - 1));
                    TileBase t3 = tilemap.GetTile(new Vector3Int(x - 2, y - 2));

                    if (t1 != null || t2 != null || t3 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 2)
                {
                    if (x == 0)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x - 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x, y - 1));

                    if (t1 != null || t2 != null)
                    {
                        return false;
                    }
                }

                SetTiles(tilemap, null);
                x -= 1;
                SetTiles(tilemap, tile);

                break;

            case Tetromino.Z:

                if (rotatePosition == 1)
                {
                    if (x == 0)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x - 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x - 1, y - 1));
                    TileBase t3 = tilemap.GetTile(new Vector3Int(x, y - 2));

                    if (t1 != null || t2 != null || t3 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 2)
                {
                    if (x == 1)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x - 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x - 2, y - 1));

                    if (t1 != null || t2 != null)
                    {
                        return false;
                    }
                }

                SetTiles(tilemap, null);
                x -= 1;
                SetTiles(tilemap, tile);

                break;
        }
        return true;
    }

    public virtual bool MoveRight(Tilemap tilemap)
    {
        switch (type)
        {
            case Tetromino.I:
                if (rotatePosition == 1)
                {
                    if (x == 9)
                    {
                        return false;
                    }

                    for (int i = y; i >= y - 3; i--)
                    {
                        TileBase t = tilemap.GetTile(new Vector3Int(x + 1, i));
                        if (t != null)
                        {
                            return false;
                        }
                    }
                }
                else if (rotatePosition == 2)
                {
                    if (x == 6)
                    {
                        return false;
                    }

                    TileBase t = tilemap.GetTile(new Vector3Int(x + 4, y));
                    if (t != null)
                    {
                        return false;
                    }
                }

                SetTiles(tilemap, null);
                x += 1;
                SetTiles(tilemap, tile);

                break;

            case Tetromino.T:

                if (rotatePosition == 1)
                {
                    if (x == Board.BOARD_WIDTH - 2)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x + 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x + 2, y - 1));

                    if (t1 != null || t2 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 2)
                {
                    if (x == Board.BOARD_WIDTH - 2)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x + 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x + 2, y - 1));
                    TileBase t3 = tilemap.GetTile(new Vector3Int(x + 1, y - 2));

                    if (t1 != null || t2 != null || t3 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 3)
                {
                    if (x == Board.BOARD_WIDTH - 3)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x + 3, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x + 2, y - 1));

                    if (t1 != null || t2 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 4)
                {
                    if (x == Board.BOARD_WIDTH - 1)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x + 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x + 1, y - 1));
                    TileBase t3 = tilemap.GetTile(new Vector3Int(x + 1, y - 2));

                    if (t1 != null || t2 != null || t3 != null)
                    {
                        return false;
                    }
                }

                SetTiles(tilemap, null);
                x += 1;
                SetTiles(tilemap, tile);

                break;

            case Tetromino.S:

                if (rotatePosition == 1)
                {
                    if (x == Board.BOARD_WIDTH - 1)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x + 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x + 1, y - 1));
                    TileBase t3 = tilemap.GetTile(new Vector3Int(x, y - 2));

                    if (t1 != null || t2 != null || t3 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 2)
                {
                    if (x == Board.BOARD_WIDTH - 3)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x + 2, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x + 3, y - 1));

                    if (t1 != null || t2 != null)
                    {
                        return false;
                    }
                }

                SetTiles(tilemap, null);
                x += 1;
                SetTiles(tilemap, tile);

                break;

            case Tetromino.Z:

                if (rotatePosition == 1)
                {
                    if (x == Board.BOARD_WIDTH - 2)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x + 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x + 2, y - 1));
                    TileBase t3 = tilemap.GetTile(new Vector3Int(x + 2, y - 2));

                    if (t1 != null || t2 != null || t3 != null)
                    {
                        return false;
                    }
                }
                else if (rotatePosition == 2)
                {
                    if (x == Board.BOARD_WIDTH - 2)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x + 2, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x + 1, y - 1));

                    if (t1 != null || t2 != null)
                    {
                        return false;
                    }
                }

                SetTiles(tilemap, null);
                x += 1;
                SetTiles(tilemap, tile);

                break;
        }
        return true;
    }

    public virtual bool Rotate(Tilemap tilemap)
    {
        switch (type)
        {
            case Tetromino.I:
                if (rotatePosition == 1)
                {
                    if (x == Board.BOARD_WIDTH - 1)
                    {
                        TileBase t1 = tilemap.GetTile(new Vector3Int(x - 3, y - 1));
                        TileBase t2 = tilemap.GetTile(new Vector3Int(x - 2, y - 1));
                        TileBase t3 = tilemap.GetTile(new Vector3Int(x - 1, y - 1));

                        if (t1 != null || t2 != null || t3 != null)
                        {
                            return false;
                        }

                        SetTiles(tilemap, null);
                        x -= 3;
                        y -= 1;
                        rotatePosition = 2;
                        SetTiles(tilemap, tile);
                    }
                    else if (x == Board.BOARD_WIDTH - 2)
                    {
                        TileBase t1 = tilemap.GetTile(new Vector3Int(x - 2, y - 1));
                        TileBase t2 = tilemap.GetTile(new Vector3Int(x - 1, y - 1));
                        // don't do x, y-1 : the piece is already occupying it
                        TileBase t3 = tilemap.GetTile(new Vector3Int(x + 1, y - 1));

                        if (t1 != null || t2 != null || t3 != null)
                        {
                            return false;
                        }

                        SetTiles(tilemap, null);
                        x -= 2;
                        y -= 1;
                        rotatePosition = 2;
                        SetTiles(tilemap, tile);
                    }
                    else if (x == 0)
                    {
                        // don't do x, y-1 : the piece is already occupying it
                        TileBase t1 = tilemap.GetTile(new Vector3Int(x + 1, y - 1));
                        TileBase t2 = tilemap.GetTile(new Vector3Int(x + 2, y - 1));
                        TileBase t3 = tilemap.GetTile(new Vector3Int(x + 3, y - 1));

                        if (t1 != null || t2 != null || t3 != null)
                        {
                            return false;
                        }

                        SetTiles(tilemap, null);
                        y -= 1;
                        rotatePosition = 2;
                        SetTiles(tilemap, tile);
                    }
                    else
                    {
                        TileBase t1 = tilemap.GetTile(new Vector3Int(x - 1, y - 1));
                        // don't do x, y-1 : the piece is already occupying it
                        TileBase t2 = tilemap.GetTile(new Vector3Int(x + 1, y - 1));
                        TileBase t3 = tilemap.GetTile(new Vector3Int(x + 2, y - 1));

                        if (t1 != null || t2 != null || t3 != null)
                        {
                            return false;
                        }

                        SetTiles(tilemap, null);
                        x -= 1;
                        y -= 1;
                        rotatePosition = 2;
                        SetTiles(tilemap, tile);
                    }
                }
                else if (rotatePosition == 2)
                {
                    if (y <= 3)
                    {
                        // Cannot rotate or it will go outside the board
                        return false;
                    }

                    // don't do x+1, y : the piece is already occupying it
                    TileBase t1 = tilemap.GetTile(new Vector3Int(x + 1, y - 1));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x + 1, y - 2));
                    TileBase t3 = tilemap.GetTile(new Vector3Int(x + 1, y - 3));

                    if (t1 != null || t2 != null || t3 != null)
                    {
                        return false;
                    }

                    SetTiles(tilemap, null);
                    x += 1;
                    rotatePosition = 1;
                    SetTiles(tilemap, tile);
                }
                break;

            case Tetromino.T:
                if (rotatePosition == 1)
                {
                    if (y == 2)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x, y + 2));

                    if (t1 != null)
                    {
                        return false;
                    }

                    SetTiles(tilemap, null);
                    rotatePosition = 2;
                    SetTiles(tilemap, tile);
                }
                else if (rotatePosition == 2)
                {
                    if (x == 0)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x - 1, y - 1));

                    if (t1 != null)
                    {
                        return false;
                    }

                    SetTiles(tilemap, null);
                    x -= 1;
                    y -= 1;
                    rotatePosition = 3;
                    SetTiles(tilemap, tile);
                }
                else if (rotatePosition == 3)
                {
                    if (y == 1)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x + 1, y + 1));

                    if (t1 != null)
                    {
                        return false;
                    }

                    SetTiles(tilemap, null);
                    x += 1;
                    y += 1;
                    rotatePosition = 4;
                    SetTiles(tilemap, tile);
                }
                else if (rotatePosition == 4)
                {
                    if (x == Board.BOARD_WIDTH - 1)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x + 1, y - 1));

                    if (t1 != null)
                    {
                        return false;
                    }

                    SetTiles(tilemap, null);
                    rotatePosition = 1;
                    SetTiles(tilemap, tile);
                }
                break;

            case Tetromino.S:
                if (rotatePosition == 1)
                {
                    if (x == Board.BOARD_WIDTH - 1)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x - 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x + 1, y - 1));

                    if (t1 != null || t2 != null)
                    {
                        return false;
                    }

                    SetTiles(tilemap, null);
                    x -= 1;
                    rotatePosition = 2;
                    SetTiles(tilemap, tile);
                }
                else if (rotatePosition == 2)
                {
                    TileBase t1 = tilemap.GetTile(new Vector3Int(x, y - 1));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x, y - 2));

                    if (t1 != null || t2 != null)
                    {
                        return false;
                    }

                    SetTiles(tilemap, null);
                    x += 1;
                    rotatePosition = 1;
                    SetTiles(tilemap, tile);
                }
                break;

            case Tetromino.Z:
                if (rotatePosition == 1)
                {
                    if (x == 0 || x == Board.BOARD_WIDTH - 2)
                    {
                        return false;
                    }

                    TileBase t1 = tilemap.GetTile(new Vector3Int(x + 1, y));
                    TileBase t2 = tilemap.GetTile(new Vector3Int(x - 1, y - 1));

                    if (t1 != null || t2 != null)
                    {
                        return false;
                    }

                    SetTiles(tilemap, null);
                    x += 1;
                    rotatePosition = 2;
                    SetTiles(tilemap, tile);
                }
                else if (rotatePosition == 2)
                {
                    if (y == 2)
                    {
                        return false;
                    }

                    SetTiles(tilemap, null);
                    x -= 1;
                    rotatePosition = 1;
                    SetTiles(tilemap, tile);
                }
                break;
        }

        return true;
    }
}
