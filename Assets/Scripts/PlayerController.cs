using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    //Public Variables
    public GameController gameController;

    public bool canGrab;
    public bool canPlace;

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

        //Detects a grab or place, makes it so you can't do both same frame
        if (grab && canGrab){
            gameController.Grab(playerPosition);
        }else if (place && canPlace){
            gameController.Place(playerPosition);
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
