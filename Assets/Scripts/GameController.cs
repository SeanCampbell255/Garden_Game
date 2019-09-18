using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Public Variables
    public GameObject board;

    //Private Variables
    private int boardWidth = 7;
    private int boardHeight = 12;
    private GameObject[,] boardArray = new GameObject[12, 7];


    // Instantiate & Preprocess
    void Start()
    {
        GameObject currentRow;
        //Creates an array filled with tile game objects
        for(int i = 0; i < boardHeight; i++)
        {
            currentRow = board.transform.GetChild(i).gameObject;
            for(int j = 0; j < boardWidth; j++)
            {
                boardArray[i, j] = currentRow.transform.GetChild(j).gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
