using UnityEngine;
using UnityEngine.Tilemaps;

public class T : Piece
{
    public T(Vector3Int position, Sprite sprite) : base(position, sprite)
    {
    }

    protected override void SetCells(int rotatePosition)
    {
        cells[0] = new Vector3Int(0, 0);
        if (rotatePosition == 1)
        {
            cells[1] = new Vector3Int(-1, -1);
            cells[2] = new Vector3Int(0, -1);
            cells[3] = new Vector3Int(1, -1);
        }
        else if (rotatePosition == 2)
        {
            cells[1] = new Vector3Int(0, -1);
            cells[2] = new Vector3Int(0, -2);
            cells[3] = new Vector3Int(1, -1);
        }
        else if (rotatePosition == 3)
        {
            cells[1] = new Vector3Int(1, 0);
            cells[2] = new Vector3Int(2, 0);
            cells[3] = new Vector3Int(1, -1);
        }
        else if (rotatePosition == 4)
        {
            cells[1] = new Vector3Int(0, -1);
            cells[2] = new Vector3Int(-1, -1);
            cells[3] = new Vector3Int(0, -2);
        }
    }

    public override bool Rotate(Tilemap tilemap)
    {
        if (rotatePosition == 1)
        {
            SetTiles(tilemap, null);
            SetCells(2);

            Vector3Int nextPosition = position + new Vector3Int(0,1);
            if (board.IsValidPosition(this, nextPosition) == NO_COLLISION)
            {
                position = nextPosition;
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

            Vector3Int[] nextPositions = new Vector3Int[2];
            nextPositions[0] = position + new Vector3Int(-1, -1);
            nextPositions[1] = position + new Vector3Int(0, -1);

            foreach (Vector3Int nextPosition in nextPositions)
            {
                if (board.IsValidPosition(this, nextPosition) == NO_COLLISION)
                {
                    position = nextPosition;
                    rotatePosition = 3;
                    SetTiles(tilemap, tile);

                    return true;
                }
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

            Vector3Int nextPosition = position + new Vector3Int(1, 1);
            if (board.IsValidPosition(this, nextPosition) == NO_COLLISION)
            {
                position = nextPosition;
                rotatePosition = 4;
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

            Vector3Int[] nextPositions = new Vector3Int[2];
            nextPositions[0] = position + new Vector3Int(0, -1);
            nextPositions[1] = position + new Vector3Int(-1, -1);

            foreach (Vector3Int nextPosition in nextPositions)
            {
                if (board.IsValidPosition(this, nextPosition) == NO_COLLISION)
                {
                    position = nextPosition;
                    rotatePosition = 1;
                    SetTiles(tilemap, tile);

                    return true;
                }
            }

            // Not ok, move back to previous position
            SetCells(4);
            SetTiles(tilemap, tile);

            return false;
        }

        return true;
    }
}
