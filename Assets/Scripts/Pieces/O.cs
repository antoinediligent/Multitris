using UnityEngine;
using UnityEngine.Tilemaps;

public class O : Piece
{
    public O(int x, int y, Sprite sprite) : base(Tetromino.O, x, y, sprite)
    {
        cells = new Vector3Int[4];
        cells[0] = new Vector3Int(0, 0);
        cells[1] = new Vector3Int(0, -1);
        cells[2] = new Vector3Int(1, 0);
        cells[3] = new Vector3Int(1, -1);
    }

    public override bool MoveLeft(Tilemap tilemap)
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

    public override bool MoveRight(Tilemap tilemap)
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

    public override bool Rotate(Tilemap tilemap)
    {
        // This piece can't rotate
        return true;
    }
}
