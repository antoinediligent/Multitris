using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private int playerIndex;
    private Board board;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerIndex = playerInput.playerIndex;
        board = GameObject.Find("Tilemap").GetComponent<Board>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        board.PlayerMove(playerIndex, context.ReadValue<Vector2>());
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        board.PlayerSlowTap(playerIndex, context.phase.ToString(), context.control.name);
    }
}