using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    //Public Instance Vars

    //Private Instance Vars
    private GameObject[,] boardMatrix = new GameObject[7, 12];

    private void Update()
    {
        
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
}
