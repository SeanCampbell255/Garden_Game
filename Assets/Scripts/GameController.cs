using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Public Variables
    public GameObject board;
    public GameObject player;
    public GameObject piece;

    public enum PieceType { Trash, Seed, Sprout, Bud, Flower };

    //Private Variables
    private int boardWidth = 7;
    private int boardHeight = 12;
    private int basketSize;
    

    private GameObject[,] boardArray = new GameObject[12, 7];


    // Instantiate & Preprocess
    void Start(){
        GameObject currentRow;

        //Creates an array filled with tile game objects
        for(int i = 0; i < boardHeight; i++){
            currentRow = board.transform.GetChild(i).gameObject;

            for(int j = 0; j < boardWidth; j++){
                boardArray[i, j] = currentRow.transform.GetChild(j).gameObject;
            }
        }
    }

    //Moves player to playerPosition and childs it to the tile at position
    public void UpdatePlayerPosition(int playerPosition){
        GameObject targetTile = boardArray[11, playerPosition];

        player.transform.SetParent(targetTile.transform, false);
    }

    //Grabs all pieces of same type adjacent to lowest piece in column
    public void Grab(int playerPosition){

    }
}
