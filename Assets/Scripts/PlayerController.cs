using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    //Public Variables
    public GameController gameController;

    //Private Variables
    private GameObject[,] boardArray;

    private int playerPosition = 3;

    //Instantiation
    private void Start(){
        boardArray = gameController.GetBoardArray();
    }

    void Update(){
        bool moveLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        bool moveRight = Input.GetKeyDown(KeyCode.RightArrow);

        if (moveLeft && playerPosition > 0){
            playerPosition--;
        }else if (moveRight && playerPosition < 6){
            playerPosition++;
        }
        UpdatePlayerPosition();
    }

    void UpdatePlayerPosition(){
        GameObject targetTile = boardArray[11, playerPosition];

        this.transform.SetParent(targetTile.transform, false);
    }
}
