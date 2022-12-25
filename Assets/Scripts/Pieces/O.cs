using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class O : Piece
{
    public O(int x, int y, Sprite sprite) : base(Tetromino.O, x, y, sprite)
    {
    }

    public override void SetTiles(Tilemap tilemap, Tile tileToSet)
    {
        tilemap.SetTile(new Vector3Int(x, y), tileToSet);
        tilemap.SetTile(new Vector3Int(x + 1, y), tileToSet);
        tilemap.SetTile(new Vector3Int(x, y - 1), tileToSet);
        tilemap.SetTile(new Vector3Int(x + 1, y - 1), tileToSet);
    }

    public override bool Down(Tilemap tilemap)
    {
        TileBase ot1 = tilemap.GetTile(new Vector3Int(x, y - 2));
        TileBase ot2 = tilemap.GetTile(new Vector3Int(x + 1, y - 2));

        if (ot1 != null || ot2 != null)
        {
            return false;
        }

        SetTiles(tilemap, null);
        y--;
        SetTiles(tilemap, tile);

        return true;
    }

    public override bool IsAtBottom()
    {
        return (y == 1);
    }

    public override bool MoveLeft(Tilemap tilemap)
    {
        if (x == 0)
        {
            return false;
        }

        TileBase ot1 = tilemap.GetTile(new Vector3Int(x - 1, y));
        TileBase ot2 = tilemap.GetTile(new Vector3Int(x - 1, y - 1));

        if (ot1 != null || ot2 != null)
        {
            return false;
        }

        SetTiles(tilemap, null);
        x -= 1;
        SetTiles(tilemap, tile);

        return true;
    }

    public override bool MoveRight(Tilemap tilemap)
    {
        if (x == Board.BOARD_WIDTH - 2)
        {
            return false;
        }

        TileBase ot1 = tilemap.GetTile(new Vector3Int(x + 2, y));
        TileBase ot2 = tilemap.GetTile(new Vector3Int(x + 2, y - 1));

        if (ot1 != null || ot2 != null)
        {
            return false;
        }

        SetTiles(tilemap, null);
        x += 1;
        SetTiles(tilemap, tile);

        return true;
    }

    public override bool Rotate(Tilemap tilemap)
    {
        // This piece can't rotate
        return true;
    }
}
