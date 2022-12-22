using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tetromino
{
    I,
    O,
    T,
    J,
    L,
    S,
    Z,
}

public class Piece
{

    public Tetromino type;
    private int rotationPosition;
    private GameObject[] bricks = new GameObject[4];

    public Piece(Tetromino type, GameObject brickOne, GameObject brickTwo, GameObject brickThree, GameObject brickFour)
    {
        bricks[0] = brickOne;
        bricks[1] = brickTwo;
        bricks[2] = brickThree;
        bricks[3] = brickFour;
        switch (type)
        {
            case Tetromino.I:
                brickOne.transform.SetPositionAndRotation(new Vector3(1.5f, 19.5f), brickOne.transform.rotation);
                brickTwo.transform.SetPositionAndRotation(new Vector3(1.5f, 18.5f), brickOne.transform.rotation);
                brickThree.transform.SetPositionAndRotation(new Vector3(1.5f, 17.5f), brickOne.transform.rotation);
                brickFour.transform.SetPositionAndRotation(new Vector3(1.5f, 16.5f), brickOne.transform.rotation);
                rotationPosition = 1;
                break;
        }
    }

    public void Down()
    {
        for (int i = bricks.Length-1; i >= 0; i--)
        {
            bricks[i].transform.Translate(Vector3.down);
        }
    }

    public bool isAtBottom()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i].transform.position.y <= 0.5f)
            {
                return true;
            }
        }
        return false;
    }

    public void insert(int[][] boardState)
    {

    }
}
