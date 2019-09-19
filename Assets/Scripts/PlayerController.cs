using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    //Public Variables
    public GameController gameController;

    //Private Variables
    private int playerPosition = 3;
    private bool moveRight;
    private bool moveLeft;
    private bool grab;
    private bool place;

    //Instantiation
    private void Start(){

    }

    void Update(){
        moveLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        moveRight = Input.GetKeyDown(KeyCode.RightArrow);
        grab = Input.GetKeyDown(KeyCode.DownArrow);
        place = Input.GetKeyDown(KeyCode.UpArrow);

        if (grab){
            gameController.Grab(playerPosition);
        }

        //Detects and applies player movement
        if (moveLeft && playerPosition > 0){
            playerPosition--;
        }else if (moveRight && playerPosition < 6){
            playerPosition++;
        }
        gameController.UpdatePlayerPosition(playerPosition);
    }

    
}
