using UnityEngine;
using UnityEngine.Tilemaps;

public class T : Piece
{
    public T(int x, int y, Sprite sprite) : base(Tetromino.T, x, y, sprite)
    {
    }

    public override void SetTiles(Tilemap tilemap, Tile tileToSet)
    {
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
    }

    /*
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
    public override bool Rotate(Tilemap tilemap)
    {
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

        return true;
    }
}
