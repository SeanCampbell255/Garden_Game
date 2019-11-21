using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    //Public Instance Vars
    public GameObject piece;

    //Private Instance Vars
    private GameObject[,] boardMatrix = new GameObject[7, 12];

    private void Start()
    {
        InitializeBoard();
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

                Piece newPiece = Instantiate(piece).GetComponent<Piece>();
                newPiece.SetType(rand, Piece.Trash.not);
                newPiece.gameObject.transform.position = FindPositionFromCoords(new int[] { j, i });
            }
        }
    }
    private Piece.Type RandomPieceType()
    {
        int num = (int)(Random.value * 4);

        return (Piece.Type)num;
    }

    private GameObject[] GetEntities(int[] xRange, int[] yRange)
    {
        GameObject[] entities = new GameObject[84];
        int count = 0;

        for(int i = xRange[0]; i <= xRange[1]; i++)
        {
            for(int j = yRange[0]; j <= yRange[1]; j++)
            {
                if (boardMatrix[i, j] != null)
                {
                    entities[i++] = boardMatrix[i, j];
                }
            }
        }

        return entities;
    }

    private IEnumerator RowSpawn()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                yield return new WaitForSeconds(420);
                Vector3 position = FindPositionFromCoords(new int[] { i, j });
                boardMatrix[i, j] = Instantiate(piece, position, Quaternion.identity);
            }
        }
    }
}
