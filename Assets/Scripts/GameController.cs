using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Public Variables
    public GameObject board;
    public GameObject player;

    //Private Variables
    private int boardWidth = 7;
    private int boardHeight = 12;
    private GameObject[,] boardArray = new GameObject[7, 12];
    private Stack basket = new Stack();


    // Instantiate & Preprocess
    void Start(){
        GameObject currentRow;

        //Creates an array filled with tile game objects
        for(int i = 0; i < boardHeight; i++){
            currentRow = board.transform.GetChild(i).gameObject;

            for(int j = 0; j < boardWidth; j++)
            {
                boardArray[j, i] = currentRow.transform.GetChild(j).gameObject;
            }
        }
    }

    //Moves player to horizontal tile according to playerPosition index
    public void UpdatePlayerPosition(int playerPosition){
        GameObject targetTile = boardArray[playerPosition, 11];

        player.transform.SetParent(targetTile.transform, false);
    }

    public void grabColumn(int playerPosition){


        for(int i = boardHeight; i >= 0; i--){
            if(boardArray[playerPosition, i].transform.childCount > 0){

            }
        }
    }

    public GameObject[] GetAdjacencies(int xPosition, int yPosition){
        GameObject[] adjacencyArray = new GameObject[4];

        if(yPosition > 0){
            Transform aboveTile = boardArray[xPosition, yPosition - 1].transform;

            if(aboveTile.childCount > 0){
                adjacencyArray[0] = aboveTile.GetChild(0).gameObject;
            }
        }
        if (xPosition < boardWidth){
            Transform rightTile = boardArray[xPosition + 1, yPosition].transform;

            if (rightTile.childCount > 0)
            {
                adjacencyArray[1] = rightTile.GetChild(0).gameObject;
            }
        }
        if (yPosition < boardHeight){
            Transform belowTile = boardArray[xPosition, yPosition + 1].transform;

            if (belowTile.childCount > 0)
            {
                adjacencyArray[2] = belowTile.GetChild(0).gameObject;
            }
        }
        if (xPosition > 0){
            Transform leftTile = boardArray[xPosition - 1, yPosition].transform;

            if (leftTile.childCount > 0)
            {
                adjacencyArray[3] = leftTile.GetChild(0).gameObject;
            }
        }
        return adjacencyArray;
    }
}
