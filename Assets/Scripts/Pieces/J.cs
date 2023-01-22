using UnityEngine;
using UnityEngine.Tilemaps;

public class J : Piece
{
    public J(int x, int y, Sprite sprite) : base(Tetromino.O, x, y, sprite)
    {
    }

    protected override void SetCells(int rotatePosition)
    {
        cells[0] = new Vector3Int(0, 0);
        if (rotatePosition == 1)
        {
            cells[1] = new Vector3Int(0, -1);
            cells[2] = new Vector3Int(0, -2);
            cells[3] = new Vector3Int(-1, -2);
        }
        else if (rotatePosition == 2)
        {
            cells[1] = new Vector3Int(0, -1);
            cells[2] = new Vector3Int(1, -1);
            cells[3] = new Vector3Int(2, -1);
        }
        else if (rotatePosition == 3)
        {
            cells[1] = new Vector3Int(1, 0);
            cells[2] = new Vector3Int(0, -1);
            cells[3] = new Vector3Int(0, -2);
        }
        else if (rotatePosition == 4)
        {
            cells[1] = new Vector3Int(1, 0);
            cells[2] = new Vector3Int(2, 0);
            cells[3] = new Vector3Int(2, -1);
        }
    }

    public override bool Rotate(Tilemap tilemap)
    {
        if (rotatePosition == 1)
        {
            SetTiles(tilemap, null);
            SetCells(2);

            Vector3Int nextPosition = new Vector3Int(x - 1, y);
            if (board.IsValidPosition(this, nextPosition))
            {
                x -= 1;
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

            nextPosition = new Vector3Int(x - 2, y - 1);
            if (board.IsValidPosition(this, nextPosition))
            {
                x -= 2;
                y -= 1;
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
            SetCells(3);

            Vector3Int nextPosition = new Vector3Int(x, y);
            if (board.IsValidPosition(this, nextPosition))
            {
                rotatePosition = 3;
                SetTiles(tilemap, tile);

                return true;
            }

            // Not ok, move back to previous position
            SetCells(2);
            SetTiles(tilemap, tile);

            return false;
        }
        else if (rotatePosition == 3)
        {
            SetTiles(tilemap, null);
            SetCells(4);

            Vector3Int nextPosition = new Vector3Int(x, y);
            if (board.IsValidPosition(this, nextPosition))
            {
                rotatePosition = 4;
                SetTiles(tilemap, tile);

                return true;
            }

            nextPosition = new Vector3Int(x - 1, y - 1);
            if (board.IsValidPosition(this, nextPosition))
            {
                x -= 1;
                y -= 1;
                rotatePosition = 2;
                SetTiles(tilemap, tile);

                return true;
            }

            // Not ok, move back to previous position
            SetCells(3);
            SetTiles(tilemap, tile);

            return false;
        }
        else if (rotatePosition == 4)
        {
            SetTiles(tilemap, null);
            SetCells(1);

            Vector3Int nextPosition = new Vector3Int(x + 1, y);
            if (board.IsValidPosition(this, nextPosition))
            {
                x += 1;
                rotatePosition = 1;
                SetTiles(tilemap, tile);

                return true;
            }

            nextPosition = new Vector3Int(x, y);
            if (board.IsValidPosition(this, nextPosition))
            {
                rotatePosition = 1;
                SetTiles(tilemap, tile);

                return true;
            }

            // Not ok, move back to previous position
            SetCells(4);
            SetTiles(tilemap, tile);

            return false;
        }

        return false;
    }
}
