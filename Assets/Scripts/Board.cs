using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public const int BOARD_WIDTH = 10;
    public const int BOARD_HEIGHT = 20;

    public Sprite playerOneSprite;
    public Sprite playerTwoSprite;

    private Tilemap tilemap;
    private Vector3Int spawnSpot = new Vector3Int(4, 19);

    private float lastUpdate;
    private Piece activePiece;

    private float lastInput;
    const float inputDealy = 0.1f;

    int debugCounter = 0;
    bool ok = true;

    void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();

        activePiece = NewPiece();
        activePiece.SetTiles(tilemap);
    }

    Piece NewPiece()
    {
        return new I(spawnSpot.x, spawnSpot.y, playerOneSprite);
    }

    void Update()
    {
        if (Time.time > lastInput + 0.2f)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                activePiece.MoveLeft(tilemap);
                lastInput = Time.time;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                activePiece.MoveRight(tilemap);
                lastInput = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            activePiece.Rotate(tilemap);
        }

        if (lastUpdate + 0.2f < Time.time)
        {
            // Only for debug
            if (debugCounter <= 400)
            {
                debugCounter++;
                ok = activePiece.Down(tilemap);
                lastUpdate = Time.time;
            }

            // Give the player an opportunity to move his piece just before it's sealed
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                activePiece.MoveLeft(tilemap);
                lastInput = Time.time;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                activePiece.MoveRight(tilemap);
                lastInput = Time.time;
            }

            if (!ok || activePiece.IsAtBottom())
            {
                activePiece = NewPiece();
                activePiece.SetTiles(tilemap);
            }
        }
    }

}