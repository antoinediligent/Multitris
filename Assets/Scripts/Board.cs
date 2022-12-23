using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Sprite playerOneSprite;
    public Sprite playerTwoSprite;

    private Tilemap tilemap;
    private Vector3Int spawnSpot = new Vector3Int(4, 19);

    private float lastUpdate;
    private Piece activePiece;

    void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        if (tilemap == null)
        {
            Debug.Log("tilemap == null");
        }

        activePiece = new Piece(Tetromino.I, spawnSpot.x, spawnSpot.y, playerOneSprite);
        activePiece.SetTiles(tilemap);
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            activePiece.Rotate(tilemap);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            activePiece.MoveLeft(tilemap);
        }*/

        if (lastUpdate + 0.2f < Time.time)
        {
            bool ok = activePiece.Down(tilemap);
            lastUpdate = Time.time;

            if (!ok)
            {
                activePiece = new Piece(Tetromino.I, spawnSpot.x, spawnSpot.y, playerOneSprite);
                activePiece.SetTiles(tilemap);
            }
        }
    }
    
}