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
