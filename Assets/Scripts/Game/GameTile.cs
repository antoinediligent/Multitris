using UnityEngine.Tilemaps;

/**
 * Special tiles having a tag, used to differentiate
 * player's active piece from board blocks
 */
public class GameTile : Tile
{
    public const string ACTIVE_PIECE = "activePiece";
    public const string BOARD_PIECE = "boardPiece";

    public string gameTag = ACTIVE_PIECE;
}
