using UnityEngine;
using UnityEngine.Tilemaps;

public class Z : Piece
{
    public Z(int x, int y, Sprite sprite) : base(Tetromino.Z, x, y, sprite)
    {
    }

    protected override void SetCells(int rotatePosition)
    {
        cells[0] = new Vector3Int(0, 0);
        if (rotatePosition == 1)
        {
            cells[1] = new Vector3Int(0, -1);
            cells[2] = new Vector3Int(1, -1);
            cells[3] = new Vector3Int(1, -2);
        }
        else if (rotatePosition == 2)
        {
            cells[1] = new Vector3Int(1, 0);
            cells[2] = new Vector3Int(0, -1);
            cells[3] = new Vector3Int(-1, -1);
        }
    }

    public override bool Rotate(Tilemap tilemap)
    {
        if (rotatePosition == 1)
        {
            SetTiles(tilemap, null);
            SetCells(2);

            Vector3Int nextPosition = new Vector3Int(x + 1, y);
            if (board.IsValidPosition(this, nextPosition))
            {
                x += 1;
                rotatePosition = 2;
                SetTiles(tilemap, tile);

                return true;
            }

            nextPosition = new Vector3Int(x, y);
            if (board.IsValidPosition(this, nextPosition))
            {
                rotatePosition = 2;
                SetTiles(tilemap, tile);

                return true;
            }

            // Not ok, move back to previous position
            SetCells(1);
            SetTiles(tilemap, tile);

            return false;
        }
        else if (rotatePosition == 2)
        {
            SetTiles(tilemap, null);
            SetCells(1);

            Vector3Int nextPosition = new Vector3Int(x - 1, y);
            if (board.IsValidPosition(this, nextPosition))
            {
                x -= 1;
                rotatePosition = 1;
                SetTiles(tilemap, tile);

                return true;
            }

            // Not ok, move back to previous position
            SetCells(2);
            SetTiles(tilemap, tile);

            return false;
        }

        return true;
    }
}
