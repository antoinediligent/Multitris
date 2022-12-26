using UnityEngine;
using UnityEngine.Tilemaps;

public class S : Piece
{
    public S(int x, int y, Sprite sprite) : base(Tetromino.S, x, y, sprite)
    {
    }

    public override void SetTiles(Tilemap tilemap, Tile tileToSet)
    {
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
    }

    public override bool Down(Tilemap tilemap)
    {
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

        return true;
    }

    public override bool IsAtBottom()
    {
        if (rotatePosition == 1)
        {
            return (y == 2);
        }
        else if (rotatePosition == 2)
        {
            return (y == 1);
        }

        return false;
    }

    public override bool MoveLeft(Tilemap tilemap)
    {
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

        return true;
    }

    public override bool MoveRight(Tilemap tilemap)
    {
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

        return true;
    }

    public override bool Rotate(Tilemap tilemap)
    {
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

        return true;
    }
}
