using UnityEngine;
using UnityEngine.Tilemaps;

public class I : Piece
{
    public I(int x, int y, Sprite sprite) : base(Tetromino.I, x, y, sprite)
    {
    }

    public override void SetTiles(Tilemap tilemap, Tile tileToSet)
    {
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
    }

    public override bool Down(Tilemap tilemap)
    {
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

        return true;
    }

    public override bool IsAtBottom()
    {
        if (rotatePosition == 1)
        {
            return (y == 3);
        }
        else if (rotatePosition == 2)
        {
            return (y == 0);
        }

        return false;
    }

    public override bool MoveLeft(Tilemap tilemap)
    {
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

        SetTiles(tilemap, null);
        x -= 1;
        SetTiles(tilemap, tile);

        return true;
    }

    public override bool MoveRight(Tilemap tilemap)
    {
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

        return true;
    }

    /**
     * I piece rotatePosition :
     * 1) x
     *    x
     *    x
     *    x
     *   
     * 2) xxxx
     */
    public override bool Rotate(Tilemap tilemap)
    {
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

        return true;
    }
}
