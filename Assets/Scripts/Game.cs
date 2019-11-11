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

    public void AddToMatrix(GameObject entity, int[] coords)
    {
        boardMatrix[coords[0], coords[1]] = entity;
    }
}
