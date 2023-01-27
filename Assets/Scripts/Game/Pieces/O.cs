using UnityEngine;
using UnityEngine.Tilemaps;

public class O : Piece
{
    public O(Vector3Int position, Sprite sprite) : base(position, sprite)
    {
    }

    protected override void SetCells(int rotatePosition)
    {
        cells[0] = new Vector3Int(0, 0);
        cells[1] = new Vector3Int(0, -1);
        cells[2] = new Vector3Int(1, 0);
        cells[3] = new Vector3Int(1, -1);
    }

    public override bool Rotate(Tilemap tilemap)
    {
        // This piece can't rotate
        return true;
    }
}
