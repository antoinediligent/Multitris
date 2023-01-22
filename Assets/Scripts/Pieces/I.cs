using UnityEngine;
using UnityEngine.Tilemaps;

public class I : Piece
{
    public I(int x, int y, Sprite sprite) : base(Tetromino.I, x, y, sprite)
    {

    }

    protected override void SetCells(int rotatePosition)
    {
        if (rotatePosition == 1)
        {
            cells[0] = new Vector3Int(0, 0);
            cells[1] = new Vector3Int(0, -1);
            cells[2] = new Vector3Int(0, -2);
            cells[3] = new Vector3Int(0, -3);
        }
        else
        {
            cells[0] = new Vector3Int(0, 0);
            cells[1] = new Vector3Int(1, 0);
            cells[2] = new Vector3Int(2, 0);
            cells[3] = new Vector3Int(3, 0);
        }
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
            SetTiles(tilemap, null);
            SetCells(2);

            // Try one cell on the left to harmonize with the other rotation
            Vector3Int nextPosition = new Vector3Int(x - 1, y - 1);
            if (board.IsValidPosition(this, nextPosition))
            {
                x -= 1;
                y -= 1;
                rotatePosition = 2;
                SetTiles(tilemap, tile);

                return true;
            }

            // If the piece is on the left border
            nextPosition = new Vector3Int(x, y - 1);
            if (board.IsValidPosition(this, nextPosition))
            {
                y -= 1;
                rotatePosition = 2;
                SetTiles(tilemap, tile);

                return true;
            }

            // Try one more cell on the left
            nextPosition = new Vector3Int(x - 2, y - 1);
            if (board.IsValidPosition(this, nextPosition))
            {
                x -= 2;
                y -= 1;
                rotatePosition = 2;
                SetTiles(tilemap, tile);

                return true;
            }

            // Try one more cell on the left
            nextPosition = new Vector3Int(x - 3, y - 1);
            if (board.IsValidPosition(this, nextPosition))
            {
                x -= 3;
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
            SetCells(1);

            Vector3Int nextPosition = new Vector3Int(x + 1, y);
            if (board.IsValidPosition(this, nextPosition))
            {
                x += 1;
                rotatePosition = 1;
                SetTiles(tilemap, tile);

                return true;
            }

            nextPosition = new Vector3Int(x + 1, y + 1);
            if (board.IsValidPosition(this, nextPosition))
            {
                x += 1;
                y += 1;
                rotatePosition = 1;
                SetTiles(tilemap, tile);

                return true;
            }

            nextPosition = new Vector3Int(x + 1, y + 2);
            if (board.IsValidPosition(this, nextPosition))
            {
                x += 1;
                y += 2;
                rotatePosition = 1;
                SetTiles(tilemap, tile);

                return true;
            }

            // Not the last possibility, because it would give the player a way to make the piece go up

            SetCells(2);
            SetTiles(tilemap, tile);

            return true;
        }

        return false;
    }
}
