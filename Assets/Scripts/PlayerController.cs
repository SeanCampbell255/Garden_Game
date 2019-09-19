using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    //Public Variables
    public GameController gameController;

    //Private Variables
    private int playerPosition = 3;

    //Instantiation
    private void Start(){

    }

    void Update(){
        //Gets input from arrow keys
        bool moveLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        bool moveRight = Input.GetKeyDown(KeyCode.RightArrow);
        bool grab = Input.GetKeyDown(KeyCode.DownArrow);
        bool place = Input.GetKeyDown(KeyCode.UpArrow);

        //Updates player position var & tells GameController to update the actual position
        if (moveLeft && playerPosition > 0){
            playerPosition--;
        }else if (moveRight && playerPosition < 6){
            playerPosition++;
        }
        gameController.UpdatePlayerPosition(playerPosition);

        if (grab){
            gameController.grabColumn(playerPosition);
        }
    }
}
