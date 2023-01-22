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

            Vector3Int[] nextPositions = new Vector3Int[4];
            nextPositions[0] = position + new Vector3Int(-1, -1);
            nextPositions[1] = position + new Vector3Int(0, -1);
            nextPositions[2] = position + new Vector3Int(-2, -1);
            nextPositions[3] = position + new Vector3Int(-3, -1);

            foreach (Vector3Int nextPosition in nextPositions)
            {
                if (board.IsValidPosition(this, nextPosition))
                {
                    position = nextPosition;
                    rotatePosition = 2;
                    SetTiles(tilemap, tile);

                    return true;
                }
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

            Vector3Int[] nextPositions = new Vector3Int[3];
            nextPositions[0] = position + new Vector3Int(1, 0);
            nextPositions[1] = position + new Vector3Int(1, 1);
            nextPositions[2] = position + new Vector3Int(1, 2);

            foreach (Vector3Int nextPosition in nextPositions)
            {
                if (board.IsValidPosition(this, nextPosition))
                {
                    position = nextPosition;
                    rotatePosition = 1;
                    SetTiles(tilemap, tile);

                    return true;
                }
            }

            // Not the last possibility, because it would give the player a way to make the piece go up

            SetCells(2);
            SetTiles(tilemap, tile);

            return true;
        }

        return false;
    }
}
