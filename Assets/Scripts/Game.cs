using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    //Public Instance Vars
    public GameObject piece;

    public float spawnTime = 4.0f;

    //Private Instance Vars
    private GameObject[,] boardMatrix = new GameObject[7, 12];

    private void Start()
    {
        InitializeBoard();
        StartCoroutine(RowSpawn());
    }

    //Adds GameObject entity to matrix at int[2] coords
    public void AddToMatrix(GameObject entity, int[] coords)
    {
        boardMatrix[coords[0], coords[1]] = entity;
    }

    //Returns GameObject at int[2] coords from matrix
    public GameObject GetFromMatrix(int[] coords)
    {
        return boardMatrix[coords[0], coords[1]];
    }

    //Removes GameObject at int[2] coords from matrix & returns it
    public GameObject RemoveFromMatrix(int[] coords)
    {
        GameObject temp = boardMatrix[coords[0], coords[1]];
        boardMatrix[coords[0], coords[1]] = null;
        return temp;
    }

    //Takes int[] coords to calculate and return the Vector3 world position
    public Vector3 FindPositionFromCoords(int[] coords)
    {
        float x = (coords[0] - 3) * 0.9f;
        float y = 0.46f + (5 - coords[1]) * 0.9f;

        return new Vector3(x, y, 0.0f);
    }

    private void InitializeBoard()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                Piece.Type rand = RandomPieceType();

                Piece newPiece = Instantiate(piece, FindPositionFromCoords(new int[] {j, i}),
                                             Quaternion.identity).GetComponent<Piece>();
                newPiece.coords = new int[] {j, i};
                boardMatrix[j, i] = newPiece.gameObject;
                newPiece.SetType(rand, Piece.Trash.not);
            }
        }
    }
    private Piece.Type RandomPieceType()
    {
        int num = (int)(Random.value * 4);

        return (Piece.Type)num;
    }

    private GameObject[] GetPieces(int[] xRange, int[] yRange)
    {
        GameObject[] pieces = new GameObject[84];
        int count = 0;

        for(int i = xRange[0]; i <= xRange[1]; i++)
        {
            for(int j = yRange[0]; j <= yRange[1]; j++)
            {
                if (boardMatrix[i, j] != null && boardMatrix[i, j].tag != "Player")
                {
                    pieces[count] = boardMatrix[i, j];
                }
                count++;
            }
        }

        return pieces;
    }

    private IEnumerator RowSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            GameObject[] pieces = GetPieces(new int[] {0, 6}, new int[] {0, 11});
            foreach(GameObject obj in pieces)
            {
                if (obj)
                {
                    Piece movingPiece = obj.GetComponent<Piece>();
                    Debug.Log(movingPiece.coords[0] + " " + movingPiece.coords[1] + " " + movingPiece.GetPieceType());
                    movingPiece.Move(1);
                }
            }

            for (int i = 0; i < 7; i++)
            {
                Vector3 position = FindPositionFromCoords(new int[] { i, -1 });
                Piece.Type rand = RandomPieceType();

                boardMatrix[i, 0] = Instantiate(piece, position, Quaternion.identity);
                boardMatrix[i, 0].GetComponent<Piece>().SetType(rand, Piece.Trash.not);
                boardMatrix[i, 0].GetComponent<Piece>().coords = new int[] {i, 0};
                boardMatrix[i, 0].GetComponent<Piece>().Move(1);

            }
        }
    }
}
