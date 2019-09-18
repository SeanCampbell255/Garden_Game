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
        bool moveLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        bool moveRight = Input.GetKeyDown(KeyCode.RightArrow);

        if (moveLeft && playerPosition > 0){
            playerPosition--;
        }else if (moveRight && playerPosition < 6){
            playerPosition++;
        }
        gameController.UpdatePlayerPosition(playerPosition);
    }

    
}
