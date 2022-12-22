using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    private int[][] boardState;

    public GameObject prefabBlueBrick;
    public Vector3 popPlace = new Vector3(1.5f, 19.5f, 0);

    private float lastUpdate;
    private Piece activePiece;

    void Start()
    {
        boardState = new int[10][];
        for (int i = 0; i < 10; i++)
        {
            boardState[i] = new int[20];
        }
        instantiatePiece();
        lastUpdate = Time.time;
    }

    void Update()
    {
        if (lastUpdate + 0.2f < Time.time)
        {
            activePiece.Down();
            lastUpdate = Time.time;

            if (activePiece.isAtBottom())
            {
                activePiece.insert();
                instantiatePiece();
            }
        }
    }

    void instantiatePiece()
    {
        GameObject brickOne = Instantiate(prefabBlueBrick, popPlace, prefabBlueBrick.transform.rotation);
        GameObject brickTwo = Instantiate(prefabBlueBrick, popPlace, prefabBlueBrick.transform.rotation);
        GameObject brickThree = Instantiate(prefabBlueBrick, popPlace, prefabBlueBrick.transform.rotation);
        GameObject brickFour = Instantiate(prefabBlueBrick, popPlace, prefabBlueBrick.transform.rotation);
        activePiece = new Piece(Tetromino.I, brickOne, brickTwo, brickThree, brickFour);
    }
}